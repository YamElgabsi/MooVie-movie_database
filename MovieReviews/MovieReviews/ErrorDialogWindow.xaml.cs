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
    /// Interaction logic for ErrorDialogWindow.xaml
    /// </summary>
    public partial class ErrorDialogWindow : Window
    {
        public bool ConfirmChanges { get; private set; }
        public ErrorDialogWindow(string message, string title, string trueVal, string falseVal)
        {
            InitializeComponent();

            label_ErrorMessage.Content = message;

            textBlock_title.Text = title;
            btnCancel.Content = falseVal;
            btnConfirm.Content = trueVal;
        }

        public ErrorDialogWindow(string message, string title, string ok) : this(message,title,ok,ok)
        {
            btnCancel.Visibility = Visibility.Hidden;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            ConfirmChanges = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ConfirmChanges = false;
            Close();
        }
    }
}
