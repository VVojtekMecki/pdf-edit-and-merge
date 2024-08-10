using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using PDF_Edit_And_Merge.Model;
using PdfSharp.Pdf.IO;

namespace PDF_Edit_And_Merge.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<FileItem> fileList;
        [ObservableProperty]
        double pagesCount = 0;
        public MainPageViewModel()
        {
            FileList = [];
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
