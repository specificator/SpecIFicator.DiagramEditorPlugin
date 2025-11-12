using GalaSoft.MvvmLight.Command;
using MDD4All.SpecIF.DataFactory;
using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.SpecIF.DataModels.Helpers;
using MDD4All.SpecIF.DataProvider.Contracts;
using MDD4All.SpecIF.ViewModels;
using MDD4All.UI.DataModels.TabControl;
using SpecIFicator.DiagramEditorPlugin.CommandParamaters;
using SpecIFicator.DiagramEditorPlugin.Views.DiagramEditor;
using System.Reflection.Metadata;
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

        public DiagramViewModel(ISpecIfDataProviderFactory specIfDataProviderFactory,
                                NodeViewModel diagramNode,
                                HierarchyViewModel hierarchyViewModel) :
                            base(specIfDataProviderFactory.MetadataReader,
                                 specIfDataProviderFactory.DataReader,
                                 specIfDataProviderFactory.DataWriter,
                                 diagramNode.ReferencedResource.Resource)
        {
            _specIfDataProviderFactory = specIfDataProviderFactory;
            DiagramNode = diagramNode;
            HierarchyViewModel = hierarchyViewModel;

            if ((NodeViewModel)diagramNode.Parent != null)
            {
                ParentForNewDiagramResources = (NodeViewModel)diagramNode.Parent;
            }

            InitializeCommands();
        }

        public DiagramViewModel(ISpecIfMetadataReader metadataReader,
                                ISpecIfDataReader dataReader,
                                ISpecIfDataWriter dataWriter,
                                NodeViewModel diagramNode) : base(metadataReader, 
                                                                  dataReader, 
                                                                  dataWriter, 
                                                                  diagramNode.ReferencedResource.Resource)
        {
            DiagramNode = diagramNode;

            if ((NodeViewModel)diagramNode.Parent != null)
            {
                ParentForNewDiagramResources = (NodeViewModel)diagramNode.Parent;
            }

            InitializeCommands();
        }
        
        private void InitializeCommands()
        {
            CreateDiagramObjectCommand = new RelayCommand<DiagramObjectCreationParamater>(ExecuteCreateDiagramObject);
        }

        public NodeViewModel DiagramNode { get; set; }

        public HierarchyViewModel HierarchyViewModel { get; set; }

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

        public NodeViewModel ParentForNewDiagramResources { get; set; }

        public ICommand CreateDiagramObjectCommand { get; private set; }


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

                            DataWriter.AddResource(mainResource);

                            diagramObjectViewModel.MainResource = mainResourceViewModel;

                            if(ParentForNewDiagramResources != null)
                            {
                                Node hierarchyNode = CreateNewNodeForAddition(mainResourceViewModel);

                                NodeViewModel newTreeNodeViewModel = new NodeViewModel(MetadataReader,
                                                                       DataReader,
                                                                       DataWriter,
                                                                       HierarchyViewModel,
                                                                       hierarchyNode);

                                newTreeNodeViewModel.Parent = ParentForNewDiagramResources;

                                Node parentNode = ParentForNewDiagramResources.HierarchyNode;

                                parentNode.Nodes.Add(hierarchyNode);

                                if (ParentForNewDiagramResources.Children.Any())
                                {
                                    NodeViewModel lastChild = (NodeViewModel) ParentForNewDiagramResources.Children[ParentForNewDiagramResources.Children.Count - 1];
                                    DataWriter.AddNodeAsPredecessor(lastChild.NodeID, hierarchyNode);
                                }
                                else
                                {
                                    DataWriter.AddNodeAsFirstChild(ParentForNewDiagramResources.NodeID, hierarchyNode);
                                }


                            }
                        }
                    }



                    DiagramObjects.Add(diagramObjectViewModel);

                }
            }

        }

        private Node CreateNewNodeForAddition(ResourceViewModel resource)
        {
            Node result = new Node()
            {
                ID = SpecIfGuidGenerator.CreateNewSpecIfGUID(),
                Revision = SpecIfGuidGenerator.CreateNewRevsionGUID(),
                ChangedAt = DateTime.Now,
                IsHierarchyRoot = false,

            };

            Key resourceKey = new Key(resource.Resource.ID, resource.Resource.Revision);

            result.ResourceReference = resourceKey;

            return result;
        }

    }
}
