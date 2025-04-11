using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor.PropertyPanels
{
    public partial class BaseShapePropertyRows
    {
        [Parameter]
        public GraphicalShapeObjectViewModel DataContext { get; set; }
    }
}