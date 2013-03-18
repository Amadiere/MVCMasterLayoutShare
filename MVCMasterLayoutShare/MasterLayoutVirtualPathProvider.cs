using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Hosting;

namespace MVCMasterLayoutShare
{
    /// <summary>
    /// Enables the use of a shared Header & Footer / layout within web projects. There are certain requirements that are required for this
    /// to be successful.
    /// - You need to have published the project used for the master layouts to a directory that maps to /MasterLayoutVirtualDirectory within IIS (same box).
    /// - You need to have a 'MasterLayoutPath' setting within the AppSettings within the Web.Config of the calling project that points to the filesystem location of the project root.
    /// - You need to use the Register() method to set it up within your Application_Start method in Global.asax.cs (or similar).
    /// - You optionally can have a 'MasterLayoutVirtualDirectory' setting within the AppSettings of the Web.Config of the calling project, the default value is 'Shared'
    /// </summary>
    public class MasterLayoutVirtualPathProvider : VirtualPathProvider
    {
        /// <summary>
        /// Registers this VirtualPathProvider and enables the serving of content from the shared directory within the
        /// virtual IIS directory and the 'MasterLayoutPath' as set within the Web.config AppSetting node.
        /// </summary>
        public static void Register()
        {
            HostingEnvironment.RegisterVirtualPathProvider(new MasterLayoutVirtualPathProvider());
        }

        /// <summary>
        /// Gets a value that indicates whether a file exists in the virtual file system, based on the 'MasterLayoutPath' setting
        /// from the AppSettings node within the Web.config file.
        /// </summary>
        /// <param name="virtualPath">The path to the virtual file.</param>
        /// <returns>
        /// true if the file exists in the virtual file system; otherwise, false.
        /// </returns>
        public override bool FileExists(string virtualPath)
        {
            if (IsMasterLayoutView(virtualPath))
            {
                return true;
            }
            return base.FileExists(virtualPath);
        }

        /// <summary>
        /// Determines whether the file is within the specified virtual path.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns>
        ///   <c>true</c> if is part of shared library; otherwise, <c>false</c>.
        /// </returns>
        private bool IsMasterLayoutView(string virtualPath)
        {
            bool exists = (File.Exists(TransformVirtualPathToAbsolute(virtualPath)));
            return exists;
        }

        /// <summary>
        /// Gets the master layout path from the Web.config AppSettings node.
        /// </summary>
        private static string MasterLayoutPath
        {
            get { return ConfigurationManager.AppSettings["MasterLayoutPath"].ToString(); }
        }

        /// <summary>
        /// Gets the master layout virtual directory name from the Web.config AppSettings node.
        /// </summary>
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

        /// <summary>
        /// Transforms the virtual path to absolute path based on some very bias methods which revolve around the notion that
        /// if you want the shared area, you use a common directory at the start of your string, taken from the 'MasterLayoutVirtualDirectory'
        /// setting in the config for the application.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns>An absolute filesystem path.</returns>
        private static string TransformVirtualPathToAbsolute(string virtualPath)
        {
            string fullPath = virtualPath;
            string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
            string shared = MasterLayoutVirtualDirectory;

            if (fullPath.StartsWith("~" + shared))
            {
                fullPath = MasterLayoutPath + fullPath.Substring(shared.Length + 1);
            }
            else if (fullPath.StartsWith(shared))
            {
                fullPath = MasterLayoutPath + fullPath.Substring(shared.Length);
            }
            else if (virtualPath.StartsWith(applicationPath + shared))
            {
                fullPath = MasterLayoutPath + fullPath.Substring((applicationPath + shared).Length);
            }
            return fullPath;
        }

        /// <summary>
        /// Gets a virtual file from the virtual file system, occasionally of type MasterLayoutVirtualFile.
        /// </summary>
        /// <param name="virtualPath">The path to the virtual file.</param>
        /// <returns>
        /// A descendent of the <see cref="T:System.Web.Hosting.VirtualFile"/> class that represents a file in the virtual file system.
        /// </returns>
        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsMasterLayoutView(virtualPath))
            {
                string fullPath = TransformVirtualPathToAbsolute(virtualPath);
                MasterLayoutVirtualFile file = new MasterLayoutVirtualFile(virtualPath, fullPath);
                if (file != null)
                {
                    return file;
                }
            }
            return base.GetFile(virtualPath);
        }

        /// <summary>
        /// Creates a cache dependency based on the specified virtual paths.
        /// </summary>
        /// <param name="virtualPath">The path to the primary virtual resource.</param>
        /// <param name="virtualPathDependencies">An array of paths to other resources required by the primary virtual resource.</param>
        /// <param name="utcStart">The UTC time at which the virtual resources were read.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Caching.CacheDependency"/> object for the specified virtual resources.
        /// </returns>
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (IsMasterLayoutView(virtualPath))
            {
                return new CacheDependency(TransformVirtualPathToAbsolute(virtualPath), utcStart);
            }
            else
            {
                return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
            }
        }
    }
}
