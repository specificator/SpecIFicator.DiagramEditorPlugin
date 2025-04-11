using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor.Shapes
{
    public partial class Circle
    {

        [Parameter]
        public CircleViewModel DataContext { get; set; }

        //public string Color { get; set; } = "blue";
        public int Radius { get; set; } = 50;


        private void OnMouseDown(MouseEventArgs e)
        {
            DataContext.State = EditState.Moving;
            DataContext.OffsetX = e.OffsetX - DataContext.X;
            DataContext.OffsetY = e.OffsetY - DataContext.Y;
            DataContext.IsSelected = true;


            foreach (GraphicalShapeObjectViewModel graphicalObjectViewModel in DataContext.Parent.DiagramObjects)
            {
                if (graphicalObjectViewModel != DataContext)
                {
                    if (graphicalObjectViewModel.IsSelected == true)
                    {
                        graphicalObjectViewModel.IsSelected = false;
                    }
                }
            }
            if (DataContext.Parent != null)
            {
                DataContext.Parent.SelectedElement = DataContext;
            }


        }


    }
}