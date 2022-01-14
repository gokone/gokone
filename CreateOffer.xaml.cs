using System.Windows;

namespace GokOne
{
    public partial class CreateOffer : Window
    {
        public CreateOffer()
        {
            InitializeComponent();
        }

        private void broadcast_pKey_Click(object sender, RoutedEventArgs e)
        {
            string hex = MainWindow.CreateOffer();

            MessageBox.Show(hex);
        }
    }
}
