using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor
{
    public class DiagramViewModel : ViewModelBase
    {
        public DiagramViewModel()
        {
            BringToFrontCommand = new RelayCommand(ExecuteBringToFrontCommand);
            BringToBackwardCommand = new RelayCommand(ExecuteBringToBackwardCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);

        }

        private void ExecuteDeleteCommand()
        {
            for (int index = 0; index < GraphicalObjects.Count; index++)
            {
                if (GraphicalObjects[index].IsSelected)
                {
                    // Entferne das ausgewählte Objekt aus der Liste
                    GraphicalObjects.RemoveAt(index);
                    break; // Beende die Schleife nach dem ersten gefundenen Element
                }
            }
            RaisePropertyChanged();
        }

        private void ExecuteBringToFrontCommand()
        {
            int selectedElementIndex = -1;

            for (int index = 0; index < GraphicalObjects.Count; index++)
            {
                GraphicalObjectViewModel x = GraphicalObjects[index];
                if (x.IsSelected == true)
                {
                    selectedElementIndex = index;
                    break; // // Beende die Schleife, sobald das erste ausgewählte Element gefunden wurde
                } // Ende if

            } // Ende for - Schleife

            // Prüfen, ob ein Element gefunden wurde und ob es ein rechtes Nachbarelement gibt
            if (selectedElementIndex != -1 && selectedElementIndex < GraphicalObjects.Count - 1)
            {
                // Tausche das ausgewählte Element mit dem rechten Nachbarn
                GraphicalObjectViewModel temp = GraphicalObjects[selectedElementIndex];
                GraphicalObjects[selectedElementIndex] = GraphicalObjects[selectedElementIndex + 1];
                GraphicalObjects[selectedElementIndex + 1] = temp;
            }

            RaisePropertyChanged();
        }

        private void ExecuteBringToBackwardCommand()
        {
            for (int index = 1; index < GraphicalObjects.Count; index++) // Start bei 1, damit es immer einen linken Nachbarn gibt
            {
                GraphicalObjectViewModel x = GraphicalObjects[index];

                if (x.IsSelected)
                {
                    // Tausche das aktuelle Objekt mit dem linken Nachbarn
                    (GraphicalObjects[index], GraphicalObjects[index - 1]) =
                        (GraphicalObjects[index - 1], GraphicalObjects[index]);

                    break; // Beende die Schleife nach dem ersten gefundenen Element
                }
            }

            RaisePropertyChanged();
        }

        public ICommand BringToFrontCommand { get; set; }

        public ICommand BringToBackwardCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public List<GraphicalObjectViewModel> GraphicalObjects { get; set; } = new List<GraphicalObjectViewModel>();

        public string ToolboxItemType { get; set; } = null;



    }

}