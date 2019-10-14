using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using Othello.OthelloApp.Presentation.ViewModel;

namespace Othello.OthelloApp.Presentation.View
{
    public class CellViewSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var cell = item as CellViewModel;

            if(cell == null ||
               cell.CellType == CellType.NotCell ||
               cell.CellType == CellType.EmptyAndCanPut ||
               cell.CellType == CellType.Empty)
            {
                return ((FrameworkElement)container).FindResource(cell.CellType.ToString()) as DataTemplate;
            }

            return ((FrameworkElement)container).FindResource("Stone") as DataTemplate;
        }
    }
}
