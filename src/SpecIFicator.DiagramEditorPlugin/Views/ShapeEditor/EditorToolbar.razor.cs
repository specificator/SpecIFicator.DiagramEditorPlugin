using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor
{
    public partial class EditorToolbar
    {
        [Parameter]
        public ShapeDiagramViewModel DataContext { get; set; }

        [Parameter]
        public bool ShowCloseButton { get; set; }

        [Parameter]
        public EventCallback CloseCallback { get; set; }

        public void BringTofront()
        {
            DataContext.BringToFrontCommand.Execute(null);

        } // Ende Bringfront - Methode


        public void SendBackward()
        {
            DataContext.BringToBackwardCommand.Execute(null);

        } // Ende SendBackward

        public void DeleteSelected()
        {
            DataContext.DeleteCommand.Execute(null);

        } // Ende DeleteSelected

        private void OnCloseButtonClicked()
        {
            CloseCallback.InvokeAsync(null);
        }



    } // Ende der class EditorToolbar


}