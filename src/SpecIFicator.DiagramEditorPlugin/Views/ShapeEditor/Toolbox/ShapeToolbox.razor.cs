using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor.Toolbox
{
    public partial class ShapeToolbox
    {
        [Parameter]
        public ShapeDiagramViewModel DataContext { get; set; }
    }
}