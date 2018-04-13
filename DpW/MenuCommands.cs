using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DpW
{
    public class MenuCommands
    {
        public static readonly RoutedUICommand Create = new RoutedUICommand
        (
                "Create", "Create", typeof(MenuCommands),
                new InputGestureCollection()
                {
                        new KeyGesture(Key.N, ModifierKeys.Control)
                }
        );
    }
}
