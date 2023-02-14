using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Linq;

namespace Filer2
{
    public partial class MainWindow : Window
    {
        List<string> typeFiles = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            //зщаписываем номер версии программы в угол
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            textBloclVersion.Text = Convert.ToString(version);

            //по дефолту выбирается рабочий стол как рабочая область
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

        /// добавление форматов в List
        private void addFiles(string TF, bool? check)
        {
            if (check == true && !typeFiles.Contains(TF))
                typeFiles.Add(TF);
            else typeFiles.Remove(TF);
        }

        private void DeleteFile(object sender, RoutedEventArgs e)
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
        private void FailMowe(object sender, RoutedEventArgs e)
        {
            string pathDir;
            string nameDir = DateTime.Today.ToString();
            int found = nameDir.IndexOf(" ");
            string pathFile = System.IO.Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Filer2" + "/log/" + $"{nameDir.Substring(0, found)}.txt");
            using (StreamWriter log = File.Exists(pathFile) ? File.AppendText(pathFile) : File.CreateText(pathFile))
            {
                string text = ($"{nameDir} --- Операция Перемещения --- {typeFiles.Count} --- {check_path.IsChecked} --- {addresNew.Text}");
                log.WriteLine(text);
            }

            if (check_path.IsChecked == true)
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Filer2" + "/" + nameDir.Substring(0, found));
                pathDir = System.IO.Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Filer2" + "/" + nameDir.Substring(0, found));
            }
            else //ошибка 2
                if (addresNew.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Вы не выбрали путь до хранилища!", "Ошибка 2");
                return;
            }
            else pathDir = addresNew.Text;
            //ошибка 1
            if (typeFiles.Count == 0)
            { 
                System.Windows.Forms.MessageBox.Show("Вы не выбрали форматы файлов!", "Ошибка 1");
                return;
            }
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

        //сканировать рабочую область и создать чекбоксы соответствующие файлам
        private void ScanWorkPath(object sender, RoutedEventArgs e)
        {
            CheckBoxConteiner.Children.Clear();
            List<string> nameCheckBox = new List<string>();

            string[] _files = Directory.GetFiles(addresOld.Text);
            foreach (string _file in _files)
            {
                int begin = _file.LastIndexOf(".");
                nameCheckBox.Add(_file.Substring(begin));
            }

            IEnumerable<string> distinctNameCheckBox = nameCheckBox.Distinct();
            foreach (string _file in distinctNameCheckBox)
            {
                System.Windows.Controls.CheckBox checkBox = new();
                checkBox.Content = _file;
                CheckBoxConteiner.Children.Add(checkBox);
            }
        }
    }
}
