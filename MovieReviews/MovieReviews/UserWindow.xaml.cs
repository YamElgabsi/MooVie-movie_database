using Microsoft.Win32;
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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private User user, userToView;
        private string orginal_username;
        private UserService userService = UserService.Instance;
        string image;
        bool isCreate = false;
        public UserWindow(User user, User userToView)
        {
            InitializeComponent();
            this.user = user;
            this.userToView = userToView;

            if (userToView != null)
            {
                if (user != null && (user.IsAdmin || user.Equals(userToView)))
                {
                    btn_editmode.Visibility = Visibility.Visible;
                    if(user.IsAdmin && !user.Equals(userToView))
                    {
                        btn_delete.Visibility = Visibility.Visible;
                    }
                }
                textBoxTextFill();

                image = userToView.Image;
                if (image != "")
                {
                    image_view.Source = UserService.Base64ToImage(image);
                }
            }
            else
            {
                //Create Mode
                btn_create.Visibility = Visibility.Visible;
                image = "";
                isCreate = true;
                textBoxReadOnly(false);

            }
           


            

        }

        void textBoxTextFill()
        {
            textbox_username.Text = userToView.Username;
            textbox_firstname.Text = userToView.FirstName;
            textbox_lastname.Text = userToView.LastName;
            textbox_email.Text = userToView.Email;
            textbox_date.Text = userToView.DateOfBirth.ToString("dd/MM/yyyy");
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

        private void textBoxReadOnly(bool visabilities)
        {
            textbox_username.IsReadOnly = visabilities;
            textbox_firstname.IsReadOnly = visabilities;
            textbox_lastname.IsReadOnly = visabilities;
            textbox_email.IsReadOnly = visabilities;
            textbox_date.IsReadOnly = visabilities;
        }

        private void btn_editmode_Click(object sender, RoutedEventArgs e)
        {
            btn_editmode.Visibility = Visibility.Hidden;
            btn_saveChenge.Visibility = Visibility.Visible;
            orginal_username = userToView.Username;
            textBoxReadOnly(false);
        }

        private async void btn_saveChenge_Click(object sender, RoutedEventArgs e)
        {
            bool confirmChanges;

            // Check if the API is connected
            if (userService.IsApiConnected)
            {

                string username = textbox_username.Text;
                string firstName = textbox_firstname.Text;
                string lastName = textbox_lastname.Text;
                string email = textbox_email.Text;


                // Check if any of the fields are empty
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(firstName) ||
                    string.IsNullOrEmpty(lastName))
                {
                    // Display an error message or perform the necessary actions
                    // when any of the fields are empty
                    confirmChanges = userService.ShowErrorBox("username, first name, last name  an not be emptey ", "Unfilled fields", "OK");
                    return;
                }
                DateTime date;
                bool success = DateTime.TryParseExact(textbox_date.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date);

                if (!success) {
                    confirmChanges = userService.ShowErrorBox("date in wrong Format (DD/MM/YYYY) ", "Error!", "OK");
                    return;
                }

                if (email != "" && !UserService.IsValidEmail(email))
                {
                    confirmChanges = userService.ShowErrorBox("email in wrong Format (example@moovie.com) or empty", "Error!", "OK");
                    return;
                }

                var user_1 = await userService.GetUserAsync(username);
                if (user_1 != null) {
                    userService.ShowErrorBox("the username is taken, try other!", "Error!", "OK");
                    return;
                }

                if(isCreate)
                {
                    User newUser = new User(username,firstName,lastName,email,username,date,false);
                    var created = await userService.CreateUserAsync(newUser);
                    if (created) {
                        userService.ShowErrorBox("Created!!", "", "OK");
                        return;
                    }
                    userService.ShowErrorBox("somthing got wrong, try again latar!", "Error!", "OK");
                    return;


                }

                User updatedUser = userToView;
                updatedUser.Username = username;
                updatedUser.FirstName = firstName;
                updatedUser.LastName = lastName;
                updatedUser.Email = email;
                updatedUser.DateOfBirth = date;
                updatedUser.Image = image;


                bool updated =  await userService.UpdateUserAsync(updatedUser, orginal_username);
                if (updated)
                {
                    textBoxReadOnly(true);
                    btn_editmode.Visibility = Visibility.Visible;
                    btn_saveChenge.Visibility = Visibility.Hidden; 
                    return;
                }
                // Show error message to the user and ask for confirmation
                confirmChanges = userService.ShowErrorBox("username already in use", "Error", "OK");

            }
            else
            {
                // Handle the case when the API connection is not available
                MessageBox.Show("BAPI connection is not available\nPlease try again leter!", "Error Message");
            }

        }

        public BitmapImage UploadBitmapImageFromComputer()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif) | *.jpg; *.jpeg; *.png; *.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;

                // Create a BitmapImage and set its source to the selected image path
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(selectedImagePath);
                bitmapImage.EndInit();

                return bitmapImage;
            }

            return null;
        }

        private void btn_image_Click(object sender, RoutedEventArgs e)
        {
            if( btn_saveChenge.Visibility == Visibility.Visible )
            {
                BitmapImage bitmapImage = UploadBitmapImageFromComputer();
                if (bitmapImage == null) return;
                image = UserService.ImageToBase64(bitmapImage);
                image_view.Source = bitmapImage;
            }

        }

        private async void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            bool isDeleted = await userService.DeleteUser(userToView.Username);
            if (isDeleted)
            {
                userService.ShowErrorBox($"The user {userToView.Username} Deleted!");
                this.Close();
            }
            else
            {
                userService.ShowErrorBox($"The user {userToView.Username} Not Deleted!");
                this.Close();
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }

}
