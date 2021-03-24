using System.IO;
using System.Linq;
using System.Text;

namespace ErLiang.Test.Helper
{
    public class FileHelper
    {
        public static string ReadJsonFromFile(string fileName)
        {
            string result = string.Empty;
            string text = FindJsonFilePath(fileName);
            if (!string.IsNullOrEmpty(text) && File.Exists(text))
            {
                result = File.ReadAllText(text, Encoding.UTF8);
            }

            return result;
        }

        private static string FindJsonFilePath(string filename)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = currentDirectory + "\\DataPrepare\\";
            string[] files = Directory.GetFiles(path, filename, SearchOption.AllDirectories);
            if (files != null && files.Count() > 0)
            {
                return files.FirstOrDefault();
            }

            return string.Empty;
        }

        public static void WriteJson2File(string fileName, string json)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo parent = Directory.GetParent(currentDirectory);
            string path = parent.Parent.FullName + "\\DataPrepare\\" + fileName;
            if (!File.Exists(path))
            {
                File.WriteAllText(path, json);
            }
        }
    }
}
