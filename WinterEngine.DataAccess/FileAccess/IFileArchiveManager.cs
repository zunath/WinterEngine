using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinterEngine.DataAccess.FileAccess
{
    public interface IFileArchiveManager
    {
        void ArchiveDirectory(string inputDirectoryPath, string outputDirectoryPath);
        void ExtractArchive(string inputArchivePath, string outputDirectoryPath);
        string CreateUniqueDirectory();
        string GenerateUniqueFileName(string path);
    }
}
