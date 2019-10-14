using System.Collections.ObjectModel;
using System.ComponentModel;

using Othello.Business.ApplicationService;
using Othello.Business.Domain.Model;

namespace Othello.OthelloApp.Presentation.ViewModel
{
    public class OthelloBoardCtrlViewModel : INotifyPropertyChanged
    {
        private OthelloApplicationService service = null;

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public PutStoneCommand PutStoneCommand { get; set; }
        public ObservableCollection<ObservableCollection<CellViewModel>> Board { get; set; }
        public StoneType CurrentPlayer { get; set; }

        public OthelloBoardCtrlViewModel(OthelloApplicationService service)
        {
            this.service = service;
            PutStoneCommand = new PutStoneCommand(service, Update);

            Board = new ObservableCollection<ObservableCollection<CellViewModel>>();
            Update();
        }

        //VisualBrush生成用のビューモデルコンストラクタ
        public OthelloBoardCtrlViewModel(Game game)
        {
            Board = new ObservableCollection<ObservableCollection<CellViewModel>>();
            UpdateProperties(game);
        }

        public void Update()
        {
            UpdateProperties(service.GetGame());
        }

        public void UpdateProperties(Game game)
        {
            Board.Clear();
            for(int y=0; y<game.Board.Height; y++)
            {
                var row = new ObservableCollection<CellViewModel>();
                for (int x = 0; x<game.Board.Width; x++)
                    row.Add(new CellViewModel() { CellType=CellType.NotCell, X=x, Y=y});
                Board.Add(row);
            }

            foreach(var cell in game.Board.GetAllCells())
            {
                if(cell.Stone == null)
                    Board[cell.Position.Y][cell.Position.X].CellType = CellType.Empty;
                else
                {
                    Board[cell.Position.Y][cell.Position.X].CellType = CellType.PutStone;
                    Board[cell.Position.Y][cell.Position.X].StoneType = cell.Stone;
                }
            }

            foreach(var cell in game.GetCanPutCellsOfCurrentTurn())
            {
                Board[cell.Position.Y][cell.Position.X].CellType = CellType.EmptyAndCanPut;
            }

            CurrentPlayer = game.CurrentTurn;

            RaisePropertyChanged(null);

        }
    }
}
