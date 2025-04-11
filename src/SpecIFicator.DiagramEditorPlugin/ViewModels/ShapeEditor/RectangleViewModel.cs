using MDD4All.SpecIF.DataModels.DiagramMetadata;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor
{
    public class RectangleViewModel : GraphicalShapeObjectViewModel
    {
        public RectangleViewModel() : base(new RectangleShapePrimitive())
        {
            Style = "stroke:black;fill:white;";
        }

        public RectangleViewModel(RectangleShapePrimitive shape) : base(shape) 
        { 
        }

        public RectangleShapePrimitive RectangleShapePrimitive
        {
            get
            {
                return ShapePrimitive as RectangleShapePrimitive;
            }
        }

        public int HorizontalRadius
        {
            get
            {
                return RectangleShapePrimitive.HorizontalRadius;
            }

            set
            {
                RectangleShapePrimitive.HorizontalRadius = value;
            }
        }

        public int VerticalRadius
        {
            get
            {
                return RectangleShapePrimitive.VerticalRadius;
            }

            set
            {
                RectangleShapePrimitive.VerticalRadius = value;
            }
        }
    }
}
