using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SpecIFicator.DiagramEditorPlugin.ViewModels;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views
{
    public partial class SelectionBox
    {
        [Parameter]
        public GraphicalObjectViewModel DataContext { get; set; }

        private void OnMouseDown(MouseEventArgs e)
        {
            DataContext.State = EditState.Moving;
            DataContext.OffsetX = e.OffsetX - DataContext.X;
            DataContext.OffsetY = e.OffsetY - DataContext.Y;
            DataContext.IsSelected = true;

            if (DataContext.Parent != null)
            {
                foreach (GraphicalObjectViewModel graphicalObjectViewModel in DataContext.Parent.DiagramObjects)
                {
                    if (graphicalObjectViewModel != DataContext)
                    {
                        if (graphicalObjectViewModel.IsSelected == true)
                        {
                            graphicalObjectViewModel.IsSelected = false;
                        }
                    }
                }


                DataContext.Parent.SelectedElement = null;
                DataContext.Parent.SelectedElement = DataContext;
            }

        }


    }
}