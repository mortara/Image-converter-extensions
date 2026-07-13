using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.ObjectModel;


namespace TestAndBenchmark
{
    public partial class ResultViewModel : ObservableObject
    {
        [ObservableProperty]
        private String text = String.Empty;

        [ObservableProperty]
        private BitmapSource image = null;
    }

    public partial class ConverterSelectionViewModel : ObservableObject
    {
        public ConverterSelectionViewModel(ConverterDescriptor descriptor)
        {
            Descriptor = descriptor;
        }

        public ConverterDescriptor Descriptor { get; }

        public String DisplayName => Descriptor.DisplayName;

        [ObservableProperty]
        private bool isSelected;
    }

    public partial class ViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ResultViewModel> results = new ObservableCollection<ResultViewModel>();

        [ObservableProperty]
        private ObservableCollection<ConverterSelectionViewModel> availableConverters = new ObservableCollection<ConverterSelectionViewModel>();

        [ObservableProperty]
        private bool overlayTestText;
    }
}
