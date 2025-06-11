using MDD4All.SpecIF.DataModels.DiagramInterchange.DiagramDefinition;
using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.SpecIF.DataModels.Manipulation;
using MDD4All.SpecIF.DataProvider.Contracts;
using MDD4All.SVG.Conversion;
using MDD4All.SVG.DataModels;
using MDD4All.Text.DataModels;
using MDD4All.Xml.DataAccess;
using SpecIFicator.DiagramEditorPlugin.ViewModels;
using SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor;
using System.Globalization;

namespace SpecIFicator.DiagramEditorPlugin.Converters
{
    internal class ShapeToDiagramObjectConverter
    {
        private ISpecIfDataProviderFactory _dataProviderFactory;

        public ShapeToDiagramObjectConverter(ISpecIfDataProviderFactory specIfDataProviderFactory) 
        {
            _dataProviderFactory = specIfDataProviderFactory;
        }


        public string ConvertDiagramObjectToSVG(DiagramObjectClass diagramObjectClass,
                                                GraphicalObjectViewModel graphicalObject)
        {
            string result = string.Empty;

            Bounds bounds = CalculateBounds(diagramObjectClass);

            double xFactor = graphicalObject.Width / (bounds.Width);
            double yFactor = graphicalObject.Height / bounds.Height;

            Group group = new Group();

            foreach(ShapePrimitive shapePrimitive in diagramObjectClass.ShapePrimitives)
            {
                double x = graphicalObject.X + ((shapePrimitive.X - bounds.X) * xFactor);
                double y = graphicalObject.Y + ((shapePrimitive.Y - bounds.Y) * yFactor);

                double width = shapePrimitive.Width * xFactor;
                double height = shapePrimitive.Height * yFactor;


                if (shapePrimitive is RectangleShapePrimitive)
                {
                    RectangleShapePrimitive rectangleShapePrimitive = (RectangleShapePrimitive)shapePrimitive;

                    Rectangle svgRectangle = new Rectangle
                    {
                        X = x.ToString(CultureInfo.InvariantCulture),
                        Y = y.ToString(CultureInfo.InvariantCulture),
                        Width = width.ToString(CultureInfo.InvariantCulture),
                        Height = height.ToString(CultureInfo.InvariantCulture),
                        VerticalCornerRadius = rectangleShapePrimitive.VerticalRadius.ToString(CultureInfo.InvariantCulture),
                        HorizontalCornerRadius = rectangleShapePrimitive.HorizontalRadius.ToString(CultureInfo.InvariantCulture),
                        CssStyle = shapePrimitive.Style
                    };
                    group.Elements.Add(svgRectangle);
                }
                else if (shapePrimitive is CircleShapePrimitive)
                {
                    double radius = Math.Min(width / 2, height / 2);
                    Circle svgCircle = new Circle
                    {
                        Cx = (x + width / 2).ToString(CultureInfo.InvariantCulture),
                        Cy = (y + height / 2).ToString(CultureInfo.InvariantCulture),
                        Radius = radius.ToString(CultureInfo.InvariantCulture),
                        CssStyle = shapePrimitive.Style
                    };
                    group.Elements.Add(svgCircle);
                }
                else if (shapePrimitive is EllipseShapePrimitive)
                {
                    Ellipse svgEllipse = new Ellipse
                    {
                        Cx = (x + width / 2).ToString(CultureInfo.InvariantCulture),
                        Cy = (y + height / 2).ToString(CultureInfo.InvariantCulture),
                        RadiusX = (width / 2).ToString(CultureInfo.InvariantCulture),
                        RadiusY = (height / 2).ToString(CultureInfo.InvariantCulture),
                        CssStyle = shapePrimitive.Style
                    };
                    group.Elements.Add(svgEllipse);
                }
                else if (shapePrimitive is TextShapePrimitive)
                {
                    if(graphicalObject is DiagramObjectViewModel)
                    {
                        DiagramObjectViewModel diagramObjectViewModel = (DiagramObjectViewModel)graphicalObject;

                        
                        Group svgText = ConvertTextPrimitive(shapePrimitive as TextShapePrimitive,
                                                             graphicalObject,
                                                             x, y, width, height);

                        // avoid text selection and pointer events
                        foreach (PresentationElement element in svgText.Elements)
                        {
                            if(element is Text)
                            {
                                Text text = (Text)element;
                                text.CssStyle = text.CssStyle + " pointer-events: none; user-select:none;";
                            }
                        }

                        group.Elements.Add(svgText);
                        
                    }

                    
                }
            }

            result = group.SerializeToXml();

            return result;
        }

