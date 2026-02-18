$Target = "$PSScriptRoot\bin\Debug\net9.0-windows"
$Deploy = "$env:APPDATA\FlowLauncher\Plugins\Flow.Launcher.Plugin.Downloader"
$Flow   = "$env:LOCALAPPDATA\FlowLauncher\Flow.Launcher.exe"

Get-Process Flow.Launcher -ErrorAction SilentlyContinue | Stop-Process -Force

Start-Sleep -Milliseconds 500

Get-ChildItem -Path $Target -Include *.pdb -Recurse | Remove-Item -Force

robocopy "$Target" "$Deploy" /MIR /NJH /NJS /NDL /NC /NS

Start-Process $Flow
