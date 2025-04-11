using GalaSoft.MvvmLight.Command;
using MDD4All.SpecIF.DataFactory;
using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.SpecIF.DataProvider.Contracts;
using MDD4All.SpecIF.ViewModels;
using MDD4All.UI.DataModels.TabControl;
using SpecIFicator.DiagramEditorPlugin.CommandParamaters;
using SpecIFicator.DiagramEditorPlugin.Views.DiagramEditor;
using System.Windows.Input;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor
{
    public class DiagramViewModel : ResourceViewModel, ITabPage, IDiagramViewModel
    {
        private ISpecIfDataProviderFactory _specIfDataProviderFactory;

        public DiagramViewModel(ISpecIfDataProviderFactory specIfDataProviderFactory) : 
                            base(specIfDataProviderFactory.MetadataReader, 
                                 specIfDataProviderFactory.DataReader, 
                                 specIfDataProviderFactory.DataWriter)
        {
            _specIfDataProviderFactory = specIfDataProviderFactory;
            InitializeCommands();
        }

        public DiagramViewModel(ISpecIfMetadataReader metadataReader,
                                ISpecIfDataReader dataReader,
                                ISpecIfDataWriter dataWriter,
                                Resource diagramResource) : base(metadataReader, dataReader, dataWriter, diagramResource)
        {
            InitializeCommands();
        }
        
        private void InitializeCommands()
        {
            CreateDiagramObjectCommand = new RelayCommand<DiagramObjectCreationParamater>(ExecuteCreateDiagramObject);
        }

        private void ExecuteCreateDiagramObject(DiagramObjectCreationParamater paramater)
        {
            if (paramater != null && paramater.DiagramObjectClassKey != null)
            {
                DiagramObjectClass diagramObjectClass = MetadataReader.GetDiagramObjectClassByKey(paramater.DiagramObjectClassKey);
                if (diagramObjectClass != null)
                {
                    DiagramObjectViewModel diagramObjectViewModel = new DiagramObjectViewModel(_specIfDataProviderFactory,
                                                                                               paramater.DiagramObjectClassKey);

                    diagramObjectViewModel.X = paramater.X;
                    diagramObjectViewModel.Y = paramater.Y;
                    diagramObjectViewModel.SetDefaultSize();

                    diagramObjectViewModel.Parent = this;

                    // create resource object
                    if (diagramObjectClass.DataTemplate != null &&
                        diagramObjectClass.DataTemplate.Resources.Any())
                    {
                        Key templateMainResourceClassKey = diagramObjectClass.DataTemplate.Resources[0].Class;

                        Resource mainResource = SpecIfDataFactory.CreateResource(templateMainResourceClassKey);

                        if (mainResource != null)
                        {
                            ResourceViewModel mainResourceViewModel = new ResourceViewModel(MetadataReader,
                                                                                            DataReader,
                                                                                            DataWriter,
                                                                                            mainResource);
                            
                            diagramObjectViewModel.MainResource = mainResourceViewModel;
                        }
                    }



                    DiagramObjects.Add(diagramObjectViewModel);
                    
                }
            }

        }

        public Type ViewType
        {
            get 
            { 
                return typeof(Diagram); 
            }
        }

        public string Header
        {
            get
            {
                return Title;
            }
        }

        public List<GraphicalObjectViewModel> DiagramObjects { get; set; } = new List<GraphicalObjectViewModel>();

        private GraphicalObjectViewModel _selectedElement;
        
        public GraphicalObjectViewModel SelectedElement
        {
            set
            {
                if (_selectedElement != value)
                {
                    _selectedElement = value;
                    RaisePropertyChanged();
                }
            }

            get
            {
                return _selectedElement;
            }
        }

        public ICommand CreateDiagramObjectCommand { get; private set; }


    }
}
