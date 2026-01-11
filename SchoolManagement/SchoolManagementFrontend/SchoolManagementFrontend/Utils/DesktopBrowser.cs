using Duende.IdentityModel.OidcClient.Browser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagementFrontend.Utils
{
    public class DesktopBrowser : IBrowser
    {
        private readonly int _port;

        public DesktopBrowser(int port = 7890)
        {
            _port = port;
        }

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            using var listener = new HttpListener();
            listener.Prefixes.Add($"http://127.0.0.1:{_port}/");
            listener.Start();

            // Open system browser
            Process.Start(new ProcessStartInfo
            {
                FileName = options.StartUrl,
                UseShellExecute = true
            });

            // Wait for redirect request
            var context = await listener.GetContextAsync();

            // Respond to the browser
            string responseString = "<html><body>Login successful. You can close this window.</body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer);
            context.Response.OutputStream.Close();

            listener.Stop();

            // Return the redirect URL to OidcClient
            return new BrowserResult
            {
                Response = context.Request.RawUrl ?? "",
                ResultType = BrowserResultType.Success
            };
        }
    }
}
