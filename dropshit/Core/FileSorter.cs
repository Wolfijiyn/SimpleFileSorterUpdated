using System;
using System.IO;

namespace dropshit.Core
{
    class FileSorter
    {
        public FileSorter() { }

        public int Sort(string[] files)
        {
            int notCounted = 0;

            foreach (string filePath in files)
            {
                if (Path.GetExtension(filePath) != "")
                {
                    string folderName = Path.GetExtension(filePath).Replace(".", "").ToUpper() + "S";
                    notCounted = notCounted + Sort(Path.GetExtension(filePath), folderName, filePath);
                }
            }

            return notCounted;
        }

        public int Sort(string ext, string folderName, string fileName)
        {
            try
            {
                string _fileName = Path.GetFileName(fileName);

                string _folderPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                string combinedPaths = System.IO.Path.Combine(_folderPath, folderName);

                string targetPath = System.IO.Path.Combine(combinedPaths, _fileName);

                if (!Directory.Exists(combinedPaths))
                    System.IO.Directory.CreateDirectory(combinedPaths);

                if (File.Exists(targetPath))
                    return 1;

                if (!File.Exists(targetPath))
                {
                    System.IO.File.Move(fileName, targetPath);
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;
        }
    }
}
