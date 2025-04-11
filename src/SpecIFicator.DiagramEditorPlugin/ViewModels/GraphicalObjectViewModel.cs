using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels
{
    public abstract class GraphicalObjectViewModel
    {
        public abstract double X {  get; set; }

        public abstract double Y { get; set; }

        public abstract double Width { get; set; }

        public abstract double Height { get; set; }
        
        public double OffsetX { get; set; }

        public double OffsetY { get; set; }

        public bool IsSelected { get; set; } = false;

        public EditState State { get; set; } = EditState.None;

        public MarkerPosition MarkerPosition { get; set; } = MarkerPosition.None;

        public IDiagramViewModel Parent { get; set; }
    }
}
