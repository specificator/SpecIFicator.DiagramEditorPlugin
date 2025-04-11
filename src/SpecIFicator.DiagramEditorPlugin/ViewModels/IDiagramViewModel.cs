using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels
{
    public interface IDiagramViewModel
    {
        GraphicalObjectViewModel? SelectedElement { get; set; }

        List<GraphicalObjectViewModel> DiagramObjects { get; set; }
    }
}
