using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor
{
    public partial class ShapeToolbox
    {
        [Parameter]
        public DiagramViewModel DataContext { get; set; }
    }
}