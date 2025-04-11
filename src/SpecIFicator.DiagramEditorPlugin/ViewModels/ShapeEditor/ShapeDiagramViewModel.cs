using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.SpecIF.DataProvider.Contracts;
using MDD4All.UI.DataModels.TabControl;
using SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor;
using System.Windows.Input;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor
{
    public class ShapeDiagramViewModel : ViewModelBase, ITabPage, IDiagramViewModel
    {
        private ISpecIfMetadataWriter _specIfMetadataWriter;

        public ShapeDiagramViewModel(ISpecIfMetadataWriter specIfMetadataWriter)
        {
            _specIfMetadataWriter = specIfMetadataWriter;
            DiagramObjectClass = new DiagramObjectClass();
            InitializeCommands();
        }

        public ShapeDiagramViewModel(ISpecIfMetadataWriter specIfMetadataWriter, 
                                DiagramObjectClass diagramObjectClass) 
                                : this(specIfMetadataWriter)
        {
            DiagramObjectClass = diagramObjectClass;

            foreach (ShapePrimitive shape in DiagramObjectClass.ShapePrimitives)
            {
                if (shape is RectangleShapePrimitive)
                {
                    DiagramObjects.Add(new RectangleViewModel((RectangleShapePrimitive)shape)
                                            {
                                                Parent = this
                                            });
                }
                else if (shape is CircleShapePrimitive)
                {
                    DiagramObjects.Add(new CircleViewModel((CircleShapePrimitive)shape)
                                            {
                                                Parent = this
                                            });
                }
                else if (shape is EllipseShapePrimitive)
                {
                    DiagramObjects.Add(new EllipseViewModel((EllipseShapePrimitive)shape)
                                            {
                                                Parent = this
                                            });
                }
                else if (shape is TextShapePrimitive)
                {
                    DiagramObjects.Add(new TextViewModel((TextShapePrimitive)shape)
                                            {
                                                Parent = this
                                            });
                }
            }
        }

        private void InitializeCommands()
        {
            SaveDiagramCommand = new RelayCommand(ExecuteSaveDiagramCommand);
            BringToFrontCommand = new RelayCommand(ExecuteBringToFrontCommand);
            BringToBackwardCommand = new RelayCommand(ExecuteBringToBackwardCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        }

        // The data model
        public DiagramObjectClass DiagramObjectClass { get; }

        
        public string Title
        {
            get 
            { 
                return DiagramObjectClass.Title; 
            }
            
            set 
            { 
                DiagramObjectClass.Title = value; 
            }
        }

        public string ID
        {
            get
            {
                return DiagramObjectClass.ID;
            }

            set
            {
                DiagramObjectClass.ID = value;
            }
        }

        public string Revision
        {
            get
            {
                return DiagramObjectClass.Revision;
            }

            set
            {
                DiagramObjectClass.Revision = value;
            }
        }

        public string Stereotype
        {
            get
            {
                return DiagramObjectClass.MapsTo.Stereotype;
            }

            set
            {
                DiagramObjectClass.MapsTo.Stereotype = value;
            }
        }

        public string Type
        {
            get
            {
                return DiagramObjectClass.MapsTo.Type;
            }

            set
            {
                DiagramObjectClass.MapsTo.Type = value;
            }
        }

        public Key MainResourceClassKey 
        {
            get; set;
        }



        public bool IsPort
        {
            get { return DiagramObjectClass.IsPort; }
            set { DiagramObjectClass.IsPort = value; }
        }

        public bool CanResize
        {
            get { return DiagramObjectClass.CanResize; }
            set { DiagramObjectClass.CanResize = value; }
        }

        public bool CanRotate
        {
            get { return DiagramObjectClass.CanRotate; }
            set { DiagramObjectClass.CanRotate = value; }
        }

        public List<GraphicalObjectViewModel> DiagramObjects { get; set; } = new List<GraphicalObjectViewModel>();

        public string ToolboxItemType { get; set; } = null;

        private GraphicalObjectViewModel? _selectedElement = null;

        public GraphicalObjectViewModel? SelectedElement
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

        public bool IsModified { get; set; }


        #region COMMAND_DEFINITIONS
        public ICommand SaveDiagramCommand { get; private set; }

        public ICommand BringToFrontCommand { get; private set; }

        public ICommand BringToBackwardCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }
        #endregion

        public Type ViewType
        {
            get
            {
                Type result = System.Type.GetType("SpecIFicator.DiagramEditorPlugin.Views.ShapeEditor.ShapeEditor");
                return result;
            }
        }

        public string Header
        {
            get
            {
                string result = string.Empty;

                result += Title;

                return result;
            }
        }

        


        #region COMMAND_IMPLEMENTATIONS
        private void ExecuteSaveDiagramCommand()
        {
            DiagramObjectClass.ShapePrimitives.Clear();

            foreach (GraphicalShapeObjectViewModel graphicalObject in DiagramObjects)
            {
                DiagramObjectClass.ShapePrimitives.Add(graphicalObject.ShapePrimitive);    
            }
            _specIfMetadataWriter.UpdateDiagramObjectClass(DiagramObjectClass);
        }

        private void ExecuteDeleteCommand()
        {
            for (int index = 0; index < DiagramObjects.Count; index++)
            {
                if (DiagramObjects[index].IsSelected)
                {
                    // Entferne das ausgewählte Objekt aus der Liste
                    DiagramObjects.RemoveAt(index);
                    break; // Beende die Schleife nach dem ersten gefundenen Element
                }
            }
            RaisePropertyChanged();
        }

        private void ExecuteBringToFrontCommand()
        {
            int selectedElementIndex = -1;

            for (int index = 0; index < DiagramObjects.Count; index++)
            {
                GraphicalObjectViewModel graphicalObject = DiagramObjects[index];
                if (graphicalObject.IsSelected == true)
                {
                    selectedElementIndex = index;
                    break; // // Beende die Schleife, sobald das erste ausgewählte Element gefunden wurde
                } // Ende if

            } // Ende for - Schleife

            // Prüfen, ob ein Element gefunden wurde und ob es ein rechtes Nachbarelement gibt
            if (selectedElementIndex != -1 && selectedElementIndex < DiagramObjects.Count - 1)
            {
                // Tausche das ausgewählte Element mit dem rechten Nachbarn
                GraphicalObjectViewModel temp = DiagramObjects[selectedElementIndex];
                DiagramObjects[selectedElementIndex] = DiagramObjects[selectedElementIndex + 1];
                DiagramObjects[selectedElementIndex + 1] = temp;
            }

            RaisePropertyChanged();
        }

        private void ExecuteBringToBackwardCommand()
        {
            for (int index = 1; index < DiagramObjects.Count; index++) // Start bei 1, damit es immer einen linken Nachbarn gibt
            {
                GraphicalObjectViewModel x = DiagramObjects[index];

                if (x.IsSelected)
                {
                    // Tausche das aktuelle Objekt mit dem linken Nachbarn
                    (DiagramObjects[index], DiagramObjects[index - 1]) =
                        (DiagramObjects[index - 1], DiagramObjects[index]);

                    break; // Beende die Schleife nach dem ersten gefundenen Element
                }
            }

            RaisePropertyChanged();
        }
        #endregion

    }

}