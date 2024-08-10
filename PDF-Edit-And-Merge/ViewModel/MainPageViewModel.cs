using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using PDF_Edit_And_Merge.Model;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PDF_Edit_And_Merge.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private CancellationTokenSource cancellationTokenSource;
        [ObservableProperty]
        ObservableCollection<FileItem> fileList;
        [ObservableProperty]
        double pagesCount = 0;
        public MainPageViewModel()
        {
            FileList = [];
            cancellationTokenSource = new CancellationTokenSource();
        }

        [RelayCommand]
        public async Task AddPdfFiles()
        {

            var fileResults = await PickPdfFilesFromSystem();
            foreach (var file in fileResults)
            {
                AddPdfFile(file);
            }
        }

        [RelayCommand]
        public void ClearAll()
        {
            FileList.Clear();
            PagesCount = 0;
        }

        [RelayCommand]
        public async Task MergeFilesIntoOnePdf()
        {
            var outputPdf = new PdfDocument();
            foreach (var file in FileList)
            {
                var doc = file.GetPdfDocument;
                foreach (var page in doc.Pages)
                {
                    outputPdf.AddPage(page);
                }
            }
            var name = "tempfile.pdf";
            var folder = await PickFolderDestination();
            //add default folder path
            var defaultPath = "C:\\Users\\wojmed\\Desktop";
            if (folder?.Folder != null) 
            {
                defaultPath = folder.Folder.Path;
            }
            var filename = defaultPath + "\\" + name;
            outputPdf.Save(filename);
            ClearAll();
        }

        private void AddPdfFile(FileResult? file)
        {
            if (file != null)
            {
                if (file.ContentType == "application/pdf")
                {
                    FileList.Add(new FileItem(file));
                    PagesCount = FileList.Select(f => f.Pages).Sum();
                }
            }
        }

        private async Task<FolderPickerResult?> PickFolderDestination()
        {
            try
            {
                var result = await FolderPicker.Default.PickAsync(cancellationTokenSource.Token);
                result.EnsureSuccess();
                return result;
            }
            catch (Exception ex) { return null; }
        }

        private async Task<IEnumerable<FileResult?>> PickPdfFilesFromSystem()
        {

            try
            {
                var pickerOptions = new PickOptions
                {
                    FileTypes = FilePickerFileType.Pdf
                };
                return await FilePicker.Default.PickMultipleAsync(pickerOptions);
            }
            catch (Exception ex)
            {
                return [];
            }
        }
    }
}
