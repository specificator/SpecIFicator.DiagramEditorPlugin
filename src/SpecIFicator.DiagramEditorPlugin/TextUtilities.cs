using System.Drawing;

namespace SpecIFicator.DiagramEditorPlugin
{
    internal class TextUtilities
    {
        public static SizeF GetTextDimension(string text, int fontSize = 10)
        {
            Font font = new Font("Helvetica Neue", fontSize, FontStyle.Regular);

            //create a bmp / graphic to use MeasureString on
            Bitmap b = new Bitmap(2200, 2200);
            Graphics g = Graphics.FromImage(b);

            //measure the string
            SizeF sizeOfString = new SizeF();
            sizeOfString = g.MeasureString(text, font);

            return sizeOfString;
        }
    }
}
