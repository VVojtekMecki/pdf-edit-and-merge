using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_Edit_And_Merge.Model
{
    public class FileItem
    {
        private readonly PdfDocument _pdfDocument;
        private readonly FileResult _fileResult;
        public string Name { get; }
        public string Path { get; }
        public double Pages { get; }
        public string ContentType { get; }
        public FileItem(FileResult file)
        {
            _fileResult = file;
            Name = file.FileName;
            Path = file.FullPath;
            _pdfDocument = PdfReader.Open(Path);
            ContentType = file.ContentType;
            Pages = _pdfDocument.Pages.Count;
        }
    }
}
