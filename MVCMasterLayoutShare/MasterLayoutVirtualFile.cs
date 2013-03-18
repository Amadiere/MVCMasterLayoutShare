using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace MVCMasterLayoutShare
{
    public class MasterLayoutVirtualFile : VirtualFile
    {
        public string path;
 
        public MasterLayoutVirtualFile(string virtualPath, string fullPath) : base(virtualPath)
        {
            this.path = fullPath;
        }

        public override System.IO.Stream Open()
        {
            // Maybe doesn't need the ASCII encoding line now, probably just works with File.ReadAllBytes()....
            ASCIIEncoding encoding = new ASCIIEncoding();
            return new MemoryStream(encoding.GetBytes(File.ReadAllText(path)), false);
        }
    }
}