        private Group ConvertTextPrimitive(TextShapePrimitive shapePrimitive, 
                                          GraphicalObjectViewModel graphicalObjectViewModel,
                                          double x,
                                          double y,
                                          double width,
                                          double height)
        {
            Group result = new Group();

            string textToDisplay = string.Empty;

            if (graphicalObjectViewModel is DiagramObjectViewModel)
            {
                DiagramObjectViewModel diagramObjectViewModel = graphicalObjectViewModel as DiagramObjectViewModel;

                string metaText = shapePrimitive.Value;

                if (!string.IsNullOrEmpty(metaText))
                {
                    bool inVariable = false;

                    List<TextToken> tokens = new List<TextToken>();

                    TextToken currentToken = new TextToken();

                    for (int index = 0; index < metaText.Length; index++)
                    {
                        char tokenChar = metaText[index];
                        if(tokenChar == '#')
                        {
                            if (!inVariable)
                            {
                                inVariable = true;
                                tokens.Add(currentToken);
                                currentToken = new TextToken();
                                currentToken.IsVariable = true;
                            }
                            else
                            {
                                inVariable = false;
                                tokens.Add(currentToken);
                                currentToken = new TextToken();
                            }
                        }
                        else
                        {
                            currentToken.Value += tokenChar;
                        }
                    }

                    textToDisplay = CalculateDynamicText(tokens, diagramObjectViewModel);

                }
            }

            FontDescription font = CreateFontDescriptionFromShape(shapePrimitive);

            result = SvgConvert.CreateTextLabel(textToDisplay, 
                                                (int)width, 
                                                font, 
                                                shapePrimitive.VerticalAlignment, 
                                                shapePrimitive.HorizontalAlignment, 
                                                new System.Drawing.Point((int)x, (int)y), 
                                                new System.Drawing.Size((int)width, (int)height));


            return result;
        }

        private FontDescription CreateFontDescriptionFromShape(TextShapePrimitive shapePrimitive)
        {
            FontDescription result = new FontDescription()
            {
                FontSize = shapePrimitive.FontSize,
                FontWeight = shapePrimitive.FontWeight,
                FontFamily = shapePrimitive.FontFamily,
                FontStyle = shapePrimitive.FontStyle
            };

            

            return result;
        }

        private string CalculateDynamicText(List<TextToken> tokens, 
                                            DiagramObjectViewModel diagramObjectViewModel)
        {
            string result = string.Empty;

            foreach (TextToken token in tokens)
            {
                if (token.IsVariable && diagramObjectViewModel.MainResource != null)
                {
                    string propertyValue = diagramObjectViewModel.MainResource.Resource.GetPropertyValue(token.Value,
                                                                                  _dataProviderFactory.MetadataReader);
                    
                    result += propertyValue;
                }
                else
                {
                    result += token.Value;
                }

            }

            return result;
        }

        public Bounds CalculateBounds(DiagramObjectClass diagramObjectClass)
        {
            // define the return values
            Bounds result = new Bounds();
            
            double minX = double.MaxValue;
            double minY = double.MaxValue;
            double maxX = 0;
            double maxY = 0;

            foreach (ShapePrimitive shapePrimitive in diagramObjectClass.ShapePrimitives)
            {
                if (shapePrimitive.X < minX)
                {
                    minX = shapePrimitive.X;
                }
                if (shapePrimitive.Y < minY)
                {
                    minY = shapePrimitive.Y;
                }
                if (shapePrimitive.X + shapePrimitive.Width > maxX)
                {
                    maxX = shapePrimitive.X + shapePrimitive.Width;
                }
                if (shapePrimitive.Y + shapePrimitive.Height > maxY)
                {
                    maxY = shapePrimitive.Y + shapePrimitive.Height;
                }
            }

            result.X = minX;
            result.Y = minY;
            result.Width = maxX - minX;
            result.Height = maxY - minY;

            return result;
        }

         
    }
}
