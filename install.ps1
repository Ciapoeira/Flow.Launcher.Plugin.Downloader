$Flow   = "$env:LOCALAPPDATA\FlowLauncher\Flow.Launcher.exe"
$TargetDir = "$env:APPDATA\FlowLauncher\Plugins"
$Url = "https://github.com/Ciapoeira/Flow.Launcher.Plugin.Downloader/releases/latest/download/Flow.Launcher.Plugin.Downloader.zip"
$Zip = "$TargetDir\temp.zip"

winget install --id "yt-dlp.yt-dlp"

Get-Process Flow.Launcher -ErrorAction SilentlyContinue | Stop-Process -Force

Write-Host "Downloading plugin..." -ForegroundColor Cyan
Invoke-WebRequest -Uri $Url -OutFile $Zip

Expand-Archive -Path $Zip -DestinationPath $TargetDir -Force

Remove-Item -Path $Zip -Force

Start-Process $Flow

Write-Host "Installation Complete!" -ForegroundColor Green

