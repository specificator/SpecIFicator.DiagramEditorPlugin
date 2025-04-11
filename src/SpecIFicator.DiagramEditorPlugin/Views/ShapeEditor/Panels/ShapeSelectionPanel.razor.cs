using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor.Panels
{
    public partial class ShapeSelectionPanel
    {
        [Parameter]
        public EditorViewModel DataContext { get; set; }
    }
}