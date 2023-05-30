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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        UserService userService = UserService.Instance;
        public LoginWindow()
        {
            InitializeComponent();
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
            Application.Current.Shutdown();

        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();  // Create an instance of the RegisterWindow
            registerWindow.Closed += (s, args) => this.Show();  // Show the current window when RegisterWindow is closed
            registerWindow.Show();  // Show the RegisterWindow
            this.Hide();  // Hide the current window
        }

        private async void btn_login_Click(object sender, RoutedEventArgs e)
        {
            // Check if the API is connected
            if (userService.IsApiConnected)
            {
                string username = textbox_username.Text;
                string password = passwordbox_password.Password;

                // Check if any of the fields are empty
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                { 
                    // Display an error message or perform the necessary actions
                    // when any of the fields are empty
                    textBlock_error.Text = "Please fill in all the required fields.";
                }
                else
                {
                    // All fields are valid, proceed with further actions
                    // such as saving the data or performing additional processing

                    User user  = await userService.GetUserAsync(username);
                    
                    if(user == null)
                    {
                        //user by giving username not found
                        textBlock_error.Text = "username or password are incorrect.";
                    }
                    else
                    {
                        if(user.Password == password)
                        {
                            // Can Login
                            textBlock_error.Text = "";

                            MainWindow mainWindow = new MainWindow(user);  // Create an instance of the MainWindow
                            mainWindow.Closed += (s, args) => this.Show();  // Show the current window when MainWindow is closed
                            mainWindow.Show();  // Show the MainWindow
                            this.Hide();  // Hide the current window

                        }
                        else
                        {
                            textBlock_error.Text = "username or password are incorrect";
                        }
                    }
                }


            }
            else
            {
                // Handle the case when the API connection is not available
                MessageBox.Show("BAPI connection is not available\nPlease try again leter!", "Error Message");
            }

        }
    }
}
