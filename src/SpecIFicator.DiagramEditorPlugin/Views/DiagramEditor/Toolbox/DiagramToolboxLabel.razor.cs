using MDD4All.UI.BlazorComponents.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.DiagramEditor.Toolbox
{
    public partial class DiagramToolboxLabel
    {
        [Inject]
        public DragDropDataProvider DragDropDataProvider { get; set; }

        [Parameter]
        public DiagramObjectClassViewModel DataContext { get; set; }

        private void OnDragStart(DragEventArgs dragEventArgs)
        {
            DragDropDataProvider.SetData(DataContext.Key);
        }
    }
}