using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using CheckBox = System.Windows.Controls.CheckBox;

namespace Filer2
{
    public partial class MainWindow : Window
    {
        private List<string> typeFiles = new();

        public MainWindow()
        {
            InitializeComponent();

            //зщаписываем номер версии программы в угол
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            textBloclVersion.Text = Convert.ToString(version);

            //по дефолту выбирается рабочий стол и папка в "Документы" как рабочие области
            addresOld.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            addresNew.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Filer2";
        }

        //выбор папки с файлами
        private void Btn_addres_old_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new()
            {
                InitialDirectory = "C:",
                IsFolderPicker = true
            };
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                addresOld.Text = dialog.FileName;
            }
        }

        //выбор конечной папки для перемещения файлов
        private void Btn_addres_new_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new()
            {
                InitialDirectory = "C:",
                IsFolderPicker = true
            };
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                addresNew.Text = dialog.FileName;
            }
        }

        /// добавление форматов в List                                                                   
        private void AddFiles(object sender, RoutedEventArgs e)
        {
            var chbox = sender as CheckBox;
            if(((bool)chbox.IsChecked) && (!typeFiles.Contains(chbox.Content)))
            {
                typeFiles.Add((string)chbox.Content);
            }
            else
                typeFiles.Remove((string)chbox.Content);
        }

        private void DeleteFile(object sender, RoutedEventArgs e)
        {
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Это действие удаляет файлы полностью (не в корзину!). Продолжить?", "Удаление!", MessageBoxButtons.OKCancel);
            if(dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if(typeFiles.Count == 0)
                    System.Windows.Forms.MessageBox.Show("Вы не выбрали форматы файлов!", "Ошибка 1");
                else
                    foreach(string format in typeFiles)
                    {
                        string[] _files = Directory.GetFiles(addresOld.Text, format);
                        foreach(string _file in _files)
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
            string pathFile = Path.GetFullPath($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Filer2/log/{nameDir[..found]}.txt");
            using(StreamWriter log = File.Exists(pathFile) ? File.AppendText(pathFile) : File.CreateText(pathFile))
            {
                string text = ($"{nameDir} --- Операция Перемещения --- {typeFiles.Count} --- {addresNew.Text}");
                log.WriteLine(text);
            }

            //ошибка 2
            if(addresNew.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Вы не выбрали путь до хранилища!", "Ошибка 2");
                return;
            }
            else
                pathDir = addresNew.Text;
            //ошибка 1
            if(typeFiles.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Вы не выбрали форматы файлов!", "Ошибка 1");
                return;
            }
            else
            {
                foreach(var format in typeFiles)
                {
                    string[] _files = Directory.GetFiles(addresOld.Text, format);
                    foreach(string _file in _files)
                    {
                        string pathFale = pathDir + "\\" + _file[(_file.LastIndexOf("\\") + 1)..];
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
            typeFiles.Clear();
            List<string> nameCheckBox = new();

            string[] _files = Directory.GetFiles(addresOld.Text);
            foreach(string _file in _files)
            {
                int begin = _file.LastIndexOf(".");
                nameCheckBox.Add($"*{_file[begin..]}");
            }

            IEnumerable<string> distinctNameCheckBox = nameCheckBox.Distinct();
            foreach(string _file in distinctNameCheckBox)
            {
                CheckBox checkBox = new()
                {
                    Content = _file
                };
                checkBox.Click += AddFiles;
                CheckBoxConteiner.Children.Add(checkBox);
            }
        }
    }
}
