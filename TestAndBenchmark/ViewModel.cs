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

    public partial class ViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ResultViewModel> results = new ObservableCollection<ResultViewModel>();
    }
}
