using MovieReviews.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
    /// Interaction logic for ActorsWindow.xaml
    /// </summary>
    public partial class ActorsWindow : Window
    {
        private List<Star> stars;
        private bool isDragging;
        private Point lastMousePosition;

        public ActorsWindow(List<Star> stars)
        {
            InitializeComponent();
            this.stars = stars;
            PopulateStars();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            lastMousePosition = e.GetPosition(this);
            CaptureMouse();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(this);
                double offsetX = currentPosition.X - lastMousePosition.X;
                double offsetY = currentPosition.Y - lastMousePosition.Y;

                Left += offsetX;
                Top += offsetY;

                lastMousePosition = currentPosition;
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            ReleaseMouseCapture();
        }

        private void PopulateStars()
        {
            foreach (var star in stars)
            {
                // Create a StackPanel to hold the image and text side by side
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(10) // Adjust the margin as needed
                };

                // Create an Image control for the star's image
                Image image = new Image
                {
                    Source = UserService.Base64ToImage(star.Image),
                    Width = 100, // Adjust the size as needed
                    Height = 100,
                    Stretch = Stretch.UniformToFill // Adjust the stretch mode as needed
                };

                // Create a TextBlock for the star's name
                TextBlock textBlock = new TextBlock
                {
                    Text = star.Name,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 0, 0, 0) // Adjust the margin as needed
                };

                // Add the image and text to the stack panel
                stackPanel.Children.Add(image);
                stackPanel.Children.Add(textBlock);

                // Add the stack panel to the wrap panel
                wrapPanel.Children.Add(stackPanel);
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
    }
}
