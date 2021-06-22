using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ClientWPFF
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        public TimeSpan TimeOfWork { get; set; } = TimeSpan.Zero;
        public static int CountCall { get; set; } = 0;

        public MainPage()
        {
            InitializeComponent();
            MainFrame.Content = new MainPagee();

        }

        //Очень плохой таймер(Создал фрейм для навигации,нужно будет переделать)

        /*public void Button_Click(object sender, RoutedEventArgs e)
        {
            var box = sender as Button;
            Timer timer = null;
            if ((string)box.Content == "Запустить")
            {
                LaunchButton.Content = "Остановить";

                vilksDisappeared.Text = "9990";
                hasBeenUploadVilks.Text = "9990";
                canceledRepeatVilks.Text = "9990";
                canceledVilks.Text = "9990";
                countOfVilks.Text = "9990";
                vilksDisappeared.Text = "9990";

                MessageBox.Show("Запущен");

                TimerCallback tm = new TimerCallback(TimerFy);
                // создаем таймер
                timer = new(tm, timeOfWork, 100, 1000);
            }
            else
            {
                LaunchButton.Content = "Запустить";
                MessageBox.Show("Остановлен =_=");
                if(timer != null) {
                    timer.Dispose();
                }
            }
        }
        public void TimerFy(object obj)
        {
            this.Dispatcher.Invoke(() =>
            {
                long.TryParse(timeOfWork.Text,out long res);
                res++;
                timeOfWork.Text = res.ToString();
            });
            
        }
        */

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if((string)btn.Content == "Запустить")
            {
                LaunchButton.Content = "Остановить";
            }
            else
            {
                LaunchButton.Content = "Запустить";
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
            Hide();
        }
        private void MainMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new MainPagee();
        }
    }
}
