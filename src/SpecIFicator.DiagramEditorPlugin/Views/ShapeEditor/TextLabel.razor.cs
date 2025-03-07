using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor
{
    public partial class TextLabel
    {
        [Parameter]
        public TextViewModel DataContext { get; set; }

        private void OnMouseDown(MouseEventArgs e)
        {
            DataContext.State = EditState.Moving;
            DataContext.OffsetX = e.OffsetX - DataContext.X;
            DataContext.OffsetY = e.OffsetY - DataContext.Y;
            DataContext.IsSelected = true;
            foreach (GraphicalObjectViewModel graphicalObjectViewModel in DataContext.Parent.GraphicalObjects)
            {
                if (graphicalObjectViewModel != DataContext)
                {
                    if (graphicalObjectViewModel.IsSelected == true)
                    {
                        graphicalObjectViewModel.IsSelected = false;
                    }
                }
            }
        }
    }
}