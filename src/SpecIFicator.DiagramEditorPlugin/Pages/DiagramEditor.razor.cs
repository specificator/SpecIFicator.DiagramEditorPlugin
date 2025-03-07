using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Pages
{
    public partial class DiagramEditor
    {
        public EditorViewModel DataContext { get; set; }

        protected override void OnInitialized()
        {
            DataContext = new EditorViewModel();
        }
    }
}
