using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MDD4All.SpecIF.DataFactory;
using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.SpecIF.DataProvider.Contracts;
using MDD4All.UI.DataModels.TabControl;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor
{
    public class EditorViewModel : ViewModelBase, ITabControl
    {
        private ISpecIfMetadataWriter _specIfMetadataWriter;
        private ISpecIfMetadataReader _specIfMetadataReader;


        public EditorViewModel(ISpecIfMetadataWriter specIfMetadataWriter,
                               ISpecIfMetadataReader specIfMetadataReader)
        {
            _specIfMetadataWriter = specIfMetadataWriter;
            _specIfMetadataReader = specIfMetadataReader;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            NewEditorCommand = new RelayCommand(ExecuteNewEditorCommand);
            OpenShapeCommand = new RelayCommand(ExecuteOpenShapeCommand);
            ConfirmOpenShapeCommand = new RelayCommand(ExecuteConfirmOpenShapeCommand);
            CloseDiagramCommand = new RelayCommand<ShapeDiagramViewModel>(ExecuteCloseDiagramCommand);
        }

        public ShapeDiagramViewModel ShapeUnderEdit { get; set; }

        public bool EditDiagramObjectClassProperties { get; set; } = false;

        public bool ShowShapeSelection { get; set; } = false;

        public Key SelectedDiagramObjectClassKey { get; set; }


        #region COMMAND_DEFINITIONS
        public ICommand NewEditorCommand { get; private set; }

        public ICommand OpenShapeCommand { get; private set; }

        public ICommand ConfirmOpenShapeCommand { get; private set; }

        public ICommand CloseDiagramCommand { get; private set; }
        #endregion



        public ObservableCollection<ITabPage> Pages { get; set; } = new ObservableCollection<ITabPage>();

        public bool CanClose { get; set; }

        public ITabPage ActivePage { get; set; }

        public bool ShowTabsOnBottom { get; set; }




        private void ExecuteNewEditorCommand()
        {
            ShapeUnderEdit = new ShapeDiagramViewModel(_specIfMetadataWriter);

            EditDiagramObjectClassProperties = true;
            RaisePropertyChanged();
        }

        

        private void ExecuteOpenShapeCommand()
        {
            ShowShapeSelection = true;
            RaisePropertyChanged();
        }

        private void ExecuteConfirmOpenShapeCommand()
        {
            if (SelectedDiagramObjectClassKey != null)
            {
                DiagramObjectClass diagramObjectClass = _specIfMetadataReader.GetDiagramObjectClassByKey(SelectedDiagramObjectClassKey);
                ShapeDiagramViewModel diagramViewModel = new ShapeDiagramViewModel(_specIfMetadataWriter, diagramObjectClass);
                ActivePage = diagramViewModel;
                Pages.Add(diagramViewModel);

            }

            ShowShapeSelection = false;
            RaisePropertyChanged();
        }

        private void ExecuteCloseDiagramCommand(ShapeDiagramViewModel model)
        {
            Pages.Remove(model);
            RaisePropertyChanged();
        }
    }
}
