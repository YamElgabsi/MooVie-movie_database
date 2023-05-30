using MovieReviews.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using static System.Net.Mime.MediaTypeNames;

namespace MovieReviews
{
    /// <summary>
    /// Interaction logic for MovieWindow.xaml
    /// </summary>
    public partial class MovieWindow : Window
    {
        User user;
        Movie movie;
        UserService userService = UserService.Instance;
        string image;
        bool isCreate = false;
        Star? director;
        Company? company;
        List<Star> actors = new List<Star>();
        public MovieWindow(User user, Movie movie, Star? director = null, Company? company = null, List<Star>? actors = null)
        {
            InitializeComponent();
            this.user = user;
            this.movie = movie;

            this.company = company;
            this.director = director;

            if (movie == null)
            {
                //Create mode
                btn_create.Visibility = Visibility.Visible;
                image = "";
                isCreate = true;
                textBoxReadOnly(false);

            }
            else
            {
                if (user != null && user.IsAdmin)
                {
                    btn_editmode.Visibility = Visibility.Visible;
                    btn_delete.Visibility = Visibility.Visible;

                }
                textBoxTextFill();
                
                if(director != null)
                {
                    textBlock_director.Text = director.Name;
                    if(director.Image != "")
                    {
                        image_director.Source = UserService.Base64ToImage(director.Image);
                    }
                }

                if (company != null)
                {
                    textBlock_company.Text = company.Name;
                    if (company.Logo != "")
                    {
                        image_company.Source = UserService.Base64ToImage(company.Logo);
                    }
                }


                image = movie.Poster;
                if (image != "")
                {
                    image_view.Source = UserService.Base64ToImage(image);
                }

                this.actors = actors;

                
                



            }

        }

        void textBoxTextFill()
        {
            textbox_title.Text = movie.Title;
            textbox_releaseDate.Text = movie.ReleaseDate.ToString("dd/MM/yyyy");
        }

