# Installation
Using the [script](https://github.com/Ciapoeira/Flow.Launcher.Plugin.Downloader/blob/master/install.ps1) it downloads everything you need automatically including yt-dlp, ffmpeg, deno (temporary solution)
```pwsh
irm https://raw.githubusercontent.com/Ciapoeira/Flow.Launcher.Plugin.Downloader/refs/heads/master/install.ps1 | iex
```
## Manual installation:
 - [yt-dlp](https://github.com/yt-dlp/yt-dlp) or `winget install --id yt-dlp.yt-dlp` (installs ffmpeg and deno too)
 - [ffmpeg](https://www.ffmpeg.org/download.html#build-windows) (Optional but recommended)
 - Get the latest [plugin release](https://github.com/Ciapoeira/Flow.Launcher.Plugin.Downloader/releases/latest/download/Flow.Launcher.Plugin.Downloader.zip) and extract it to `%appdata%\FlowLauncher\Plugins\`

 # Common issues
 - Wrong plugin configuration
 - Outdated yt-dlp version (`yt-dlp -U` or if installed via winget `winget upgrade --id yt-dlp.yt-dlp`)

