using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Othello.Business.ApplicationService;
using Othello.Business.Domain.Model.Boards;
using Othello.Business.Domain.Model.Games;

namespace Othello.OthelloApp.Presentation.ViewModel
{
    public class HexagonalBoardViewModel : IBoardViewModel //: INotifyPropertyChanged
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
        public HexagonalBoardViewModel(Game game)
        {
            Board = new ObservableCollection<ObservableCollection<CellViewModel>>();
            Update(game);
        }

        //コンストラクタ
        public HexagonalBoardViewModel(OthelloApplicationService service)
        {
            this.service = service;
            PutStoneCommand = new DelegateCommand(
                    (param) => {
                        var cell = param as CellViewModel;
                        if (cell == null)
                            return;
                        //service.PutStone(cell.X, cell.Y, CurrentPlayer);
                        service.PutStone(cell.Position, CurrentPlayer);
                        Update();
                    },
                    (param) => {
                        var cell = param as CellViewModel;
                        if (cell == null)
                            return false;
                        var pos = cell.Position as HexagonalPosition; 

                        if(pos.Y < 0 || Board.Count <= pos.Y ||
                           pos.X < 0 || Board[pos.Y].Count <= pos.X)
                            return false;

                        return Board[pos.Y][pos.X].CellType == CellType.EmptyAndCanPut;
                    }
                );

            Board = new ObservableCollection<ObservableCollection<CellViewModel>>();
            Update();
        }

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

            var board = game.Board as HexagonalBoard;

            if (board == null)
                throw new InvalidDataException("Board type mismatch.");

            for(int y=0; y<board.Height; y++)
            {
                var row = new ObservableCollection<CellViewModel>();
                for (int x = 0; x<board.Width; x++)
                    row.Add(new CellViewModel() { CellType=CellType.NotCell, Position = new HexagonalPosition(x,y)});
                    //row.Add(new CellViewModel() { CellType=CellType.NotCell, X=x, Y=y});
                Board.Add(row);
            }

            foreach(var cell in board.GetAllCells())
            {
                var pos = cell.Position as HexagonalPosition;
                
                if(cell.Stone == null)
                    Board[pos.Y][pos.X].CellType = CellType.Empty;
                else
                {
                    Board[pos.Y][pos.X].CellType = CellType.PutStone;
                    Board[pos.Y][pos.X].StoneType = cell.Stone;
                }
            }

            foreach(var cell in game.GetCanPutCellsOfCurrentTurn())
            {
                var pos = cell.Position as HexagonalPosition;
                Board[pos.Y][pos.X].CellType = CellType.EmptyAndCanPut;
            }

            CurrentPlayer = game.CurrentTurn;

            //通知
            PutStoneCommand?.RaiseCanExecuteChanged();
            RaisePropertyChanged(null);

        }
    }
}
