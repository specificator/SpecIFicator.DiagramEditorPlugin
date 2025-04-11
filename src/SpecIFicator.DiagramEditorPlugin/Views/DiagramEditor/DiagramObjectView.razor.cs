using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SpecIFicator.DiagramEditorPlugin.ViewModels;
using SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.DiagramEditor
{
    public partial class DiagramObjectView
    {
        [Parameter]
        public DiagramObjectViewModel DataContext { get; set; }

        private void OnMouseDown(MouseEventArgs e)
        {
            DataContext.State = EditState.Moving;

            DataContext.OffsetX = e.OffsetX - DataContext.X;
            DataContext.OffsetY = e.OffsetY - DataContext.Y;
            DataContext.IsSelected = true;


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
            if (DataContext.Parent != null)
            {
                DataContext.Parent.SelectedElement = DataContext;
            }
        }
    }
}