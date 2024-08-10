using PDF_Edit_And_Merge.ViewModel;

namespace PDF_Edit_And_Merge
{
    public partial class MainPage : ContentPage
    {
        private double headingHeight = 50;
        private double upperActionsRowHeight = 50;
        private double paddingValue = 20;
        private readonly MainPageViewModel viewModel;

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            BindingContext = vm;
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            var contentHeight = height - headingHeight - upperActionsRowHeight - 2 * paddingValue;
            ContentGrid.Padding = paddingValue;
            ContentGrid.RowDefinitions = [
                new RowDefinition(headingHeight),
                new RowDefinition(upperActionsRowHeight),
                new RowDefinition(contentHeight*0.8),
                new RowDefinition(contentHeight*0.2)
                ];
        }

    }

}
