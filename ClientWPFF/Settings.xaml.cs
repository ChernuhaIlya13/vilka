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
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            SettingsFrame.Content = new CommonSettings();
        }

        
        private void SettingsFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void onClickCommonSettings(object sender, MouseButtonEventArgs e)
        {
            SettingsFrame.Content = new CommonSettings();
        }
        private void onClickBookmakers(object sender, MouseButtonEventArgs e)
        {
            SettingsFrame.Content = new Bookmakers();
        }


        private void onClickCurrency(object sender, MouseButtonEventArgs e)
        {
            SettingsFrame.Content = new Currency();
        }

        private void onClickRestrictions(object sender, MouseButtonEventArgs e)
        {
            SettingsFrame.Content = new Restrictions();
        }

        private void onClickNotification(object sender, MouseButtonEventArgs e)
        {
            SettingsFrame.Content = new Notifications();
        }

        private void onClickVilksAndBets(object sender, MouseButtonEventArgs e)
        {
            SettingsFrame.Content = new VilksAndBets();
        }

 
    }
}
