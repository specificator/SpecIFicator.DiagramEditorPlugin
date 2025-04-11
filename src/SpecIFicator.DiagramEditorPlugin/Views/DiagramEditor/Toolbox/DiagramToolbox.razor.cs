using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.SpecIF.DataProvider.Contracts;
using Microsoft.AspNetCore.Components;
using SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.DiagramEditor.Toolbox
{
    public partial class DiagramToolbox
    {
        [Inject]
        public ISpecIfDataProviderFactory DataProviderFactory { get; set; }

        private List<DiagramObjectClassViewModel> DiagramObjectClassViewModels { get; set; } = new List<DiagramObjectClassViewModel>();

        protected override void OnInitialized()
        {
            List<DiagramObjectClass> diagramObjectClasses = DataProviderFactory.MetadataReader.GetAllDiagramObjectClasses();

            foreach (DiagramObjectClass diagramObjectClass in diagramObjectClasses)
            {
                DiagramObjectClassViewModels.Add(new DiagramObjectClassViewModel(diagramObjectClass));
            }
        }
    }
}