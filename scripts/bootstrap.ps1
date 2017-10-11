param (
    $SourceRoot = "$PSScriptRoot/..",
    $PaketBootstrapper = "$SourceRoot/.paket/paket.exe",

    $PaketBootstrapperUrl = "https://github.com/fsprojects/Paket/releases/download/5.114.0/paket.bootstrapper.exe"
)

$ErrorActionPreference = 'Stop'

if (!(Test-Path $PaketBootstrapper)) {
    Invoke-WebRequest $PaketBootstrapperUrl -UseBasicParsing -OutFile $PaketBootstrapper
}

& $PaketBootstrapper restore
