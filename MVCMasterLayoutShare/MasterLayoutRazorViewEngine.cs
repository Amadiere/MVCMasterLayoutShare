using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCMasterLayoutShare
{
    public class MasterLayoutRazorViewEngine : RazorViewEngine
    {
        private static string MasterLayoutVirtualDirectory
        {
            get
            {
                var configDir = ConfigurationManager.AppSettings["MasterLayoutVirtualDirectory"];
                // Defaults the virtual directory to "Shared" if one hasn't been set in the config.
                string virtualDirectory = (configDir ?? "Shared").ToString();
                return "/" + virtualDirectory;
            }
        }

        public MasterLayoutRazorViewEngine()
        {
            MasterLocationFormats = MasterLocationFormats.Union(new[] { 
                "~/" + MasterLayoutVirtualDirectory + "/Views/Shared/{1}/{0}.cshtml", 
                "~/" + MasterLayoutVirtualDirectory + "/Views/Shared/{0}.cshtml" 
            }).ToArray();

            ViewLocationFormats = ViewLocationFormats.Union(new[] { 
                "~/" + MasterLayoutVirtualDirectory + "/Views/{1}/{0}.cshtml", 
                "~/" + MasterLayoutVirtualDirectory + "/Views/{1}/{0}.cshtml", 
                "~/" + MasterLayoutVirtualDirectory + "/Views/Shared/{0}.cshtml", 
                "~/" + MasterLayoutVirtualDirectory + "/Views/Shared/{0}.cshtml" 
            }).ToArray();

            PartialViewLocationFormats = ViewLocationFormats;
        }
    }
}
