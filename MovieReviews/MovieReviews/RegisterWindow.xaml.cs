using MovieReviews.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace MovieReviews
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        UserService userService;
        public RegisterWindow()
        {
            InitializeComponent();
            userService = UserService.Instance; // Access the UserService instance

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

        private async void btn_signup_Click(object sender, RoutedEventArgs e)
        {
            // Check if the API is connected
            if (userService.IsApiConnected)
            {
                string username = textbox_username.Text;
                string password = passwordbox_password.Password;
                string confirm_password = passwordbox_confirmPassword.Password;
                string firstName = textbox_firstname.Text;
                string lastName = textbox_lastname.Text;

                // Check if any of the fields are empty
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(confirm_password) || string.IsNullOrEmpty(firstName) ||
                    string.IsNullOrEmpty(lastName))
                {
                    // Display an error message or perform the necessary actions
                    // when any of the fields are empty
                    textBlock_error.Text = "Please fill in all the required fields.";
                    textBlock_success.Text = "";
                }
                // Check if the password and confirm password match
                else if (password != confirm_password)
                {
                    // Display an error message or perform the necessary actions
                    // when the password and confirm password don't match
                    textBlock_error.Text = "Password and confirm password do not match.";
                    textBlock_success.Text = "";

                }
                else
                {
                    // All fields are valid, proceed with further actions
                    // such as saving the data or performing additional processing

                    User user = new User(username, firstName, lastName, "", password, DateTime.Now, false);

                    bool isCreated = await userService.CreateUserAsync(user);
                    if (isCreated)
                    {
                        textBlock_error.Text = "";
                        textBlock_success.Text = $"The User {username} Created!";
                    }
                    else
                    {
                        textBlock_error.Text = $"The username {username} is already exist!\nPlease try other";
                        textBlock_success.Text = "";

                    }
                }


            }
            else
            {
                // Handle the case when the API connection is not available
                MessageBox.Show("BAPI connection is not available\nPlease try again leter!","Error Message");
            }
           






        }
    }
}
