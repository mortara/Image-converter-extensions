using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
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

        public String ShortLabel => $"{Descriptor.Method.Name} ({Descriptor.Attribute.Kind})";

        [ObservableProperty]
        private bool isSelected;
    }

    /// <summary>
    /// One matrix cell: every discovered converter whose source/target format
    /// matches this cell's row/column (zero, one, or several).
    /// </summary>
    public sealed class ConverterMatrixCellViewModel
    {
        public ConverterMatrixCellViewModel(IEnumerable<ConverterSelectionViewModel> converters)
        {
            Converters = new ObservableCollection<ConverterSelectionViewModel>(converters);
        }

        public ObservableCollection<ConverterSelectionViewModel> Converters { get; }
    }

    /// <summary>One matrix row: a target format plus its cell per source format.</summary>
    public sealed class ConverterMatrixRowViewModel
    {
        public ConverterMatrixRowViewModel(String targetFormatLabel, IEnumerable<ConverterMatrixCellViewModel> cells)
        {
            TargetFormatLabel = targetFormatLabel;
            Cells = new ObservableCollection<ConverterMatrixCellViewModel>(cells);
        }

        public String TargetFormatLabel { get; }

        public ObservableCollection<ConverterMatrixCellViewModel> Cells { get; }
    }

    public partial class ViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ResultViewModel> results = new ObservableCollection<ResultViewModel>();

        [ObservableProperty]
        private ObservableCollection<ConverterSelectionViewModel> availableConverters = new ObservableCollection<ConverterSelectionViewModel>();

        [ObservableProperty]
        private ObservableCollection<String> matrixSourceFormats = new ObservableCollection<String>();

        [ObservableProperty]
        private ObservableCollection<ConverterMatrixRowViewModel> matrixRows = new ObservableCollection<ConverterMatrixRowViewModel>();

        [ObservableProperty]
        private bool overlayTestText;
    }
}
