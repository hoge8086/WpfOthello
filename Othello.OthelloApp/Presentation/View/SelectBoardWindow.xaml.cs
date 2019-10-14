using Othello.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Othello.OthelloApp.Presentation.ViewModel;

namespace Othello.OthelloApp.Presentation.View
{
    /// <summary>
    /// SelectBoardWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectBoardWindow : Window
    {
        RestartGameCommand restartGameCommand;
        public SelectBoardWindow(RestartGameCommand restartGameCommand)
        {
            InitializeComponent();
            this.restartGameCommand = restartGameCommand;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            restartGameCommand.Execute(combo.SelectedItem);
            Close();
        }
    }
}
