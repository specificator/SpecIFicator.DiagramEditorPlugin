using GalaSoft.MvvmLight;
using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataProvider.Contracts;
using MDD4All.SpecIF.ViewModels;
using MDD4All.UI.DataModels.TabControl;
using System.Collections.ObjectModel;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor
{
    public class DiagramEditorViewModel : ViewModelBase, ITabControl
    {
        private ISpecIfDataProviderFactory _dataProviderFactory;

        public DiagramEditorViewModel(ISpecIfDataProviderFactory specIfDataProviderFactory,
                                      HierarchyViewModel hierarchyViewModel)
        {
            _dataProviderFactory = specIfDataProviderFactory;
            HierarchyViewModel = new HierarchyViewModel(_dataProviderFactory, hierarchyViewModel.RootNode.HierarchyKey);

            Resource diagramResource = ((NodeViewModel)hierarchyViewModel.SelectedNode).ReferencedResource.Resource;

            NodeViewModel diagramNodeViewModel = hierarchyViewModel.SelectedNode as NodeViewModel;

            

            // just for testing
            DiagramViewModel diagramViewModel = new DiagramViewModel(specIfDataProviderFactory, diagramNodeViewModel, HierarchyViewModel);

            diagramViewModel.PropertyChanged += OnDiagramViewModelPropertyChanged;

            //diagramViewModel.DiagramObjects = new List<GraphicalObjectViewModel>();

            //{
            //    new DiagramObjectViewModel()
            //    {
            //        SvgShape = new Shape()
            //        {
            //            Bounds = new Bounds()
            //            {
            //                X = 100,
            //                Y = 100,
            //                Width = 300,
            //                Height = 200
            //            }
            //        },
            //        Parent = diagramViewModel
            //    }
            //};

            //DiagramObjectViewModel diagramObjectViewModel = new DiagramObjectViewModel(specIfDataProviderFactory,
            //                           new Key("_c30b7d04_7e02_4c03_b25b_6cf663404282", "d546b249-b837-4008-9590-ec7a99636899"));

            //diagramObjectViewModel.X = 100;
            //diagramObjectViewModel.Y = 200;
            //diagramObjectViewModel.Width = 300;
            //diagramObjectViewModel.Height = 200;

            //diagramObjectViewModel.Parent = diagramViewModel;

            //diagramViewModel.DiagramObjects.Add(diagramObjectViewModel);




            Pages.Add(diagramViewModel);

        }

        private void OnDiagramViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged();
        }

        private ObservableCollection<ITabPage> _diagramEditors = new ObservableCollection<ITabPage>();

        public ObservableCollection<ITabPage> Pages 
        { 
            get
            {
                return _diagramEditors;
            }
            
            set
            {
                _diagramEditors = value;
            }
        }

        public bool CanClose 
        { 
            get => throw new NotImplementedException(); 
            
            set => throw new NotImplementedException(); 
        }
        
        public DiagramViewModel ActiveDiagram
        {
            get
            {
                DiagramViewModel result = null;

                if (ActivePage != null && ActivePage is DiagramViewModel)
                {
                    result = (DiagramViewModel)ActivePage;
                }

                return result;
            }
        }

        public ITabPage ActivePage 
        {
            get; set;
        }
        
        public bool ShowTabsOnBottom 
        { 
            get => throw new NotImplementedException(); 
            
            set => throw new NotImplementedException(); 
        }

        public HierarchyViewModel HierarchyViewModel { get; set; }

        public NodeViewModel DiagramNodeViewModel { get; set; }
    }
}
