using System.Diagnostics;
using System.Reflection;

namespace WebApi
{
    public class ProductInfo
    {
        public string Application { get; set; }
        public string Version { get; set; }
        public string ImageRuntimeVersion { get; set; }
        public string ProductVersion { get; set; }
        public string ProcessName { get; set; }

        public static ProductInfo GetProductInfo()
        {
            var assembly = Assembly.GetEntryAssembly();
            return new ProductInfo
            {
                Application = "Sat",
                Version = assembly!.GetName().Version!.ToString(),
                ImageRuntimeVersion = assembly.ImageRuntimeVersion,
                ProductVersion = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion,
                ProcessName = assembly.GetName().Name
            };
        }
    }
}