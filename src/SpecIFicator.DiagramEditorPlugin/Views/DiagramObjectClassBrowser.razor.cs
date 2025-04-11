using MDD4All.SpecIF.DataProvider.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SpecIFicator.DiagramEditorPlugin.ViewModels;

namespace SpecIFicator.DiagramEditorPlugin.Views
{
    public partial class DiagramObjectClassBrowser
    {
        [Inject]
        private ISpecIfDataProviderFactory SpecIfDataProviderFactory { get; set; }

        [Inject]
        private IStringLocalizer<DiagramObjectClassBrowser> L { get; set; }

        public DiagramObjectClassesViewModel DataContext { get; set; }

        protected override void OnInitialized()
        {
            DataContext = new DiagramObjectClassesViewModel(SpecIfDataProviderFactory);
        }

        private void OnCloseEditor()
        {
            DataContext.CloseEditorCommand.Execute(null);
            StateHasChanged();
        }
    }
}