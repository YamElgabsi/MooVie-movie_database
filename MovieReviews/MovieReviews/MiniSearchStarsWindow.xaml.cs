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
    /// Interaction logic for MiniSearchStarsWindow.xaml
    /// </summary>
    public partial class MiniSearchStarsWindow : Window
    {
        UserService userService = UserService.Instance;
        int limit;
        public List<Star> SelectedStars { get; }
        List<Star> allStars = new List<Star>();

        public  MiniSearchStarsWindow(List<Star> stars, int limit)
        {
            InitializeComponent();

            this.limit = limit;
            this.SelectedStars = stars;  

            foreach (Star star1 in this.SelectedStars) listBox_Selected.Items.Add(star1);
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
            listBox_Rest.Items.Clear();
            var getS = await userService.GetStarsByNameStartingWithAsync(textbox_search.Text);
            this.allStars = getS.Except(this.SelectedStars).ToList();
            foreach (Star star in this.allStars) listBox_Rest.Items.Add(star);

        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_Rest.SelectedItem == null)   return;

            if(limit == SelectedStars.Count)
            {
                userService.ShowErrorBox($"You are have limit to {1} Star!");
                return;
            }

            listBox_Selected.Items.Add(listBox_Rest.SelectedItem);
            SelectedStars.Add(listBox_Rest.SelectedItem as Star);
            listBox_Rest.Items.Remove(listBox_Rest.SelectedItem); 
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_Selected.SelectedItem == null) return;

            Star rmv = (Star) listBox_Selected.SelectedItem;
            listBox_Selected.Items.Remove(rmv);
            SelectedStars.Remove(rmv);

        }
    }
}
