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
using System.IO;


namespace DodgeGame
{
    /// <summary>
    /// Interaction logic for PauseMenu.xaml
    /// </summary>
    ///
    public enum eChoice
    {
        choiceNone=0,
        choicePause=1,
        choiceSave=2,
        choiceExit=3
    }
    public partial class PauseMenu : Window
    {
        public eChoice choice = eChoice.choiceNone;
        public PauseMenu()
        {
            InitializeComponent();
            PauseMenuC.Focus();
        }

        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            choice = eChoice.choicePause;
            DialogResult = true;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
            choice = eChoice.choiceSave;
            DialogResult = true;
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                choice = eChoice.choicePause;
                DialogResult = true;
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            choice = eChoice.choiceExit;
            DialogResult = false;
        }
    }
}
