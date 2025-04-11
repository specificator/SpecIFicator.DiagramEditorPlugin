using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor.PropertyPanels
{
    public partial class DiagramPropertyPanel
    {
        [Parameter]
        public ShapeDiagramViewModel DataContext { get; set; }
    }
}