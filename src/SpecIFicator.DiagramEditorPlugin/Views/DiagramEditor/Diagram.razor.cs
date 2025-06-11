using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.ViewModels;
using MDD4All.UI.BlazorComponents.Services;
using MDD4All.UI.DataModels.DragDrop;
using MDD4All.UI.DataModels.TabControl;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SpecIFicator.DiagramEditorPlugin.CommandParamaters;
using SpecIFicator.DiagramEditorPlugin.ViewModels;
using SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;


namespace SpecIFicator.DiagramEditorPlugin.Views.DiagramEditor
{
    public partial class Diagram
    {
        [Inject]
        public DragDropDataProvider DragDropDataProvider { get; set; }

        [CascadingParameter(Name = "DataContext")]
        public ITabPage TabPageContext
        {
            set
            {
                if (value is DiagramViewModel)
                {
                    DataContext = value as DiagramViewModel;
                }
            }
        }

        [Parameter]
        public DiagramViewModel DataContext { get; set; }

        // Speicherung der Mauskoordinaten
        private double ClientX { get; set; } // Position relativ zum sichtbaren Bereich des Browsers

        private double ClientY { get; set; }

        private double OffsetX { get; set; } // Position relativ zum Element, auf das geklickt wurde

        private double OffsetY { get; set; }

        private double ScreenX { get; set; } // Absolute Bildschirmposition der Maus

        private double ScreenY { get; set; }

        private double PageX { get; set; } // Position relativ zur gesamten Webseite

        private double PageY { get; set; }

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



            foreach (GraphicalObjectViewModel graphicalObject in DataContext.DiagramObjects)
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
            foreach (GraphicalObjectViewModel graphicalObjectViewModel in DataContext.DiagramObjects)
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

            foreach (GraphicalObjectViewModel graphicalObjectViewModel in DataContext.DiagramObjects)
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
                foreach (GraphicalObjectViewModel graphicalObjectViewModel in DataContext.DiagramObjects)
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
    
        private void OnDrop(DragEventArgs dragEventArguments)
        {
            DragDropInformation data = DragDropDataProvider.GetData();
            if (data != null)
            {
                if(data.Data is Key && data.OperationInformation == DragDropInformationValues.CREATE_DIAGRAM_OBJECT)
                {
                    DiagramObjectCreationParamater paramater = new DiagramObjectCreationParamater
                    {
                        DiagramObjectClassKey = (Key)data.Data,
                        X = dragEventArguments.OffsetX,
                        Y = dragEventArguments.OffsetY,
                    };

                    DragDropDataProvider.SetData(null);
                    DataContext.CreateDiagramObjectCommand.Execute(paramater);
                }
            }
        }

        

    }
}