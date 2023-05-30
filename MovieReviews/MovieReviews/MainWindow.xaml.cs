using MovieReviews.Models;
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

namespace MovieReviews
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User user;
        public MainWindow( User user )
        {
            InitializeComponent();
            if ( user == null )
            {
                MessageBox.Show("USER = NULL", "");
                this.Close();
            }
            this.user = user;
            if (!user.IsAdmin)
            {
                btn_users.Visibility = Visibility.Hidden;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        private void btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btn_profile_Click(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new UserWindow(user,user);  // Create an instance of the UserWindow
            userWindow.Closed += (s, args) => this.Show();  // Show the current window when UserWindow is closed
            userWindow.Show();  // Show the UserWindow
            this.Hide();  // Hide the current window

        }

     

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            string buttonName = clickedButton.Name;
            SearchType searchType;

            switch (buttonName)
            {
                case "btn_movies":           
                    searchType = SearchType.MOVIES;
                    break;
                case "btn_stars":
                    searchType = SearchType.STARS;
                    break;
                case "btn_companies":
                    searchType = SearchType.COMPANIES;
                    break;
                case "btn_users":
                    searchType = SearchType.USERS;
                    break;
                default:
                    return;
            }

            SearchWindow searchWindow = new SearchWindow(user, searchType);  // Create an instance of the SearchWindow
            searchWindow.Closed += (s, args) => this.Show();  // Show the current window when SearchWindow is closed
            searchWindow.Show();  // Show the SearchWindow
            this.Hide();  // Hide the current window



        }

    }
}
