using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor.PropertyPanels
{
    public partial class RectanglePropertyPanel
    {
        [Parameter]
        public RectangleViewModel DataContext { get; set; }
    }
}