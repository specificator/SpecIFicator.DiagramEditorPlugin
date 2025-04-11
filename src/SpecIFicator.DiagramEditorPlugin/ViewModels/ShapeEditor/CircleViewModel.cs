using MDD4All.SpecIF.DataModels.DiagramMetadata;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor
{
    public class CircleViewModel : GraphicalShapeObjectViewModel
    {

        public CircleViewModel() : base(new CircleShapePrimitive())
        {
            Style = "stroke:black;fill:white;";
        }

        public CircleViewModel(CircleShapePrimitive shape) : base(shape)
        {
        }


        public override double Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                base.Height = value; // Sicherstellen, dass Höhe = Breite bleibt
            }
        }

        public override double Height
        {
            get => base.Height;
            set
            {
                base.Height = value;
                base.Width = value; // Sicherstellen, dass Breite = Höhe bleibt
            }
        }




    }
}