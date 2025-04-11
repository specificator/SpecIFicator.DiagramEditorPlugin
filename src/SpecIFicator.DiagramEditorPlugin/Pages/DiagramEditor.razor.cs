using MDD4All.SpecIF.DataProvider.Contracts;
using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Pages
{
    public partial class DiagramEditor
    {
        [Inject]
        private ISpecIfDataProviderFactory DataProviderFactory { get; set; }

        public EditorViewModel DataContext { get; set; }

        protected override void OnInitialized()
        {
            DataContext = new EditorViewModel(DataProviderFactory.MetadataWriter,
                                              DataProviderFactory.MetadataReader);


            DataContext.PropertyChanged += OnDataContextPropertyChanged;
        }

        private void OnDataContextPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }
    }
}
