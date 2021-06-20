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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientWPFF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ToolTip tooltip { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            tooltip = new ToolTip();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "admin" && Password.Password == "admin")
            {
                MainPage page = new MainPage();
                page.Show();
                Hide();
            }
            else 
            {
                Login.Background = Brushes.Aqua;
                Password.Background = Brushes.Aqua;
            }
            
        }
    }
}
