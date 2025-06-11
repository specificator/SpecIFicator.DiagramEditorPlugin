using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MDD4All.SpecIF.DataFactory;
using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.SpecIF.DataProvider.Contracts;
using SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor;
using SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor;
using System.Windows.Input;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels
{
    public class DiagramObjectClassesViewModel : ViewModelBase
    {
        private ISpecIfDataProviderFactory _specIfDataProviderFactory;

        public DiagramObjectClassesViewModel(ISpecIfDataProviderFactory specIfDataProviderFactory) 
        {
            _specIfDataProviderFactory = specIfDataProviderFactory;

            EditorViewModel = new EditorViewModel(specIfDataProviderFactory.MetadataWriter,
                                                  specIfDataProviderFactory.MetadataReader);

            EditorViewModel.PropertyChanged += OnEditorViewModelPropertyChanged;
            InitializeCommands();
        }

        private void OnEditorViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged();
        }

        private void InitializeCommands()
        {
            AddDiagramObjectClassCommand = new RelayCommand(ExecuteAddDiagramObjectClassCommand);
            ConfirmNewEditorCommand = new RelayCommand(ExecuteConfirmNewEditorCommand);
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

        public EditorViewModel EditorViewModel { get; set; }


        public bool ShowShapeEditor { get; set; } = false;

        public bool ShowDiagramObjectClassCreationUserInterface { get; set; } = false;

        #region COMMAND_DEFINITIONS
        public ICommand AddDiagramObjectClassCommand { get; private set; }
        public ICommand ConfirmNewEditorCommand { get; private set; }
        public ICommand OpenDiagramObjectClassCommand { get; private set; }
        public ICommand CloseEditorCommand { get; private set; }

        #endregion

        private void ExecuteAddDiagramObjectClassCommand()
        {
            EditorViewModel.ShapeUnderEdit = new ShapeDiagramViewModel(_specIfDataProviderFactory.MetadataWriter);
            ShowDiagramObjectClassCreationUserInterface = true;
        }

        private void ExecuteConfirmNewEditorCommand()
        {
            if (EditorViewModel.ShapeUnderEdit != null)
            {
                // add templates
                if (EditorViewModel.ShapeUnderEdit.MainResourceClassKey != null)
                {
                    Resource templateResource = SpecIfDataFactory.CreateResource(EditorViewModel.ShapeUnderEdit.MainResourceClassKey);

                    EditorViewModel.ShapeUnderEdit.DiagramObjectClass.DataTemplate.Resources.Add(templateResource);
                }

                // save data and refresh the view
                _specIfDataProviderFactory.MetadataWriter.AddDiagramObjectClass(EditorViewModel.ShapeUnderEdit.DiagramObjectClass);
                EditorViewModel.Pages.Add(EditorViewModel.ShapeUnderEdit);
                //ShapeUnderEdit = null;

                ShowDiagramObjectClassCreationUserInterface = false;
                RaisePropertyChanged();
            }
        }

        private void ExecuteOpenDiagramObjectClassCommand(DiagramObjectClassViewModel diagramObjectClassViewModel)
        {
            ShapeDiagramViewModel shapeDiagramViewModel = new ShapeDiagramViewModel(_specIfDataProviderFactory.MetadataWriter,
                                                                                    diagramObjectClassViewModel.DiagramObjectClass);
            
            EditorViewModel.ShapeUnderEdit = shapeDiagramViewModel;

            ShowShapeEditor = true;
        }

        private void ExecuteCloseEditorCommand()
        {
            EditorViewModel.ShapeUnderEdit = null;
            ShowShapeEditor = false;
        }

    }
}
