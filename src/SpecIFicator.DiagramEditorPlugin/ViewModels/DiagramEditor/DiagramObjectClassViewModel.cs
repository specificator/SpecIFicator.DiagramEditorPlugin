using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataModels.DiagramMetadata;


namespace SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor
{
    public class DiagramObjectClassViewModel
    {
        

        public DiagramObjectClassViewModel(DiagramObjectClass diagramObjectClass) 
        {
            DiagramObjectClass = diagramObjectClass;

            
        }

        public DiagramObjectClass DiagramObjectClass { get; set; }

        public string Icon
        {
            get
            {
                return DiagramObjectClass.Icon;
            }

        }

        public string Title
        {
            get 
            { 
                return DiagramObjectClass.Title;
            }
        }

        public Key Key
        {
            get
            {
                return new Key(DiagramObjectClass.ID, DiagramObjectClass.Revision);
            }
        }

        public string ID
        {
            get
            {
                return DiagramObjectClass.ID;
            }
        }

        public string Revision
        {
            get
            {
                return DiagramObjectClass.Revision;
            }
        }

        public List<MultilanguageText> Description
        {
            get
            {
                return DiagramObjectClass.Description;
            }
        }

    }
}
