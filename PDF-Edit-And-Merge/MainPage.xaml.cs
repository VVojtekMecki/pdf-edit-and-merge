using PDF_Edit_And_Merge.ViewModel;

namespace PDF_Edit_And_Merge
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel viewModel;

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            BindingContext = vm;
        }

    }

}
