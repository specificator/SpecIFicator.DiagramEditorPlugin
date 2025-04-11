using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;


namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor.Shapes
{
    public partial class Rectangle
    {
        [Parameter]
        public RectangleViewModel DataContext { get; set; }


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
