using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views
{
    public partial class SelectionMarker
    {
        [Parameter]
        public double X { get; set; } // von Rotenpunkt = Marker

        [Parameter]
        public double Y { get; set; }

        [Parameter]
        public MarkerPosition Position { get; set; }

        [Parameter]
        public GraphicalObjectViewModel GraphicalObjectViewModel { get; set; }


        private void OnMouseDown(MouseEventArgs arguments)
        {
            GraphicalObjectViewModel.State = EditState.Resizing;
            GraphicalObjectViewModel.MarkerPosition = Position;

        }


    } // Ende Class 
}