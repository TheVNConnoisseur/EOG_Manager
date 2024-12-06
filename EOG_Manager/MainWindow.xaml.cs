using Microsoft.Win32;
using NVorbis;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EOG_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OGG ogg;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Original_File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.DefaultExt = ".ogg";
            ofd.Filter = "Ogg|*.ogg";
            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                Button_Convert.IsEnabled = false;
                try
                {
                    byte[] audioData = File.ReadAllBytes(ofd.FileName);
                    ogg = new OGG(audioData);
                    if (ogg.Verify(44100))
                    {
                        Button_Convert.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Make sure that the file has a sample rate of 44.1KHz and is properly encoded to .ogg.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void Button_Convert_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "";
            sfd.DefaultExt = ".eog";
            sfd.Filter = "EOG|*.eog";
            Nullable<bool> result = sfd.ShowDialog();

            if (result == true)
            {
                try
                {
                    ogg.Convert(sfd.FileName);
                    MessageBox.Show("Conversion completed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during conversion: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Button_Convert.IsEnabled = false;
            }
        }

    }
}