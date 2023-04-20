using System.Reflection.Metadata;
using System.Text.Json;
using NewCurseTest;

namespace WADH.Core
{
    public sealed class CurseHelper : ICurseHelper
    {
        public string AdjustPageAppearanceScript => GetAdjustPageAppearanceScript();
        public string GrabJsonFromPageScript => GetGrabJsonFromPageScript();

        public bool IsAddonUrl(string url)
        {
            // https://www.curseforge.com/wow/addons/coordinates/download
            url = Guard(url);
            return url.StartsWith("https://www.curseforge.com/wow/addons/") && url.EndsWith("/download");
        }

        public bool IsRedirect1Url(string url)
        {
            // https://www.curseforge.com/wow/addons/coordinates/download/4364314/file
            url = Guard(url);
            return url.StartsWith("https://www.curseforge.com/wow/addons/") && url.EndsWith("/file");
        }

        public bool IsRedirect2Url(string url)
        {
            // https://edge.forgecdn.net/files/4364/314/Coordinates-2.4.1.zip?api-key=267C6CA3
            url = Guard(url);
            return url.StartsWith("https://edge.forgecdn.net/files/") && url.Contains("?api-key=");
        }

        public bool IsDownloadUrl(string url)
        {
            // https://mediafilez.forgecdn.net/files/4364/314/Coordinates-2.4.1.zip
            url = Guard(url);
            return url.StartsWith("https://mediafilez.forgecdn.net/files/") && url.EndsWith(".zip");
        }

        public string GetAddonNameFromAddonUrl(string url)
        {
            // https://www.curseforge.com/wow/addons/coordinates/download
            url = Guard(url);
            return IsAddonUrl(url) ? url.Split("addons/").Last().Split("/download").First().ToLower() : string.Empty;
        }

        public string GetAddonNameFromRedirect1Url(string url)
        {
            // https://www.curseforge.com/wow/addons/coordinates/download/4364314/file
            url = Guard(url);
            return IsRedirect1Url(url) ? url.Split("addons/").Last().Split("/download").First().ToLower() : string.Empty;
        }

        public string GetAddonNameFromDownloadUrl(string url)
        {
            // https://mediafilez.forgecdn.net/files/4364/314/Coordinates-2.4.1.zip
            url = Guard(url);
            return IsDownloadUrl(url) ? url.Split('/').Last().Split('-').First().ToLower() : string.Empty;
        }

        public string GetFileNameFromDownloadUrl(string url)
        {
            // https://mediafilez.forgecdn.net/files/4364/314/Coordinates-2.4.1.zip
            url = Guard(url);
            return IsDownloadUrl(url) ? url.Split('/').Last() : string.Empty;
        }

        public CurseHelperJson SerializePageJson(string json)
        {
            // Curse page JSON format
            /*
            props
              pageProps
                project
                  name            --> Useful name of addon.                   Example --> "Deadly Boss Mods (DBM)"
                  id              --> Short id number for download url.       Example --> 3358
                  mainFile
                    id            --> Long id number for download url.        Example --> 4485146
                    fileName      --> The name of the zip file.               Example --> "DBM-10.0.35.zip"
                    displayName   --> Useful name of addon (incl. version).   Example --> "DBM-10.0.35"
                    slug          --> Slug name of the addon.                 Example --> "deadly-boss-mods"
            */
            // Example for download url --> https://www.curseforge.com/api/v1/mods/3358/files/4485146/download


            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentException($"'{nameof(json)}' cannot be null or whitespace.", nameof(json));
            }

            var doc = JsonDocument.Parse(json);
            var project = doc.RootElement.GetProperty("props").GetProperty("pageProps").GetProperty("project");
            var projectName = project.GetProperty("name").GetString() ?? string.Empty;
            var projectId = project.GetProperty("id").GetUInt64().ToString();
            var slug = project.GetProperty("slug").GetString() ?? string.Empty;
            var mainFile = project.GetProperty("mainFile");
            var mainFileId = mainFile.GetProperty("id").GetUInt64().ToString();
            var mainFileFileName = mainFile.GetProperty("fileName").GetString() ?? string.Empty;
            var mainFileDisplayName = mainFile.GetProperty("displayName").GetString() ?? string.Empty;

            return new CurseHelperJson(
                new CurseProjectJson(
                    projectName,
                    projectId,
                    new CurseMainFileJson(
                        mainFileId,
                        mainFileFileName,
                        mainFileDisplayName),
                    slug));
        }

        public string BuildDownloadUrl(CurseHelperJson curseHelperJson)
        {
            throw new NotImplementedException();
        }

        private static string GetAdjustPageAppearanceScript()
        {
            // The app disables the JS engine, before loading the addon page, to prevent the 5 sec timer (JS) from running.
            // With disabled JS some noscript tags become active and all relevant information is no longer visible at top.
            // Therefore hiding the empty img and the "JS is disabled" message, then all relevant stuff moves to top again.
            // And since WebView2 offers no property to hide scrollbars, the last line here does this, as some final step.

            return
                "let img = document.querySelector('body img');" +
                "if (img) { img.style.visibility = 'hidden'; img.style.height = '0px'; }" +
                "let noscripts = document.querySelectorAll('body noscript');" +
                "if (noscripts && noscripts.length >= 2) { noscripts[1].style.visibility = 'hidden'; noscripts[1].style.height = '0px'; }" +
                "let containers = document.querySelectorAll('body div.container');" +
                "if (containers && containers.length >= 4) { containers[3].style.visibility = 'hidden'; containers[3].style.height = '50px'; }" +
                "document.body.style.overflow = 'hidden'";
        }

        private static string GetGrabJsonFromPageScript()
        {
            return "document.querySelector('script#__NEXT_DATA__').innerHTML;";
        }

        private static string Guard(string url)
        {
            return url?.Trim() ?? string.Empty;
        }
    }
}
