using MDD4All.SpecIF.DataModels.DiagramMetadata;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor
{
    public class EllipseViewModel : GraphicalShapeObjectViewModel
    {
        public EllipseViewModel() : base(new EllipseShapePrimitive())
        {
            Style = "fill:white; stroke:black;";
        }

        public EllipseViewModel(ShapePrimitive shapePrimitive) : base(shapePrimitive)
        {
        }
    }
}

