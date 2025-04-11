using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor.Sidebar
{
    public partial class Sidebar
    {
        [Parameter]
        public EditorViewModel DataContext { get; set; }

        private bool _menuVisible = false;

        private void ToggleMenu()
        {
            _menuVisible = !_menuVisible;
            StateHasChanged();
        }
    }
}