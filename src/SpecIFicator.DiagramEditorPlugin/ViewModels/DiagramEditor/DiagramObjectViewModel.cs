using GalaSoft.MvvmLight.Command;
using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataModels.DiagramInterchange;
using MDD4All.SpecIF.DataModels.DiagramInterchange.DiagramDefinition;
using MDD4All.SpecIF.DataModels.DiagramMetadata;
using MDD4All.SpecIF.DataProvider.Contracts;
using MDD4All.SpecIF.ViewModels;
using MDD4All.SVG.DataModels;
using MDD4All.Xml.DataAccess;
using SpecIFicator.DiagramEditorPlugin.Converters;
using System.Windows.Input;

namespace SpecIFicator.DiagramEditorPlugin.ViewModels.DiagramEditor
{
    public class DiagramObjectViewModel : GraphicalObjectViewModel
    {

        private ShapeToDiagramObjectConverter _converter;

        public DiagramObjectViewModel(ISpecIfDataProviderFactory specIfDataProviderFactory,
                                      Key diagramObjectClassKey) 
        {
            SvgShape.Bounds = new Bounds();
            _converter = new ShapeToDiagramObjectConverter(specIfDataProviderFactory);

            ISpecIfMetadataReader specIfMetadataReader = specIfDataProviderFactory.MetadataReader;
            DiagramObjectClass = specIfMetadataReader.GetDiagramObjectClassByKey(diagramObjectClassKey);

            
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            AddDiagramObjectClassCommand = new RelayCommand(ExecuteAddDiagramObjectClassCommand);
        }

        public DiagramObjectClass DiagramObjectClass { get; set; }

        public ResourceViewModel MainResource { get; set; }

        public Shape SvgShape { get; set; } = new Shape();

        private string _svg = string.Empty;

        public string SVG
        {
            get
            {
                string result = "";

                if(State == ShapeEditor.EditState.None)
                {
                    _svg = _converter.ConvertDiagramObjectToSVG(DiagramObjectClass, this);
                }

                result = _svg;

                return result;
            }
        }

        public void SetDefaultSize()
        {
            if(DiagramObjectClass != null)
            {
                Bounds bounds = _converter.CalculateBounds(DiagramObjectClass);

                Width = bounds.Width;
                Height = bounds.Height;
            }
        }

        public override double X
        {
            get
            {
                return SvgShape.Bounds.X;
            }

            set
            {
                SvgShape.Bounds.X = value;
            }

        }

        public override double Y
        {
            get
            {
                return SvgShape.Bounds.Y;
            }

            set
            {
                SvgShape.Bounds.Y = value;
            }
        }

        

        public override double Width
        {
            get
            {

                return SvgShape.Bounds.Width;
            }

            set
            {
                SvgShape.Bounds.Width = value;
            }
        }

        public override double Height
        {
            get
            {
                return SvgShape.Bounds.Height;
            }
            set
            {
                SvgShape.Bounds.Height = value;
            }
        }

        public string CssClass
        {
            get
            {
                return "placeholderRectangle";
            }
        }


        public ICommand AddDiagramObjectClassCommand { get; private set; }

        private void ExecuteAddDiagramObjectClassCommand()
        {

        }
    }
}
