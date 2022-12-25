using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.Windows.Forms;

namespace Filer2
{
    public partial class MainWindow : Window
    {
        List<string> typeFiles = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            addresOld.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        //показать/спрятать выбор конечной папки для перемещения файлов
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (check_path.IsChecked == false)
                New_addres.Visibility = Visibility.Visible;
            else New_addres.Visibility = Visibility.Collapsed;
        }

        //выбор папки с файлами
        private void btn_addres_old_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                addresOld.Text = dialog.FileName;
            }
        }

        //выбор конечной папки для перемещения файлов
        private void btn_addres_new_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                addresNew.Text = dialog.FileName;
                check_path.IsEnabled = false;
            }
        }


        /// <summary>
        /// добавление форматов в List
        /// </summary>
        private void addFiles(string TF, bool? check)
        {
            if (check == true && !typeFiles.Contains(TF))
                typeFiles.Add(TF);
            else typeFiles.Remove(TF);
        }

        private void check_txt_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_txt.Content, check_txt.IsChecked);
        }

        private void check_png_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_png.Content, check_png.IsChecked);
        }

        private void check_jpg_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_jpg.Content, check_jpg.IsChecked);
        }

        private void check_jpeg_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_jpeg.Content, check_jpeg.IsChecked);
        }

        private void check_docx_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_docx.Content, check_docx.IsChecked);
        }

        private void check_xlsx_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_xlsx.Content, check_xlsx.IsChecked);
        }

        private void check_pdf_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_pdf.Content, check_pdf.IsChecked);
        }

        private void check_bmp_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_bmp.Content, check_bmp.IsChecked);
        }

        private void check_gif_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_gif.Content, check_gif.IsChecked);
        }

        private void check_tiff_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_tiff.Content, check_tiff.IsChecked);
        }

        private void check_inx_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_inx.Content, check_inx.IsChecked);
        }

        private void check_torrent_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_torrent.Content, check_torrent.IsChecked);
        }

        private void check_ai_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_ai.Content, check_ai.IsChecked);
        }

        private void check_mp4_Click(object sender, RoutedEventArgs e)
        {
            addFiles((string)check_mp4.Content, check_mp4.IsChecked);
        }
        /// <summary>
        /// конец добавления форматов
        /// </summary>

        //удаление файлов
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Это действие удаляет файлы полностью (не в корзину!). Продолжить?", "Удаление!", MessageBoxButtons.OKCancel);
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (typeFiles.Count == 0)
                    System.Windows.Forms.MessageBox.Show("Вы не выбрали форматы файлов!", "Ошибка 1");
                else foreach (var format in typeFiles)
                {
                    string[] _files = Directory.GetFiles(addresOld.Text, format);
                    foreach (string _file in _files)
                    {
                        File.Delete(_file);
                    }
                }
            }

        }

        //перемещение файлов
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string pathDir;
            if (check_path.IsChecked == true)
            {
                string nameDir = DateTime.Today.ToString();
                int found = nameDir.IndexOf(" ");
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Filer2" + "/" + nameDir.Substring(0, found));
                pathDir = System.IO.Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Filer2" + "/" + nameDir.Substring(0, found));
            }
            else pathDir = addresNew.Text;

            if (typeFiles.Count == 0)
                System.Windows.Forms.MessageBox.Show("Вы не выбрали форматы файлов!", "Ошибка 1");
            else
            {
                foreach (var format in typeFiles)
                {
                    string[] _files = Directory.GetFiles(addresOld.Text, format);
                    foreach (string _file in _files)
                    {
                        string pathFale = pathDir + "\\" + _file.Substring(_file.LastIndexOf("\\") + 1);
                        File.Move(@_file, pathFale);
                    }
                }
                System.Windows.Forms.MessageBox.Show("Вы переместили выбранные файлы по следующему пути: " + pathDir, "Перемещение!");
            }
        }
    }
}
