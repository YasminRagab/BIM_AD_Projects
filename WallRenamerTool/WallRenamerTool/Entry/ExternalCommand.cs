using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using System.Windows;

namespace WallRenamerTool.Entry
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ExternalCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Assign the current document to the Singleton instance
            Singleton.Instance.Doc = commandData.Application.ActiveUIDocument.Document;

            // Initialize the UI window and set the data context to the ViewModel
            var window = new View.MainWindow
            {
                DataContext = new ViewModel.MainViewModel()
            };

            // Show the window as a modal dialog
            window.ShowDialog();

            return Result.Succeeded;
        }
    }
}
