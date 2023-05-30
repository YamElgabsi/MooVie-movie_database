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
    /// Interaction logic for CompanyWindow.xaml
    /// </summary>
    public partial class CompanyWindow : Window
    {
        User user;
        Company company;
        UserService userService = UserService.Instance;
        string image;
        bool isCreate = false;
        public CompanyWindow(User user, Company company)
        {
            InitializeComponent();
            this.user = user;
            this.company = company;

            if (company == null)
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

                image = company.Logo;
                if (image != "")
                {
                    image_view.Source = UserService.Base64ToImage(image);
                }
            }

        }

        void textBoxTextFill()
        {
            textbox_name.Text = company.Name;
            textbox_date.Text = company.Founded.ToString("dd/MM/yyyy");
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

            Company new_company = new Company(textbox_name.Text, image, date);
            bool isCreated = await userService.CreateCompany(new_company);
            if (isCreated)
            {
                userService.ShowErrorBox("The Company Created", "Created!", "Close");
                this.Close();
            }
            else
            {
                userService.ShowErrorBox("The Company is not Created, try again", "Error!", "Close");
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
            bool isDeleted = await userService.DeleteCompany(company.Name);
            if (isDeleted)
            {
                userService.UpdateMoviesByDeletedCompany(company.Name);

                userService.ShowErrorBox($"The Company {company.Name} Deleted!");
                this.Close();
            }
            else
            {
                userService.ShowErrorBox($"The Company {company.Name} Not Deleted!");
                this.Close();
            }
        }

        private void btn_editmode_Click(object sender, RoutedEventArgs e)
        {
            btn_editmode.Visibility = Visibility.Hidden;
            btn_saveChenge.Visibility = Visibility.Visible;
            textBoxReadOnly(false);

        }

        private async void btn_saveChenge_Click(object sender, RoutedEventArgs e)
        {
            DateTime date;
            if (!DateTime.TryParseExact(textbox_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                userService.ShowErrorBox("the date is not valid (dd/MM/yyyy), try other!", "DateFormatEror!", "OK");
                return;
            }

            Company new_company = new Company(textbox_name.Text, image, date);
            bool isCreated = await userService.CreateCompany(new_company);
            if (isCreated)
            {
                var x = await userService.UpdateMoviesByCompany(company.Name, textbox_name.Text);
                await userService.DeleteCompany(company.Name);
                

                userService.ShowErrorBox("The Company Updated", "Created!", "Close");
                this.Close();
            }
            else
            {
                userService.ShowErrorBox("The Company is not Updated, try again", "Error!", "Close");
            }


        }
    }
}
