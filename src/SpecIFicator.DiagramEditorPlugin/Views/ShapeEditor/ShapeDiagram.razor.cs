using MDD4All.UI.DataModels.TabControl;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SpecIFicator.DiagramEditorPlugin.ViewModels;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor
{
    public partial class ShapeDiagram
    {
        
        [CascadingParameter(Name = "DataContext")]
        public ITabPage TabPageContext
        {
            set
            {
                if(value is ShapeDiagramViewModel)
                {
                    DataContext = value as ShapeDiagramViewModel;
                }
            }
        }

        private ShapeDiagramViewModel _diagramViewModel;

        [Parameter]
        public ShapeDiagramViewModel DataContext
        {
            get
            {

                return _diagramViewModel;
            }

            set
            {
                _diagramViewModel = value;
            }
        }

        // Speicherung der Mauskoordinaten
        private double ClientX { get; set; } // Position relativ zum sichtbaren Bereich des Browsers

        private double ClientY { get; set; }

        private double OffsetX { get; set; } // Position relativ zum Element, auf das geklickt wurde

        private double OffsetY { get; set; }

        private double ScreenX { get; set; } // Absolute Bildschirmposition der Maus

        private double ScreenY { get; set; }

        private double PageX { get; set; } // Position relativ zur gesamten Webseite

        private double PageY { get; set; }

        private void OnDrop(DragEventArgs dragEventArgs)
        {

            string type = DataContext.ToolboxItemType;

            if (type != null)
            {
                if (type == "Rectangle")
                {
                    RectangleViewModel rectangleViewModel = new RectangleViewModel
                    {
                        X = dragEventArgs.OffsetX,
                        Y = dragEventArgs.OffsetY,
                        Height = 50,
                        Width = 100,
                        Parent = DataContext
                    };

                    DataContext.DiagramObjects.Add(rectangleViewModel);
                }
                else if (type == "Circle")
                {
                    CircleViewModel circleViewModel = new CircleViewModel
                    {
                        X = dragEventArgs.OffsetX,
                        Y = dragEventArgs.OffsetY,
                        Height = 50,
                        Width = 50,
                        Parent = DataContext
                    };

                    DataContext.DiagramObjects.Add(circleViewModel);

                    type = null;
                }
                else if (type == "Ellipse")
                {
                    EllipseViewModel ellipseViewModel = new EllipseViewModel
                    {
                        X = dragEventArgs.OffsetX,
                        Y = dragEventArgs.OffsetY,
                        Height = 50,
                        Width = 80,
                        Parent = DataContext
                    };

                    DataContext.DiagramObjects.Add(ellipseViewModel);
                }
                else if (type == "Text")
                {
                    TextViewModel textViewModel = new TextViewModel
                    {
                        X = dragEventArgs.OffsetX,
                        Y = dragEventArgs.OffsetY,
                        Height = 50,
                        Width = 150,
                        Parent = DataContext
                    };

                    DataContext.DiagramObjects.Add(textViewModel);
                }


                type = null;


            }


            StateHasChanged();
        }

        // **************************************************************

        private void OnMouseMove(MouseEventArgs arguments)
        {
            ClientX = arguments.ClientX;
            ClientY = arguments.ClientY;
            OffsetX = arguments.OffsetX;
            OffsetY = arguments.OffsetY;
            ScreenX = arguments.ScreenX;
            ScreenY = arguments.ScreenY;
            PageX = arguments.PageX;
            PageY = arguments.PageY;



            foreach (GraphicalShapeObjectViewModel graphicalObject in DataContext.DiagramObjects)
            {
                if (graphicalObject.State == EditState.Moving)
                {
                    graphicalObject.X = arguments.OffsetX - graphicalObject.OffsetX;
                    graphicalObject.Y = arguments.OffsetY - graphicalObject.OffsetY;

                }
                if (graphicalObject.State == EditState.Resizing)
                {

                    if (graphicalObject.MarkerPosition == MarkerPosition.TopMiddle)
                    {
                        double rectangleY = graphicalObject.Y;

                        if (graphicalObject.Y < arguments.OffsetY) // Maus wurde nach UNTEN bewegt
                        {
                            double distanceY = arguments.OffsetY - rectangleY;

                            graphicalObject.Y = rectangleY + distanceY;

                            graphicalObject.Height = graphicalObject.Height - distanceY;

                        }
                        else if (graphicalObject.Y > arguments.OffsetY) // Maus wurde nach OBEN bewegt
                        {
                            double distanceY = rectangleY - arguments.OffsetY;

                            graphicalObject.Y = rectangleY - distanceY;

                            graphicalObject.Height = graphicalObject.Height + distanceY;
                        }


                    }
                    // ***********************************************************

                    //else if() 

                    else if (graphicalObject.MarkerPosition == MarkerPosition.BottomMiddle)
                    {
                        {
                            double rectangleBottomY = graphicalObject.Y + graphicalObject.Height; // Unterkante des Rechtecks

                            if (rectangleBottomY < arguments.OffsetY) // Maus wurde nach UNTEN bewegt
                            {
                                double distanceY = arguments.OffsetY - rectangleBottomY;
                                graphicalObject.Height = graphicalObject.Height + distanceY;
                            }
                            else if (rectangleBottomY > arguments.OffsetY) // Maus wurde nach OBEN bewegt
                            {
                                double distanceY = rectangleBottomY - arguments.OffsetY;
                                graphicalObject.Height = graphicalObject.Height - distanceY;
                            }
                        }

                    }

                    // ***********************************************************

                    else if (graphicalObject.MarkerPosition == MarkerPosition.LeftTop)
                    {
                        double rectangleX = graphicalObject.X;
                        double rectangleY = graphicalObject.Y;

                        // X-Achse: Breite ändern, wenn die Maus nach links oder rechts bewegt wird
                        if (rectangleX > arguments.OffsetX) // Maus wurde nach LINKS bewegt
                        {
                            double distanceX = rectangleX - arguments.OffsetX;
                            graphicalObject.X = rectangleX - distanceX;
                            graphicalObject.Width = graphicalObject.Width + distanceX;
                        }
                        else if (rectangleX < arguments.OffsetX) // Maus wurde nach RECHTS bewegt
                        {
                            double distanceX = arguments.OffsetX - rectangleX;
                            graphicalObject.X = rectangleX + distanceX;
                            graphicalObject.Width = graphicalObject.Width - distanceX;
                        }

                        // Y-Achse: Höhe ändern, wenn die Maus nach oben oder unten bewegt wird
                        if (rectangleY > arguments.OffsetY) // Maus wurde nach OBEN bewegt
                        {
                            double distanceY = rectangleY - arguments.OffsetY;
                            graphicalObject.Y = rectangleY - distanceY;
                            graphicalObject.Height = graphicalObject.Height + distanceY;
                        }
                        else if (rectangleY < arguments.OffsetY) // Maus wurde nach UNTEN bewegt
                        {
                            double distanceY = arguments.OffsetY - rectangleY;
                            graphicalObject.Y = rectangleY + distanceY;
                            graphicalObject.Height = graphicalObject.Height - distanceY;
                        }
                    }

                    // ***********************************************************

                    else if (graphicalObject.MarkerPosition == MarkerPosition.RightTop)
                    {
                        double rectangleRightX = graphicalObject.X + graphicalObject.Width; // Rechte Kante des Rechtecks
                        double rectangleY = graphicalObject.Y;

                        // X-Achse: Breite ändern, wenn die Maus nach links oder rechts bewegt wird
                        if (rectangleRightX < arguments.OffsetX) // Maus wurde nach RECHTS bewegt (vergrößern)
                        {
                            double distanceX = arguments.OffsetX - rectangleRightX;
                            graphicalObject.Width = graphicalObject.Width + distanceX;
                        }
                        else if (rectangleRightX > arguments.OffsetX) // Maus wurde nach LINKS bewegt (verkleinern)
                        {
                            double distanceX = rectangleRightX - arguments.OffsetX;
                            graphicalObject.Width = graphicalObject.Width - distanceX;
                        }

                        // Y-Achse: Höhe ändern, wenn die Maus nach oben oder unten bewegt wird
                        if (rectangleY > arguments.OffsetY) // Maus wurde nach OBEN bewegt (vergrößern)
                        {
                            double distanceY = rectangleY - arguments.OffsetY;
                            graphicalObject.Y = rectangleY - distanceY;
                            graphicalObject.Height = graphicalObject.Height + distanceY;
                        }
                        else if (rectangleY < arguments.OffsetY) // Maus wurde nach UNTEN bewegt (verkleinern)
                        {
                            double distanceY = arguments.OffsetY - rectangleY;
                            graphicalObject.Y = rectangleY + distanceY;
                            graphicalObject.Height = graphicalObject.Height - distanceY;
                        }
                    }

                    // ***********************************************************

                    else if (graphicalObject.MarkerPosition == MarkerPosition.LeftBottom)
                    {
                        double rectangleX = graphicalObject.X;
                        double rectangleBottomY = graphicalObject.Y + graphicalObject.Height; // Unterkante des Rechtecks

                        // X-Achse: Breite ändern, wenn die Maus nach links oder rechts bewegt wird
                        if (rectangleX > arguments.OffsetX) // Maus wurde nach LINKS bewegt (vergrößern)
                        {
                            double distanceX = rectangleX - arguments.OffsetX;
                            graphicalObject.X = rectangleX - distanceX;
                            graphicalObject.Width = graphicalObject.Width + distanceX;
                        }
                        else if (rectangleX < arguments.OffsetX) // Maus wurde nach RECHTS bewegt (verkleinern)
                        {
                            double distanceX = arguments.OffsetX - rectangleX;
                            graphicalObject.X = rectangleX + distanceX;
                            graphicalObject.Width = graphicalObject.Width - distanceX;
                        }

                        // Y-Achse: Höhe ändern, wenn die Maus nach oben oder unten bewegt wird
                        if (rectangleBottomY < arguments.OffsetY) // Maus wurde nach UNTEN bewegt (vergrößern)
                        {
                            double distanceY = arguments.OffsetY - rectangleBottomY;
                            graphicalObject.Height = graphicalObject.Height + distanceY;
                        }
                        else if (rectangleBottomY > arguments.OffsetY) // Maus wurde nach OBEN bewegt (verkleinern)
                        {
                            double distanceY = rectangleBottomY - arguments.OffsetY;
                            graphicalObject.Height = graphicalObject.Height - distanceY;
                        }
                    }

                    // ***********************************************************

                    else if (graphicalObject.MarkerPosition == MarkerPosition.RightBottom)
                    {
                        double rectangleRightX = graphicalObject.X + graphicalObject.Width; // Rechte Kante des Rechtecks
                        double rectangleBottomY = graphicalObject.Y + graphicalObject.Height; // Unterkante des Rechtecks

                        // X-Achse: Breite ändern, wenn die Maus nach links oder rechts bewegt wird
                        if (rectangleRightX < arguments.OffsetX) // Maus wurde nach RECHTS bewegt (vergrößern)
                        {
                            double distanceX = arguments.OffsetX - rectangleRightX;
                            graphicalObject.Width = graphicalObject.Width + distanceX;
                        }
                        else if (rectangleRightX > arguments.OffsetX) // Maus wurde nach LINKS bewegt (verkleinern)
                        {
                            double distanceX = rectangleRightX - arguments.OffsetX;
                            graphicalObject.Width = graphicalObject.Width - distanceX;
                        }

                        // Y-Achse: Höhe ändern, wenn die Maus nach oben oder unten bewegt wird
                        if (rectangleBottomY < arguments.OffsetY) // Maus wurde nach UNTEN bewegt (vergrößern)
                        {
                            double distanceY = arguments.OffsetY - rectangleBottomY;
                            graphicalObject.Height = graphicalObject.Height + distanceY;
                        }
                        else if (rectangleBottomY > arguments.OffsetY) // Maus wurde nach OBEN bewegt (verkleinern)
                        {
                            double distanceY = rectangleBottomY - arguments.OffsetY;
                            graphicalObject.Height = graphicalObject.Height - distanceY;
                        }
                    }

                    // ***********************************************************

                    else if (graphicalObject.MarkerPosition == MarkerPosition.LeftMiddle)
                    {
                        double rectangleX = graphicalObject.X;

                        // X-Achse: Breite ändern, wenn die Maus nach links oder rechts bewegt wird
                        if (rectangleX > arguments.OffsetX) // Maus wurde nach LINKS bewegt (vergrößern)
                        {
                            double distanceX = rectangleX - arguments.OffsetX;
                            graphicalObject.X = rectangleX - distanceX;
                            graphicalObject.Width = graphicalObject.Width + distanceX;
                        }
                        else if (rectangleX < arguments.OffsetX) // Maus wurde nach RECHTS bewegt (verkleinern)
                        {
                            double distanceX = arguments.OffsetX - rectangleX;
                            graphicalObject.X = rectangleX + distanceX;
                            graphicalObject.Width = graphicalObject.Width - distanceX;
                        }
                    }

                    // ************************************************************

                    if (graphicalObject.MarkerPosition == MarkerPosition.RightMiddle)
                    {
                        double rectangleRightX = graphicalObject.X + graphicalObject.Width; // Rechte Kante des Rechtecks

                        // X-Achse: Breite ändern, wenn die Maus nach links oder rechts bewegt wird
                        if (rectangleRightX < arguments.OffsetX) // Maus wurde nach RECHTS bewegt (vergrößern)
                        {
                            double distanceX = arguments.OffsetX - rectangleRightX;
                            graphicalObject.Width = graphicalObject.Width + distanceX;
                        }
                        else if (rectangleRightX > arguments.OffsetX) // Maus wurde nach LINKS bewegt (verkleinern)
                        {
                            double distanceX = rectangleRightX - arguments.OffsetX;
                            graphicalObject.Width = graphicalObject.Width - distanceX;
                        }
                    }


                    // hier gehts weiter

                } // Ende von -> IF (rectangle.State == EditState.Resizing)


            }



            StateHasChanged();

        } // Ende der Methode -> OnMouseMove


        // Aktualisiert Mauskoordinaten
        private void UpdateMousePosition(MouseEventArgs arguments)
        {
            ClientX = arguments.ClientX;
            ClientY = arguments.ClientY;
            OffsetX = arguments.OffsetX;
            OffsetY = arguments.OffsetY;
            ScreenX = arguments.ScreenX;
            ScreenY = arguments.ScreenY;
            PageX = arguments.PageX;
            PageY = arguments.PageY;
        }

        // Bewegt das grafische Objekt
        private void MoveGraphicalObject(GraphicalShapeObjectViewModel graphicalObject, MouseEventArgs arguments)
        {
            graphicalObject.X = arguments.OffsetX - graphicalObject.OffsetX;
            graphicalObject.Y = arguments.OffsetY - graphicalObject.OffsetY;
        }

        // Ändert die Größe des grafischen Objekts
        private void ResizeGraphicalObject(GraphicalShapeObjectViewModel graphicalObject, MouseEventArgs arguments)
        {
            double offsetX = arguments.OffsetX;
            double offsetY = arguments.OffsetY;

            switch (graphicalObject.MarkerPosition)
            {
                case MarkerPosition.TopMiddle:
                    ResizeTop(graphicalObject, offsetY);
                    break;
                case MarkerPosition.BottomMiddle:
                    ResizeBottom(graphicalObject, offsetY);
                    break;
                case MarkerPosition.LeftMiddle:
                    ResizeLeft(graphicalObject, offsetX);
                    break;
                case MarkerPosition.RightMiddle:
                    ResizeRight(graphicalObject, offsetX);
                    break;
                case MarkerPosition.LeftTop:
                    ResizeLeft(graphicalObject, offsetX);
                    ResizeTop(graphicalObject, offsetY);
                    break;
                case MarkerPosition.RightTop:
                    ResizeRight(graphicalObject, offsetX);
                    ResizeTop(graphicalObject, offsetY);
                    break;
                case MarkerPosition.LeftBottom:
                    ResizeLeft(graphicalObject, offsetX);
                    ResizeBottom(graphicalObject, offsetY);
                    break;
                case MarkerPosition.RightBottom:
                    ResizeRight(graphicalObject, offsetX);
                    ResizeBottom(graphicalObject, offsetY);
                    break;
            }
        }

        // Hilfsfunktionen für die Größenänderung
        private void ResizeTop(GraphicalShapeObjectViewModel graphicalObject, double offsetY)
        {
            double distanceY = graphicalObject.Y - offsetY;
            graphicalObject.Y -= distanceY;
            graphicalObject.Height += distanceY;
        }

        private void ResizeBottom(GraphicalShapeObjectViewModel graphicalObject, double offsetY)
        {
            graphicalObject.Height += offsetY - (graphicalObject.Y + graphicalObject.Height);
        }

        private void ResizeLeft(GraphicalShapeObjectViewModel graphicalObject, double offsetX)
        {
            double distanceX = graphicalObject.X - offsetX;
            graphicalObject.X -= distanceX;
            graphicalObject.Width += distanceX;
        }

        private void ResizeRight(GraphicalShapeObjectViewModel graphicalObject, double offsetX)
        {
            graphicalObject.Width += offsetX - (graphicalObject.X + graphicalObject.Width);
        }

        private void OnMouseUp(MouseEventArgs e) //DataContext.Rectangles
        {
            foreach (GraphicalShapeObjectViewModel graphicalObjectViewModel in DataContext.DiagramObjects)
            {
                if (graphicalObjectViewModel.State == EditState.Moving)
                {
                    graphicalObjectViewModel.State = EditState.None;
                }
                else if (graphicalObjectViewModel.State == EditState.Resizing)
                {
                    graphicalObjectViewModel.State = EditState.None;
                }

            }
            StateHasChanged();
        }

        private void OnMouseClick(MouseEventArgs e) //DataContext.Rectangles
        {
            double x = e.OffsetX;
            double y = e.OffsetY;

            bool isInObject = false;

            foreach (GraphicalShapeObjectViewModel graphicalObjectViewModel in DataContext.DiagramObjects)
            {
                if (x >= graphicalObjectViewModel.X && x <= graphicalObjectViewModel.X + graphicalObjectViewModel.Width)
                {
                    if (y >= graphicalObjectViewModel.Y && y <= graphicalObjectViewModel.Y + graphicalObjectViewModel.Height)
                    {
                        isInObject = true;
                        break;
                    }
                }

            }

            if (!isInObject)
            {
                foreach (GraphicalShapeObjectViewModel graphicalObjectViewModel in DataContext.DiagramObjects)
                {
                    graphicalObjectViewModel.IsSelected = false;
                    graphicalObjectViewModel.State = EditState.None;
                    if (graphicalObjectViewModel.Parent != null)
                    {
                        graphicalObjectViewModel.Parent.SelectedElement = null;
                    }
                }
                StateHasChanged();
            }

        }

    }
}
