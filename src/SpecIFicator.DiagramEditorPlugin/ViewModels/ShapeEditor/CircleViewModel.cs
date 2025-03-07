namespace SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor
{
    public class CircleViewModel : GraphicalObjectViewModel
    {

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




    } // Ende der Klasse -> CircleViewModel


} // Ende Namespace
