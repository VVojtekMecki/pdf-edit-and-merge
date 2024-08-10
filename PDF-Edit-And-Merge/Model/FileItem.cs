using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_Edit_And_Merge.Model
{
    public class FileItem
    {
        private readonly FileResult _fileResult;
        private readonly string _contentType;
        public string Name { get; set; }
        public string Path { get; set; }
        public FileItem(FileResult file)
        {
            _fileResult = file;
            Name = file.FileName;
            Path = file.FullPath;
            _contentType = file.ContentType;
        }
    }
}
