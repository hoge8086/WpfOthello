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

        public DelegateCommand PutStoneCommand { get; set; }
        public ObservableCollection<ObservableCollection<CellViewModel>> Board { get; set; }
        public StoneType CurrentPlayer { get; set; }

        //VisualBrush生成用のビューモデルコンストラクタ
        public OthelloBoardCtrlViewModel(Game game)
        {
            Board = new ObservableCollection<ObservableCollection<CellViewModel>>();
            Update(game);
        }

        //コンストラクタ
        public OthelloBoardCtrlViewModel(OthelloApplicationService service)
        {
            this.service = service;
            PutStoneCommand = new DelegateCommand(
                    (param) => {
                        var cell = param as CellViewModel;
                        if (cell == null)
                            return;
                        service.PutStone(cell.X, cell.Y, CurrentPlayer);
                        Update();
                    },
                    (param) => {
                        var cell = param as CellViewModel;
                        if (cell == null)
                            return false;

                        if(cell.Y < 0 || Board.Count <= cell.Y ||
                           cell.X < 0 || Board[cell.Y].Count <= cell.X)
                            return false;

                        return Board[cell.Y][cell.X].CellType == CellType.EmptyAndCanPut;
                    }
                );

            Board = new ObservableCollection<ObservableCollection<CellViewModel>>();
            Update();
        }

        //ViewModelの更新(とViewへの通知)
        public void Update(Game game = null)
        {
            if(game == null)
            {
                if (service == null)
                    throw new System.Exception();

                 game = service.GetGame();
            }

            //更新
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

            //通知
            PutStoneCommand?.RaiseCanExecuteChanged();
            RaisePropertyChanged(null);

        }
    }
}
