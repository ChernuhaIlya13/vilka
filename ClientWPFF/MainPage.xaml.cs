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

namespace ClientWPFF
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timeOfWork.Text = "9999";
            vilksDisappeared.Text = "9990";
            hasBeenUploadVilks.Text = "9990";
            canceledRepeatVilks.Text = "9990";
            canceledVilks.Text = "9990";
            countOfVilks.Text = "9990";
            vilksDisappeared.Text = "9990";


            MessageBox.Show("Запущен");
            
        }
    }
}
