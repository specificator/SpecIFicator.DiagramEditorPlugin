using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor.Panels
{
    public partial class DiagramObjectClassPanel
    {
        [Parameter]
        public EditorViewModel DataContext { get; set; }

        [Parameter]
        public EventCallback ClosedOkCallback { get; set; }
    }
}