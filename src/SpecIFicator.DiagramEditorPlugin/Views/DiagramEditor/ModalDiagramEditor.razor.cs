using MDD4All.SpecIF.ViewModels;
using Microsoft.AspNetCore.Components;

namespace SpecIFicator.DiagramEditorPlugin.Views.DiagramEditor
{
    public partial class ModalDiagramEditor
    {
        [CascadingParameter]
        public HierarchyViewModel DataContext { get; set; }

        private void OnEditorClose()
        {
            
            DataContext.CancelEditResourceCommand.Execute(null);
            
            StateHasChanged();
        }
    }
}