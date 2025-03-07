namespace SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor
{
    public abstract class GraphicalObjectViewModel
    {
        public double X { get; set; } //X -Koordinate vom Objekt

        public double Y { get; set; }

        public double Z { get; set; }
        public virtual double Width { get; set; }

        public virtual double Height { get; set; }

        public double OffsetX { get; set; }

        public double OffsetY { get; set; }

        public bool IsSelected { get; set; } = false;

        public EditState State { get; set; } = EditState.None;

        public MarkerPosition MarkerPosition { get; set; } = MarkerPosition.None;

        public DiagramViewModel Parent { get; set; }
    }
}
