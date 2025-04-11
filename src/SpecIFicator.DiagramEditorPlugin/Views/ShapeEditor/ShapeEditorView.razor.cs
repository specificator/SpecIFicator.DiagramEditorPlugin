using MDD4All.UI.BlazorComponents.TabControl;
using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor
{
    public partial class ShapeEditorView
    {

        

        [Parameter]
        public EditorViewModel DataContext { get; set; }

        protected override void OnInitialized()
        {
            
            DataContext.PropertyChanged += OnDataContextPropertyChanged;
        }

        private void OnDataContextPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
            StateHasChanged();
        }
    }
}