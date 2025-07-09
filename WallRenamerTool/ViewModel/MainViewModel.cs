using System.ComponentModel;
using System.Windows.Input;
using WallRenamerTool.Commands;
using WallRenamerTool.Model;

namespace WallRenamerTool.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private double _heightThreshold;
        private string _namePrefix;
        private string _statusMessage;

        public double HeightThreshold
        {
            get => _heightThreshold;
            set { _heightThreshold = value; OnPropertyChanged(nameof(HeightThreshold)); }
        }

        public string NamePrefix
        {
            get => _namePrefix;
            set { _namePrefix = value; OnPropertyChanged(nameof(NamePrefix)); }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(nameof(StatusMessage)); }
        }

        public ICommand RenameWallsCommand { get; }

        public MainViewModel()
        {
            RenameWallsCommand = new RelayCommand(RenameWalls);
            HeightThreshold = 10.0;
            NamePrefix = "Renamed_";
        }

        private void RenameWalls()
        {
            var walls = WallService.GetWallsAboveHeight(HeightThreshold);
            int renamedCount = WallService.RenameWalls(walls, NamePrefix);
            StatusMessage = $"{renamedCount} wall(s) renamed.";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}