using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mars.Framework.IO
{
    public class ZipExtractor
    {
        public static bool Extract(string zipFile, string distFolder, Regex fileRegex, bool overwrite = true)
        {
            return ZipExtractor.ExtractAsync(zipFile, distFolder, fileRegex, overwrite).GetAwaiter().GetResult();
        }

        public static async Task<bool> ExtractAsync(string zipFile, string distFolder, Regex fileRegex, bool overwrite = true)
        {
            if (!File.Exists(zipFile))
            {
                throw new FileNotFoundException();
            }

            using(var sourceMemoryStream = new MemoryStream())
            {
                using(var sourceZipFile = new FileStream(zipFile, FileMode.Open))
                {
                    await sourceZipFile.CopyToAsync(sourceMemoryStream);
                }

                using(var sourceArchive = new ZipArchive(sourceMemoryStream, ZipArchiveMode.Read))
                {
                    foreach (var entry in sourceArchive.Entries)
                    {
                        if (!fileRegex.IsMatch(entry.FullName))
                        {
                            continue;
                        }

                        var destinationPath = Path.GetFullPath(Path.Combine(distFolder, entry.FullName));
                        var folder = Path.GetDirectoryName(destinationPath);
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }

                        await Task.Run(() => entry.ExtractToFile(destinationPath, true));
                    }
                }
            }

            return true;
        }
    }
}
