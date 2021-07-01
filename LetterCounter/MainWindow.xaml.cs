using Microsoft.Win32;
using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LetterCounter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Files (*.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                SelectFileTextBox.Text = filename;
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectFileTextBox.Text != string.Empty)
            {
                SelectFilePage.Visibility = Visibility.Hidden;
                MainPage.Visibility = Visibility.Visible;
            }
            else
            {
                SelectFileLabel.Foreground = Brushes.Red;
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            TypeLabel.Foreground = Brushes.Black;
            string searchInput = SearchInputTextBox.Text;
            if (searchInput != "")
            {
                string fileText = File.ReadAllText(SelectFileTextBox.Text);
                int frequencyn = fileText.Length - fileText.Replace(searchInput.ToLower(), "").Length / searchInput.Length;
                int frequencyN = fileText.Length - fileText.Replace(searchInput.ToUpper(), "").Length / searchInput.Length;
                int frequencyNum = frequencyn + frequencyN;

                if (!char.IsLetter(char.Parse(searchInput)))
                {
                    frequencyNum /= 2;
                }

                FrequencyLabel.Content = string.Format("Frequency: {0}", frequencyNum);
                PercentageLabel.Content = string.Format("Percentage: {0}%", frequencyNum * 100 / fileText.Length);
            }
            else
            {
                TypeLabel.Foreground = Brushes.Red;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Visibility = Visibility.Hidden;
            SelectFilePage.Visibility = Visibility.Visible;
        }

        private void SearchInputTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SearchInputTextBox.Text = "";
        }
    }
}
