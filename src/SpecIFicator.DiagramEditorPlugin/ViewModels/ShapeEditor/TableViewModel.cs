using MDD4All.SpecIF.DataModels.DiagramMetadata;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels.ShapeEditor
{
    public class TableViewModel : GraphicalShapeObjectViewModel
    {
        public TableViewModel() : base(new TableShapePrimitive())
        {
        }
    }
}
