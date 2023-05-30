using MovieReviews.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MovieReviews
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private UserService userService;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create an instance of the UserService class
            userService = UserService.Instance;

            // Additional code for application startup
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            // Stop the API connection checking when the application is shut down
            userService.StopCheckingApiConnection();
        }
    }
}
