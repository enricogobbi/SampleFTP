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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using Microsoft.Win32;
//using System.Windows.Forms;

namespace FTP
{
    /// <summary>
    /// Logica di interazione per Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        TcpClient client;

        public Window1(TcpClient c)
        {
            InitializeComponent();

            client = c;
        }

        private void btn_Upload_Click(object sender, RoutedEventArgs e)
        {
            byte[] buffer;
            NetworkStream netStream;
            StreamReader sr = new StreamReader(txt_Local.Text);

            buffer = Encoding.ASCII.GetBytes(txt_Remote.Text + "," + sr.ReadToEnd());

            netStream = client.GetStream();

            netStream.Write(buffer, 0, buffer.Length);

            netStream.Close();
        }

        private void btn_Select_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if ((bool)dialog.ShowDialog())
            {
                try
                {
                    txt_Local.Text = dialog.FileName;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}");
                }
            }
        }
    }
}
