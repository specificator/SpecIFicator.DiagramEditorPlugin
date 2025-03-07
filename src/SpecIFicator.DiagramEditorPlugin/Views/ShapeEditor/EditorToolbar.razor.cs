using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor
{
    public partial class EditorToolbar
    {
        [Parameter]
        public DiagramViewModel DataContext { get; set; }

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



    } // Ende der class EditorToolbar


}