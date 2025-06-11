using MDD4All.SpecIF.DataProvider.Contracts;
using MDD4All.SpecIF.ViewModels;
using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.DiagramEditor
{
    public partial class ModalDiagramEditor
    {
        [Inject]
        public ISpecIfDataProviderFactory DataProviderFactory { get; set; }

        [CascadingParameter]
        public HierarchyViewModel DataContext { get; set; }

        private DiagramEditorViewModel DiagramEditorViewModel { get; set; }

        protected override void OnInitialized()
        {
            
            DiagramEditorViewModel = new DiagramEditorViewModel(DataProviderFactory, DataContext);
           
        }

        private void OnEditorClose()
        {
            
            DataContext.CancelEditResourceCommand.Execute(null);
            
            StateHasChanged();
        }
    }
}