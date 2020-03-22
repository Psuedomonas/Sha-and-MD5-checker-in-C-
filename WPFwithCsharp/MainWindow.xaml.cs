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
using System.Windows.Navigation;
using System.Windows.Shapes;
/*
 * Additional libraries for the open file dialog
 */
using Microsoft.Win32; // OpenFileDialog
using System.IO; // File keyword
using System.Security.Cryptography; //hash algorithms

namespace WPFwithCsharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /*
         * Vars
         */
        String strMD5;
        String strSHA1;
        String strSHA256;
        String strSHA512;
         /*
         * Copy MD5 to clipboard
         */
        private void btnCopyMD5_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(strMD5))
            {
                Clipboard.SetDataObject(strMD5);
            }

        }
        /*
         * Copy SHA1 to clipboard
         */
        private void btnCopySHA1_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(strSHA1))
            {
                Clipboard.SetDataObject(strSHA1);
            }
        }
        /*
        * Copy SHA256 to clipboard
        */
        private void btnCopySHA256_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(strSHA256))
            {
                Clipboard.SetDataObject(strSHA256);
            }
        }
        /*
         * Copy SHA512 to clipboard
         */
        private void btnCopySHA512_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(strSHA512))
            {
                Clipboard.SetDataObject(strSHA512);
            }
        }
        /*
        * Copy All checksums to clipboard
        */
        private void btnCopyAll_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(strMD5))
            {
                String strAll = strMD5 + strSHA1 + strSHA256 + strSHA512;
                Clipboard.SetDataObject(strAll);
            }
        }
        /*
         * Paste from clipboard, checks to see if usable data is in string
         */
        private void btnPaste(object sender, RoutedEventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                txtChecksum.Text = (String)iData.GetData(DataFormats.Text);
            }
        }
        /*
         * Verify Button
         */
        private void btnVerify_Click(object sender, RoutedEventArgs e)
        {
            if ((!String.IsNullOrEmpty(strMD5)) && (!String.IsNullOrEmpty(strSHA1)) && (!String.IsNullOrEmpty(strSHA256)) && (!String.IsNullOrEmpty(strSHA512)))
            {
                if (strMD5 == txtChecksum.Text)
                {
                    MessageBox.Show("MD5 Matched!", "Checksum Match", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (strSHA1 == txtChecksum.Text)
                {
                    MessageBox.Show("SHA1 Matched!", "Checksum Match", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (strSHA256 == txtChecksum.Text)
                {
                    MessageBox.Show("SHA256 Matched!", "Checksum Match", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (strSHA512 == txtChecksum.Text)
                {
                    MessageBox.Show("SHA512 Matched!", "Checksum Match", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No match to checksum", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        /*
         * Exit Menu item
         */
        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
        }
        /*
         * About Menu Item
         */
        private void mnuAbout_Click(object sender, RoutedEventArgs e)
        {

        }
        /*
         * Select File
         */
        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            String strFileName;

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.CheckFileExists)
                {
                    strFileName = openFileDialog.FileName;
                    txtFile.Text = strFileName;
                    GetChecksums(strFileName);
                }
            }
        }
        /*
         * Crunch the checksums
         */
        private void GetChecksums(String strFileName)
        {
            byte[] hash;
            //for some reason the 'using' operation has to be used seperate for eatch algorithm, or wrong checksum produced
            using (Stream stream = File.OpenRead(strFileName))
            {
                // MD5
                hash = MD5.Create().ComputeHash(stream);
                strMD5 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                strMD5 = strMD5.ToUpper();
                txtMD5.Text = strMD5;
            }
            using (Stream stream = File.OpenRead(strFileName))
            {
                // SHA1
                hash = SHA1.Create().ComputeHash(stream);
                strSHA1 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                strSHA1 = strSHA1.ToUpper();
                txtSHA1.Text = strSHA1;
            }
            using (Stream stream = File.OpenRead(strFileName))
            {
                // SHA256
                hash = SHA256.Create().ComputeHash(stream);
                strSHA256 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                strSHA256 = strSHA256.ToUpper();
                txtSHA256.Text = strSHA256;
            }
            using (Stream stream = File.OpenRead(strFileName))
            {
                // SHA512
                hash = SHA512.Create().ComputeHash(stream);
                strSHA512 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                strSHA512 = strSHA512.ToUpper();
                txtSHA512.Text = strSHA512;
            }

        }
    }
}
