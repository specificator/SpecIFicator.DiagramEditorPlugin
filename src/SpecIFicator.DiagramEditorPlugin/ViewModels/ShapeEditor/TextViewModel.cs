using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.Text.DataModels;
using System.Drawing;
using TextDataModels = MDD4All.Text.DataModels;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor
{
    public class TextViewModel : GraphicalShapeObjectViewModel
    {
        public TextViewModel() : base(new TextShapePrimitive())
        {
        }

        public TextViewModel(TextShapePrimitive textShapePrimitive) : base(textShapePrimitive) 
        {
        }

        public TextShapePrimitive TextShapePrimitive
        {
            get
            {
                return ShapePrimitive as TextShapePrimitive;
            }
        }

        public string Value
        {
            get
            {
                return TextShapePrimitive.Value;
            }

            set
            {
                TextShapePrimitive.Value = value;
            }
        }

        public int FontSize 
        { 
            get
            {
                return TextShapePrimitive.FontSize;
            }

            set
            {
                TextShapePrimitive.FontSize = value;
            }
        }

        public TextDataModels.FontFamily FontFamily
        {
            get
            {
                return TextShapePrimitive.FontFamily;
            }

            set
            {
                TextShapePrimitive.FontFamily = value;
            }
        }

        public string SvgFontFamily
        {
            get
            {
                string result = "";

                switch(FontFamily)
                {
                    case TextDataModels.FontFamily.Serif:
                        result = "serif";
                        break;

                    case TextDataModels.FontFamily.SansSerif:
                        result = "sans-serif";
                        break;

                    case TextDataModels.FontFamily.Monospace:
                        result = "monospace";
                        break;
                }

                return result;
            }
        }

        public TextDataModels.FontStyle FontStyle
        {
            get
            {
                return TextShapePrimitive.FontStyle;
            }

            set
            {
                TextShapePrimitive.FontStyle = value;
            }
        }

        public TextDataModels.FontWeight FontWeight
        {
            get
            {
                return TextShapePrimitive.FontWeight;
            }

            set
            {
                TextShapePrimitive.FontWeight = value;
            }
        }

        public string FontColor
        {
            get
            {
                return TextShapePrimitive.FontColor;
            }

            set
            {
                TextShapePrimitive.FontColor = value;
            }
        }

        public HorizontalTextAlignment HorizontalAlignment
        {
            get
            {
                return TextShapePrimitive.HorizontalAlignment;
            }
            set
            {
                TextShapePrimitive.HorizontalAlignment = value;
            }
        }

        public VerticalTextAlignment VerticalAlignment
        {
            get
            {
                return TextShapePrimitive.VerticalAlignment;
            }
            set
            {
                TextShapePrimitive.VerticalAlignment = value;
            }
        }

                
        

        public int TextX 
        { 
            get 
            {
                int result = 0;

                SizeF dimension = TextUtilities.GetTextDimension(Value, FontSize);

                switch (HorizontalAlignment)
                {
                    case HorizontalTextAlignment.Left:
                        result = (int)X; 
                        break;

                    case HorizontalTextAlignment.Right:
                        result = (int)(X + Width - dimension.Width);
                        break;

                    case HorizontalTextAlignment.Center:
                    default:
                        result = (int)(X + Width / 2 - dimension.Width / 3);
                        break;
                }

                return result;
            } 
        }

        public int TextY
        {
            get
            {
                int result = 0;

                SizeF dimension = TextUtilities.GetTextDimension(Value, FontSize);

                switch(VerticalAlignment)
                {
                    case VerticalTextAlignment.Top:
                        result = (int)(Y + dimension.Height);
                        break;

                    case VerticalTextAlignment.Bottom:
                        result = (int)(Y + Height);
                        break;

                    case VerticalTextAlignment.Center:
                    default:
                        result = (int)(Y + Height / 2 + dimension.Height / 3);
                        break;
                }
               

                return result;
            }
        }

    }
}
