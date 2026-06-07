# check-lista-negra.ps1 - hook PostToolUse (Edit|Write) del vault BibliotecaCursos.
# Avisa si una edicion a prosa de curso introduce un termino de la lista grep-able
# (_terminos-grep.txt, subconjunto literal de escritura-humana/lista-negra.md seccion 8).
#
# Contrato: lee el payload JSON del hook por stdin, saca tool_input.file_path,
# y SOLO actua si el fichero es .md bajo temario/ , presentaciones/ o locucion/
# de algun Desarrollo. Si encuentra hits: escribe aviso en stderr y sale con codigo 2
# (PostToolUse devuelve ese stderr a Claude como feedback). Si no, sale 0 en silencio.
# Pase lo que pase, NUNCA rompe el flujo: cualquier error -> exit 0.
# ASCII-only a proposito (robusto frente a encoding en pwsh y Windows PowerShell 5.1).

$ErrorActionPreference = 'Stop'
try {
    $raw = [Console]::In.ReadToEnd()
    if ([string]::IsNullOrWhiteSpace($raw)) { exit 0 }

    $payload = $raw | ConvertFrom-Json
    $fp = $payload.tool_input.file_path
    if ([string]::IsNullOrWhiteSpace($fp)) { exit 0 }

    try { $fpFull = [System.IO.Path]::GetFullPath($fp) } catch { $fpFull = $fp }
    $fpNorm = $fpFull -replace '/', '\'
    if ($fpNorm -notmatch '\\Desarrollos\\')                       { exit 0 }
    if ($fpNorm -notmatch '\\(temario|presentaciones|locucion)\\') { exit 0 }
    if ($fpNorm -notmatch '\.md$')                                 { exit 0 }
    if (-not (Test-Path -LiteralPath $fp))                         { exit 0 }

    $termsFile = Join-Path $PSScriptRoot '..\references\_terminos-grep.txt'
    if (-not (Test-Path -LiteralPath $termsFile)) { exit 0 }

    $patterns = Get-Content -LiteralPath $termsFile -Encoding UTF8 |
        Where-Object { $_ -and ($_ -notmatch '^\s*#') } |
        ForEach-Object { $_.Trim() } |
        Where-Object { $_ }
    if (-not $patterns) { exit 0 }

    $lines = @(Get-Content -LiteralPath $fp -Encoding UTF8)
    $hits = New-Object System.Collections.Generic.List[string]

    for ($i = 0; $i -lt $lines.Count; $i++) {
        $ln = $lines[$i]
        # El Changelog/metadocumentacion va al final y cita terminos a proposito: no se escanea.
        if ($ln -match '^\s*##\s+Changelog') { break }
        foreach ($p in $patterns) {
            if ($ln -match "(?i)$p") {
                $snippet = $ln.Trim()
                if ($snippet.Length -gt 90) { $snippet = $snippet.Substring(0, 90) + '...' }
                $hits.Add(("  L{0}: [{1}]  ->  {2}" -f ($i + 1), $Matches[0], $snippet))
                break
            }
        }
    }

    if ($hits.Count -gt 0) {
        $name = [System.IO.Path]::GetFileName($fp)
        $body = $hits -join "`n"
        $msg = "[escritura-humana] Posible(s) termino(s) de LISTA NEGRA en $name :`n$body`nContrasta con .claude/skills/escritura-humana/references/lista-negra.md (seccion 8) y reformula si aplica (p.ej. 'superficie' -> 'entorno de uso / por donde lo usas'). Si es falso positivo, ignoralo."
        [Console]::Error.WriteLine($msg)
        exit 2
    }
    exit 0
}
catch {
    exit 0
}
