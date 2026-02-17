using System.Diagnostics;

namespace Flow.Launcher.Plugin.Downloader.Helpers;

public static class Cmd {
    public static async Task<string?> ExecuteAsync(string exe, List<string> args, bool isSilent,
        bool RedirectOutput,
        CancellationToken token = default) {
        ProcessStartInfo psi = new(exe) {
            RedirectStandardOutput = RedirectOutput,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = isSilent,
        };

        foreach (var arg in args) psi.ArgumentList.Add(arg);

        try {
            using var p = Process.Start(psi)!;

            var output = await p.StandardOutput.ReadToEndAsync(token);
            await p.WaitForExitAsync(token);

            return output;
        } catch (OperationCanceledException) {
            throw;
        } catch {
            return null;
        }
    }
}
