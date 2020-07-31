using Microsoft.Win32;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Security;
using System;
using System.IO;
using System.Windows;

namespace PDFTool
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pdf Files|*.pdf";
            if (openFileDialog.ShowDialog() == true)
            {
                filePath.Text = openFileDialog.FileName;
            }
        }

        private void password_TextChanged(object sender, RoutedEventArgs e)
        {
            //NOOP
        }

        private void EncryptButton(object sender, RoutedEventArgs e)
        {
            encrypt(filePath.Text, password.Text);
            ClearInput();
        }


        private void encrypt(String fileName, String password)
        {
            PdfDocument outputDocument = new PdfDocument();

            PdfDocument inputDocument = PdfReader.Open(fileName, PdfDocumentOpenMode.Import);

            int count = inputDocument.PageCount;
            for (int idx = 0; idx < count; idx++)
            {
                PdfPage page = inputDocument.Pages[idx];
                outputDocument.AddPage(page);
            }

            PdfSecuritySettings securitySettings = outputDocument.SecuritySettings;
            securitySettings.UserPassword = password;
            securitySettings.OwnerPassword = password;

            fileName = String.Format("{0}{1}{2}{3}{4}",
                            Path.GetDirectoryName(fileName),
                            "\\",
                            Path.GetFileNameWithoutExtension(fileName),
                            "-encrypted",
                            Path.GetExtension(fileName));

            outputDocument.Save(fileName);
        }


        private void ClearInput()
        {
            filePath.Text = "";
            password.Text = "";
        }
    }
}
