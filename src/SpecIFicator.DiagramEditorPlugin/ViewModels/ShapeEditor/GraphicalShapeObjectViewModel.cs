using MDD4All.SpecIF.DataModels.DiagramMetadata;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor
{
    public abstract class GraphicalShapeObjectViewModel : GraphicalObjectViewModel
    {
        protected GraphicalShapeObjectViewModel(ShapePrimitive shapePrimitive) 
        { 
            ShapePrimitive = shapePrimitive;
        }

        public ShapePrimitive ShapePrimitive { get; protected set; }

        public override double X
        {
            get
            {
                return ShapePrimitive.X;
            }

            set
            {
                ShapePrimitive.X = value;
            }

        }

        public override double Y
        {
            get
            {
                return ShapePrimitive.Y;
            }

            set
            {
                ShapePrimitive.Y = value;
            }
        }

        public override double Width
        {
            get
            {

                return ShapePrimitive.Width;
            }

            set
            {
                ShapePrimitive.Width = value;
            }
        }

        public override double Height
        {
            get
            {
                return ShapePrimitive.Height;
            }
            set
            {
                ShapePrimitive.Height = value;
            }
        }


        public string Style
        {
            get
            {
                string result = "";

                if (!string.IsNullOrEmpty(ShapePrimitive.Style))
                {
                    result += ShapePrimitive.Style;
                }

                return result;
            }

            set
            {
                ShapePrimitive.Style = value;
            }
        }

        public IDiagramViewModel Parent { get; set; }

        
    }
}
