using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor.Toolbox
{
    public partial class ToolboxLabel
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string IconClass { get; set; }

        [Parameter]
        public ShapeDiagramViewModel DataContext { get; set; }

        private void OnDragStart(DragEventArgs dragEventArgs)
        {
            DataContext.ToolboxItemType = Title;
        }



    }
}