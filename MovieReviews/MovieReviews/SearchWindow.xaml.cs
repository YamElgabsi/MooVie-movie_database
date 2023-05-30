using MovieReviews.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public enum SearchType { ALL, STARS, USERS, MOVIES, COMPANIES };
    public partial class SearchWindow : Window
    {
  
        SearchType searchType;
        UserService userService = UserService.Instance;
        User user;
        public SearchWindow(User user, SearchType searchType)
        {
            InitializeComponent();
            this.searchType = searchType;
            this.user = user;

            if (user.IsAdmin) btn_create.Visibility = Visibility.Visible;

            switch (searchType)
            {
                case SearchType.MOVIES:
                    textBlock.Text += "Movies";
                    break;
                case SearchType.STARS:
                    textBlock.Text += "Stars";
                    break;
                case SearchType.COMPANIES:
                    textBlock.Text += "Companies";
                    break;
                default:
                    //else - USER
                    textBlock.Text += "Users";
                    break;
            }

            btn_search_Click(null,null);

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

        private async void btn_search_Click(object sender, RoutedEventArgs e)
        {
            switch (searchType)
            {
                case SearchType.MOVIES:
                    var movies = await userService.GetMoviesByNameStartingWithAsyn(textbox_search.Text);
                    listBox.Items.Clear();
                    foreach (var movie in movies)
                    {
                        listBox.Items.Add(movie);
                    }
                    break;
                case SearchType.STARS:
                    var stars = await userService.GetStarsByNameStartingWithAsync(textbox_search.Text);
                    listBox.Items.Clear();
                    foreach (var star in stars)
                    {
                        listBox.Items.Add(star);
                    }
                    break;
                case SearchType.COMPANIES:
                    var companies = await userService.GetCompaniesByNameStartingWithAsyn(textbox_search.Text);
                    listBox.Items.Clear();
                    foreach (var company in companies)
                    {
                        listBox.Items.Add(company);
                    }
                    break;
                default:
                    //else - USER
                    var users = await userService.GetUsersByUsernameStartingWithAsync(textbox_search.Text);
                    listBox.Items.Clear();
                    foreach (var user in users)
                    {
                        listBox.Items.Add(user);
                    }
                    break;
            }

            
        }

        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            Window window;
            switch (searchType)
            {
                case SearchType.MOVIES:
                    window = new MovieWindow(user, null);

                    break;
                case SearchType.STARS:
                    window = new StarWindow(user, null);

                    break;
                case SearchType.COMPANIES:
                    window = new CompanyWindow(user, null);
                    break;

                default:
                    //else - USER
                    window = new UserWindow(user, null);
                    break;
            }
            //UserWindow userWindow = new UserWindow(user, null);  // Create an instance of the UserWindow
            window.Closed += (s, args) => this.Show();  // Show the current window when UserWindow is closed
            window.Show();  // Show the UserWindow
            this.Hide();  // Hide the current window
        }

        private async void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listBox.SelectedIndex == -1) return;

            Window window;
            switch (searchType)
            {
                case SearchType.MOVIES:
                    Movie movieToView = listBox.SelectedItem as Movie;
                    
                    Star? director = null;
                    Company? company = null;

                    if (movieToView.Director_Id != "")
                    {
                        director = await userService.GetStarByIdAsync(movieToView.Director_Id);
                    }

                    if(movieToView.Company != "")
                    {
                        company = await userService.GetCompanyAsync(movieToView.Company);
                    }

                    var movieStars = await userService.GetMovieStarsByMovieId(movieToView.Id);
                    List<Star> actors = new List<Star>();
                    foreach (var movieStar in movieStars)
                    {
                        actors.Add(await userService.GetStarByIdAsync(movieStar.StarId));
                    }

                    window = new MovieWindow(user, movieToView, director, company,actors);

                    break;
                case SearchType.STARS:
                    Star starToView = listBox.SelectedItem as Star;
                    window = new StarWindow(user, starToView);

                    break;
                case SearchType.COMPANIES:
                    Company companyToView = listBox.SelectedItem as Company;
                    window = new CompanyWindow(user, companyToView);

                    break;

                default:
                    //else - USER
                    User userToView = listBox.SelectedItem as User;
                    window = new UserWindow(user, userToView);
                    break;
            }
            //UserWindow userWindow = new UserWindow(user, null);  // Create an instance of the UserWindow
            window.Closed += (s, args) => this.Show();  // Show the current window when UserWindow is closed
            this.Hide();
            window.ShowDialog();  // Show the UserWindow
            listBox.SelectedIndex = -1;
            btn_search_Click(sender, e);




        }
    }

}
