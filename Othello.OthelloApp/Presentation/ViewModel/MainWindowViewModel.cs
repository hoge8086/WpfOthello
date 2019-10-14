using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

using Othello.Business.ApplicationService;
using Othello.Business.Domain.Model;
using Othello.Business.Domain.Utils;

namespace Othello.OthelloApp.Presentation.ViewModel
{
    public class MainWindowViewModel //: INotifyPropertyChanged
    {
        private OthelloApplicationService service = new OthelloApplicationService(new OthelloBoardFactoryImpl());

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void RaisePropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}

        public OthelloBoardType CurrentBoardType { get; set; }
        public Action<List<StoneType>> ShowResult { get; set; }
        public RestartGameCommand RestartGameCommand { get; set; }
        public ObservableCollection<PlayerViewModel> Players { get; set; }
        public OthelloBoardCtrlViewModel Board { get; set;}
        public StoneType CurrentPlayer { get; set; }

        public MainWindowViewModel()
        {
            Board = new OthelloBoardCtrlViewModel(service);
            Players = new ObservableCollection<PlayerViewModel>();

            //ボードが更新されたら、自身も更新する
            Board.PropertyChanged += (sender, arg) => { Update(); };
            //リスタートしたらボードを更新する
            RestartGameCommand = new RestartGameCommand(service, Board.Update);
            Board.Update();
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
