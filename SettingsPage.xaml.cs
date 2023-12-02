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

namespace printTester
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Window
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void timeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow.settings.LengthOfGame  = timeCombo.SelectedIndex switch
            {
                1 => 3000 * 60,
                2 => 5000 * 60,
                _ => 1000 * 60,
            };
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow.settings.RussianLanguage = languageCombo.SelectedIndex == 0;
        }
    }
}
