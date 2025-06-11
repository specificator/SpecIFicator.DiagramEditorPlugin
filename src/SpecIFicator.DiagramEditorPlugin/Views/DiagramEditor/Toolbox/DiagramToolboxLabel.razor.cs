using MDD4All.SpecIF.ViewModels;
using MDD4All.UI.BlazorComponents.Services;
using MDD4All.UI.DataModels.DragDrop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor;

namespace SpecIFicator.DiagramEditorPlugin.Views.DiagramEditor.Toolbox
{
    public partial class DiagramToolboxLabel
    {
        [Inject]
        public DragDropDataProvider DragDropDataProvider { get; set; }

        [Parameter]
        public DiagramObjectClassViewModel DataContext { get; set; }

        private void OnDragStart(DragEventArgs dragEventArgs)
        {
            DragDropDataProvider.SetData(new DragDropInformation
                                                {
                                                    Data = DataContext.Key,
                                                    OperationInformation = DragDropInformationValues.CREATE_DIAGRAM_OBJECT
                                                });
        }
    }
}