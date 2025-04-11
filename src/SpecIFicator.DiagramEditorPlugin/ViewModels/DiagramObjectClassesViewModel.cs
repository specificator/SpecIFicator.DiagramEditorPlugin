using GalaSoft.MvvmLight.Command;
using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.SpecIF.DataProvider.Contracts;
using SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;
using System.Windows.Input;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels
{
    public class DiagramObjectClassesViewModel
    {
        private ISpecIfDataProviderFactory _specIfDataProviderFactory;

        public DiagramObjectClassesViewModel(ISpecIfDataProviderFactory specIfDataProviderFactory) 
        {
            _specIfDataProviderFactory = specIfDataProviderFactory;

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            AddDiagramObjectClassCommand = new RelayCommand(ExecuteAddDiagramObjectClassCommand);
            OpenDiagramObjectClassCommand = new RelayCommand<DiagramObjectClassViewModel>(ExecuteOpenDiagramObjectClassCommand);
            CloseEditorCommand = new RelayCommand(ExecuteCloseEditorCommand);
        }

        public List<DiagramObjectClassViewModel> DiagramObjectClasses
        {
            get
            {
                List<DiagramObjectClassViewModel> result = new List<DiagramObjectClassViewModel>();

                List<DiagramObjectClass> diagramObjectClasses = _specIfDataProviderFactory.MetadataReader.GetAllDiagramObjectClasses();

                foreach (DiagramObjectClass diagramObjectClass in diagramObjectClasses)
                {
                    result.Add(new DiagramObjectClassViewModel(diagramObjectClass));
                }

                return result;
            }
        }

        public ShapeDiagramViewModel ShapeUnderEdit { get; set; }

        public bool ShowShapeEditor { get; set; } = false;

        public ICommand AddDiagramObjectClassCommand { get; private set; }
        public ICommand OpenDiagramObjectClassCommand { get; private set; }
        public ICommand CloseEditorCommand { get; private set; }

        private void ExecuteAddDiagramObjectClassCommand()
        {

        }

        private void ExecuteOpenDiagramObjectClassCommand(DiagramObjectClassViewModel diagramObjectClassViewModel)
        {
            ShapeDiagramViewModel shapeDiagramViewModel = new ShapeDiagramViewModel(_specIfDataProviderFactory.MetadataWriter,
                                                                                    diagramObjectClassViewModel.DiagramObjectClass);
            
            ShapeUnderEdit = shapeDiagramViewModel;

            ShowShapeEditor = true;
        }

        private void ExecuteCloseEditorCommand()
        {
            ShapeUnderEdit = null;
            ShowShapeEditor = false;
        }

    }
}
