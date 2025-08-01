using MDD4All.SpecIF.DataProvider.Contracts;
using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor;
using System.ComponentModel;

namespace SpecIFicator.DiagramEditorPlugin.Views.DiagramEditor
{
    public partial class DiagramEditor
    {
        [Parameter]
        public DiagramEditorViewModel DataContext { get; set; }

        protected override void OnInitialized()
        {
            DataContext.Pages.CollectionChanged += OnPagesCollectionChanged;
            DataContext.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        private void OnPagesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            StateHasChanged();
        }
    }
}