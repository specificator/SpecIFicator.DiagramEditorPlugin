using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.SpecIF.DataProvider.Contracts;
using Microsoft.AspNetCore.Components;
using MDD4All.SpecIF.DataModels.Manipulation;

namespace SpecIFicator.DiagramEditorPlugin.Views
{
    public partial class DiagramObjectClassSelector
    {
        [Inject]
        private ISpecIfDataProviderFactory DataProviderFactory { get; set; }

        private Key _selectedDiagramObjectClass;

        [Parameter]
        public Key SelectedDiagramObjectClassKey { get; set; }

        [Parameter]
        public EventCallback<Key> SelectedDiagramObjectClassKeyChanged { get; set; }

        private List<DiagramObjectClass> AvailableDiagramObjectClasses
        {
            get
            {
                List<DiagramObjectClass> result = new List<DiagramObjectClass>();

                result = DataProviderFactory.MetadataReader.GetAllDiagramObjectClasses();

                return result;
            }
        }

        private async Task OnDiagramObjectClassSelectionChange(ChangeEventArgs args)
        {
            Console.WriteLine(args.Value.ToString());
            string selection = args.Value.ToString();
            if (!string.IsNullOrEmpty(selection))
            {
                _selectedDiagramObjectClass = new Key();
                _selectedDiagramObjectClass.InitailizeFromKeyString(selection);

                await SelectedDiagramObjectClassKeyChanged.InvokeAsync(_selectedDiagramObjectClass);
            }


        }

        protected async override Task OnInitializedAsync()
        {
            List<DiagramObjectClass> diagramObjectClasses = AvailableDiagramObjectClasses;

            if(diagramObjectClasses.Any())
            {
                SelectedDiagramObjectClassKey = new Key(diagramObjectClasses[0].ID, 
                                                        diagramObjectClasses[0].Revision);

                await SelectedDiagramObjectClassKeyChanged.InvokeAsync(SelectedDiagramObjectClassKey);
            }

        }
    }
}