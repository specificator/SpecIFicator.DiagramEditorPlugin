using MDD4All.UI.DataModels.TabControl;
using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor
{
    public partial class ShapeEditor
    {
        [CascadingParameter(Name = "DataContext")]
        public ITabPage TabPageContext
        {
            set
            {
                if (value is ShapeDiagramViewModel)
                {
                    DataContext = value as ShapeDiagramViewModel;
                }
            }
        }

        [Parameter]
        public ShapeDiagramViewModel DataContext { get; set; }

        [Parameter]
        public EventCallback CloseEditorCallback { get; set; }

        protected override void OnInitialized()
        {
            DataContext.PropertyChanged += OnPorpertyChanged;
        }

        private void OnPorpertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }
    }
}