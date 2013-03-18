using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCMasterLayoutShare
{
    public class MasterLayoutRazorViewEngine : RazorViewEngine
    {
        public MasterLayoutRazorViewEngine()
        {
            MasterLocationFormats = MasterLocationFormats.Union(new[] { 
                "~/Shared/Views/Shared/{1}/{0}.cshtml", 
                "~/Shared/Views/Shared/{0}.cshtml" 
            }).ToArray();

            ViewLocationFormats = ViewLocationFormats.Union(new[] { 
                "~/Shared/Views/{1}/{0}.cshtml", 
                "~/Shared/Views/{1}/{0}.cshtml", 
                "~/Shared/Views/Shared/{0}.cshtml", 
                "~/Shared/Views/Shared/{0}.cshtml" 
            }).ToArray();

            PartialViewLocationFormats = ViewLocationFormats;
        }
    }
}
