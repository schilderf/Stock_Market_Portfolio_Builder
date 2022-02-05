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

namespace Portfolio_Builder.Views
{
    /// <summary>
    /// Interaktionslogik für ConfirmPromptView.xaml
    /// </summary>
    public partial class ConfirmPromptView : Window
    {
        public ConfirmPromptView()
        {
            InitializeComponent();
        }
        private void JaEvent(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void NeinEvent(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
