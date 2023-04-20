using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Web.WebView2.WinForms;
using WADH.Core;

namespace NewCurseTest
{
    public partial class MainForm : Form
    {
        private readonly ICurseHelper curseHelper;

        public MainForm(ICurseHelper curseHelper)
        {
            this.curseHelper = curseHelper ?? throw new ArgumentNullException(nameof(curseHelper));

            InitializeComponent();

            textBoxUrl.Text = "https://www.curseforge.com/wow/addons/deadly-boss-mods/download";
            webView.Dock = DockStyle.Fill;
            richTextBoxJson.BackColor = Color.White;
            WindowState = FormWindowState.Maximized;
        }

        private async void ButtonLoad_Click(object sender, EventArgs e)
        {
            await webView.EnsureCoreWebView2Async();
            
            webView.CoreWebView2.Settings.IsScriptEnabled = false;
            webView.CoreWebView2.AddWebResourceRequestedFilter("*", Microsoft.Web.WebView2.Core.CoreWebView2WebResourceContext.All);
            
            webView.NavigationCompleted += WebView_NavigationCompleted;
            webView.CoreWebView2.WebResourceRequested += CoreWebView2_WebResourceRequested;
           
            var url = textBoxUrl.Text;
            webView.Source = new Uri(url);
        }

        private void CoreWebView2_WebResourceRequested(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2WebResourceRequestedEventArgs e)
        {
            AddLine(e.Request.Uri);

            if (e.Request.Uri.EndsWith(".webm"))
            {
                AddLine("Detected video webm web resource.");

                var response = webView.CoreWebView2.Environment.CreateWebResourceResponse(
                    null,
                    404,
                    "NotFound",
                    null);
                e.Response = response;
            }
        }

        private async void ButtonFetch_Click(object sender, EventArgs e)
        {
            var script = """
                document.querySelector('body').style.color = 'red';
                let color = document.querySelector('body').style.color;
                let j = document.querySelector('script#__NEXT_DATA__').innerHTML;
                console.log(j);
                let x = JSON.parse(j);
                console.log(x);
                let s = JSON.stringify(x);
                console.log(s);
                s;
                """;

            var json = await webView.ExecuteScriptAsync(script);
            var s = Regex.Unescape(json);
            s = s.Trim('"').Trim();
            richTextBoxJson.Clear();
            AddLine(s);

            //json = json.Replace("\\", "");
            //var s = Regex.Unescape(json);
            //json = json.Replace(@"\\\", "").Replace("\\", "");
            //s = Regex.Unescape(s);
        }

        private void ButtonTest_Click(object sender, EventArgs e)
        {
            var json = richTextBoxJson.Text;
            var j = curseHelper.SerializePageJson(json);
            richTextBoxJson.Clear();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void WebView_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (sender is WebView2 webViewLocal)
            {
                webViewLocal.NavigationCompleted -= WebView_NavigationCompleted;

                if (e.IsSuccess && e.HttpStatusCode == 200)
                {
                    AddLine("Site successfully loaded");
                }
            }
        }

        private void AddLine(string s)
        {
            richTextBoxJson.AppendText(s);
            richTextBoxJson.AppendText(Environment.NewLine);
        }
    }
}
