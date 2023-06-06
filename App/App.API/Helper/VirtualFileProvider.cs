
namespace App.API.Helper
{
    public interface IVirtualFileProvider
    {
        string MapPath(string path);
    }
    public class VirtualFileProvider : IVirtualFileProvider
    {

        // Store dependencies
        private readonly string _rootPath;

        // Map virtual directories
        private readonly Dictionary<string, string> _virtualDirectories = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { };
        public VirtualFileProvider(string physicalFilesUploadPath)
        {

            _virtualDirectories.Add("FilesUpload", physicalFilesUploadPath);
        }
        //public VirtualFileProvider(string physicalFilesUploadPath, bool isDevelopmentEnvironment, string rootPath)
        //{

        //    _rootPath = rootPath;
        //    if (!isDevelopmentEnvironment) _virtualDirectories.Add("FilesUpload", physicalFilesUploadPath);
        //}

        public string MapPath(string path)
        {

            // Validate path
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"path shouldn't be null or empty");
            }
            // Validate path
            if (!path.StartsWith("/", StringComparison.Ordinal))
            {
                path = @"/" + path;
                //throw new ArgumentException($"The '{path}' should be root relative, and start with a '/'.");
            }
            // Translate path to UNC format
            path = path.Replace("/", @"\", StringComparison.Ordinal);

            // Isolate first folder (or file)
            var firstFolder = path.IndexOf(@"\", 1);
            if (firstFolder < 0)
            {
                firstFolder = path.Length;
            }

            // Parse root directory from remainder of path
            var rootDirectory = path.Substring(1, firstFolder - 1);
            var relativePath = path.Substring(firstFolder);

            // Return virtual directory
            if (_virtualDirectories.ContainsKey(rootDirectory))
            {
                return _virtualDirectories[rootDirectory] + relativePath;
            }
            // Return non-virtual directory
            return _rootPath + @"\" + rootDirectory + relativePath;

        }

    }
}
