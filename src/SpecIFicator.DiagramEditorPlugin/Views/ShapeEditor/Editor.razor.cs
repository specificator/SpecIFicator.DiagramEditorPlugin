using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor
{
    public partial class Editor
    {
        public DiagramEditorViewModel DataContext { get; set; } = new DiagramEditorViewModel();

        protected override void OnInitialized()
        {
            DataContext.DiagramViewModel.PropertyChanged += OnPorpertyChanged;
        }

        private void OnPorpertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }
    }
}