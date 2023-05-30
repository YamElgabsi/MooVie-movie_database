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
    /// Interaction logic for MiniSearchCompanyWindow.xaml
    /// </summary>
    public partial class MiniSearchCompanyWindow : Window
    {
        public Company? Company { get; set; }
        UserService userService = UserService.Instance; 
        public MiniSearchCompanyWindow(Company? company)
        {
            InitializeComponent();
            this.Company = company;
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

        private void listBox_Selected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedItem == null) return;

            this.Company = (Company) listBox.SelectedItem;

        }

        private async void btn_search_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            var companies = await userService.GetCompaniesByNameStartingWithAsyn(textbox_search.Text);
            foreach( var comp in companies ){
                listBox.Items.Add(comp);
                if(Company != null && Company.Name == comp.Name) listBox.SelectedItem = comp;
            }

        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            listBox.SelectedIndex = -1;
            Company= null;
        }
    }
}