        private void textBoxReadOnly(bool visabilities)
        {
            textbox_title.IsReadOnly = visabilities;
            textbox_releaseDate.IsReadOnly = visabilities;

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

        private async void btn_create_Click(object sender, RoutedEventArgs e)
        {
            DateTime date;
            if (!DateTime.TryParseExact(textbox_releaseDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                userService.ShowErrorBox("the date is not valid (dd/MM/yyyy), try other!", "DateFormatEror!", "OK");
                return;
            }

            string director_id = "";
            if(director != null)
            {
                director_id = director.Id;
            }

            string company_name = "";
            if (company != null)
            {
                company_name = company.Name;
            }

            Movie new_movie = new Movie(textbox_title.Text,image,director_id , company_name, date);
            bool isCreated = await userService.CreateMovie(new_movie);
            if (isCreated)
            {

                foreach(var actor in actors)
                {
                    await userService.CreateActor(new MovieStar(actor.Id, new_movie.Id));
                }

                userService.ShowErrorBox("The Movie Created", "Created!", "Close");


                this.Close();
            }
            else
            {
                userService.ShowErrorBox("The Movie is not Created, try again", "Error!", "Close");
            }
        }

        private void btn_image_Click(object sender, RoutedEventArgs e)
        {
            if (btn_create.Visibility == Visibility.Visible || btn_saveChenge.Visibility == Visibility.Visible)
            {
                image = UserService.GetImageFromUser();
                image_view.Source = UserService.Base64ToImage(image);

            }
        }

        private async void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            bool isDeleted = await userService.DeleteMovie(movie.Id);
            if (isDeleted)
            {
                await userService.DeleteMovieStarsByMovieID(movie.Id);
                userService.ShowErrorBox($"The Movie {movie.Title} Deleted!");
                this.Close();
            }
            else
            {
                userService.ShowErrorBox($"The Movie {movie.Title} Not Deleted!");
                this.Close();
            }
        }

        private void btn_director_Click(object sender, RoutedEventArgs e)
        {
            if (btn_create.Visibility == Visibility.Visible || btn_saveChenge.Visibility == Visibility.Visible)
            {
                List<Star> starList = new List<Star>();

                if (director != null) starList.Add(director);

                MiniSearchStarsWindow miniSearchStarsWindow = new MiniSearchStarsWindow(starList, 1);
                miniSearchStarsWindow.ShowDialog();

                if(miniSearchStarsWindow.SelectedStars.Count > 0)
                {
                    director = miniSearchStarsWindow.SelectedStars[0];
                    textBlock_director.Text = director.Name;
                    if (director.Image != "") image_director.Source = UserService.Base64ToImage(director.Image);
                    else {
                        image_director.Source = new BitmapImage(new Uri("/Images/profile.jpg", UriKind.Relative));
                    }
                }
                else
                {
                    director= null;
                    textBlock_director.Text = "No Director";

                    image_director.Source = new BitmapImage(new Uri("/Images/profile.jpg", UriKind.Relative));


                }


            }
        }

        private void btn_company_Click(object sender, RoutedEventArgs e)
        {
            if (btn_create.Visibility == Visibility.Visible || btn_saveChenge.Visibility == Visibility.Visible)
            {
                MiniSearchCompanyWindow miniSearchCompanyWindow = new MiniSearchCompanyWindow(company);
                miniSearchCompanyWindow.ShowDialog();

                company = miniSearchCompanyWindow.Company;

                if (company != null)
                {
                    textBlock_company.Text = company.Name;

                    if (company.Logo != "") image_company.Source = UserService.Base64ToImage(company.Logo);
                    else
                    {
                        image_company.Source = new BitmapImage(new Uri("/Images/profile.jpg", UriKind.Relative));

                    }

                }
                else
                {
                    textBlock_company.Text = "No Company";

                    image_company.Source = new BitmapImage(new Uri("/Images/profile.jpg", UriKind.Relative));


                }

            }
            

        }

        private void btn_actors_Click(object sender, RoutedEventArgs e)
        {
            if (btn_create.Visibility == Visibility.Visible || btn_saveChenge.Visibility == Visibility.Visible)
            {
                ///EDIT MODE
                MiniSearchStarsWindow miniSearchStarsWindow = new MiniSearchStarsWindow(actors, 500);
                miniSearchStarsWindow.ShowDialog();

                actors = miniSearchStarsWindow.SelectedStars;

            }
            else
            {
                ActorsWindow actorsWindow = new ActorsWindow(actors);
                actorsWindow.ShowDialog();
                
            }

        }

        private async void btn_saveChenge_Click(object sender, RoutedEventArgs e)
        {
            DateTime date;
            if (!DateTime.TryParseExact(textbox_releaseDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                userService.ShowErrorBox("the date is not valid (dd/MM/yyyy), try other!", "DateFormatEror!", "OK");
                return;
            }

            string director_id = "";
            if (director != null)
            {
                director_id = director.Id;
            }

            string company_name = "";
            if (company != null)
            {
                company_name = company.Name;
            }

            Movie new_movie = new Movie(textbox_title.Text, image, director_id, company_name, date);
            bool isCreated = await userService.UpdateMovie(movie.Id, new_movie);
            if (isCreated)
            {
                userService.DeleteMovieStarsByMovieID(movie.Id);

                foreach (var actor in actors)
                {
                    await userService.CreateActor(new MovieStar(actor.Id, new_movie.Id));
                }

                userService.ShowErrorBox("The Movie Created", "Created!", "Close");


                this.Close();
            }
            else
            {
                userService.ShowErrorBox("The Movie is not Created, try again", "Error!", "Close");
            }
        }

        private void btn_editmode_Click(object sender, RoutedEventArgs e)
        {
            textBoxReadOnly(false);
            btn_saveChenge.Visibility = Visibility.Visible;
            btn_editmode.Visibility = Visibility.Hidden;

        }
    }
}
