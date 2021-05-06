using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

using Othello.Business.ApplicationService;
using Othello.Business.Domain.Utils;
using Othello.Business.Domain.Model.Games;
using Othello.Business.Domain.Model.Boards;
using System.ComponentModel;

namespace Othello.OthelloApp.Presentation.ViewModel
{
    public interface IBoardViewModel : INotifyPropertyChanged
    {
        void Update(Game game = null);
    }
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private OthelloApplicationService service = new OthelloApplicationService(new OthelloBoardFactoryImpl());

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Action<List<StoneType>> ShowResult { get; set; }
        public DelegateCommand RestartGameCommand { get; set; }

        public ObservableCollection<PlayerViewModel> Players { get; set; }
        //public OthelloBoardCtrlViewModel Board { get; set;}
        public IBoardViewModel _board;
        public IBoardViewModel Board
        {
            get { return _board; }
            set
            {
                _board = value;
                RaisePropertyChanged(nameof(Board));
            }
        }
        public StoneType CurrentPlayer { get; set; }

        public MainWindowViewModel()
        {
            Board = CreateBoardViewModel();
            Players = new ObservableCollection<PlayerViewModel>();

            //ボードが更新されたら、自身も更新する
            Board.PropertyChanged += (sender, arg) =>{ Update(); };
            //リスタートしたらボードを更新する
            RestartGameCommand = new DelegateCommand(
                    (param) => {
                        OthelloBoardType? boardType = param as OthelloBoardType?;
                        service.Restart(boardType);
                        Board = CreateBoardViewModel();
                        Board.PropertyChanged += (sender, arg) =>{ Update(); };
                        Update();
                    }
            );
            Board.Update();
        }

        public IBoardViewModel CreateBoardViewModel()
        {
            if (service.GetGame().Board is XYBoard)
            {
                return new XYBoardViewModel(service);
            }
            else if (service.GetGame().Board is HexagonalBoard)
            {
                return new HexagonalBoardViewModel(service);
            }
  
            throw new NotImplementedException("This board type is not implemented.");
        }

        public void Update()
        {
            var game = service.GetGame();

            CurrentPlayer = game.CurrentTurn;

            Players.Clear();
            var map = game.CountStones();
            foreach (var pair in map)
                Players.Add(new PlayerViewModel() { StoneType = pair.Key, StoneCount = pair.Value, IsCurrentTurn=(pair.Key == CurrentPlayer)});

            if(game.IsEnd)
            {
                if(ShowResult != null)
                {
                    var max = map.Max(x => x.Value);
                    var winner = map.Where(x => x.Value == max).Select(y => y.Key);
                    ShowResult(winner.ToList());
                }
            }
        }
    }
}
