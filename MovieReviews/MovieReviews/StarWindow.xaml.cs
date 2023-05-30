using MovieReviews.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for StarWindow.xaml
    /// </summary>
    public partial class StarWindow : Window
    {
        User user;
        Star star;
        UserService userService = UserService.Instance;
        string image;
        bool isCreate = false;
        public StarWindow(User user, Star star)
        {
            InitializeComponent();
            

            if (star == null)
            {
                //Create mode
                btn_create.Visibility = Visibility.Visible;
                image = "";
                isCreate = true;
                textBoxReadOnly(false);

            }
            else
            {
                this.star = star;
                if (user != null && user.IsAdmin)
                {
                    btn_editmode.Visibility = Visibility.Visible;
                    btn_delete.Visibility = Visibility.Visible;

                }
                textBoxTextFill();

                image = star.Image;
                if (image != "")
                {
                    image_view.Source = UserService.Base64ToImage(image);
                }
            }

        }

        void textBoxTextFill()
        {
            textbox_name.Text = star.Name;
            textbox_date.Text = star.DateOfBirth.ToString("dd/MM/yyyy");
        }

        private void textBoxReadOnly(bool visabilities)
        {
            textbox_name.IsReadOnly = visabilities;
            textbox_date.IsReadOnly = visabilities;

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
            if (!DateTime.TryParseExact(textbox_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                userService.ShowErrorBox("the date is not valid (dd/MM/yyyy), try other!", "DateFormatEror!", "OK");
                return;
            }
            Star new_star = new Star(textbox_name.Text,date, image);
            bool isCreated = await userService.CreateStar(new_star);
            if (isCreated)
            {
                userService.ShowErrorBox("The Star Created", "Created!", "Close");
                this.Close();
            }
            else
            {
                userService.ShowErrorBox("The Star is not Created, try again", "Error!", "Close");
            }
        }

        private void btn_image_Click(object sender, RoutedEventArgs e)
        {
            if( btn_create.Visibility == Visibility.Visible || btn_saveChenge.Visibility == Visibility.Visible )
            {
                image = UserService.GetImageFromUser();
                image_view.Source = UserService.Base64ToImage(image);

            }
        }

        private async void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            bool isDeleted = await userService.DeleteStar(star.Id);
            if (isDeleted)
            {
                userService.UpdateMoviesByDeletedDirector(star.Id); // update mooovies that he directror
                userService.DeleteMovieStarsByStarID(star.Id);

                userService.ShowErrorBox($"The Star {star.Name} Deleted!");
                this.Close();
            }
            else
            {
                userService.ShowErrorBox($"The Star {star.Name} Not Deleted!");
                this.Close();
            }
        }
    }
}
