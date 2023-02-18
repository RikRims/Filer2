using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using CheckBox = System.Windows.Controls.CheckBox;
using Microsoft.VisualBasic.FileIO;
using System.Windows.Controls;

namespace Filer2
{
    public partial class MainWindow : Window
    {
        //список форматов полученых с помощью AddFiles
        private List<string> typeFiles = new();
        public List<string> TypeFiles { get => typeFiles; set => typeFiles = value; }

        //форматы файлов что не стоит менять
        private static readonly List<string> BlockFormat = new()
        {
            "*.ini", "*.lnk"
        };

        //теккущая дата-время
        readonly string nameDir = DateTime.Today.ToString();

        public MainWindow()
        {
            InitializeComponent();

            //зщаписываем номер версии программы в угол
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            textBloclVersion.Text = Convert.ToString(version);

            //извлечание даты из DateTime
            nameDir = nameDir[..(nameDir.IndexOf(" "))];

            //по дефолту выбирается рабочий стол и папка в "Документы" как рабочие области
            addresOld.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            addresNew.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Filer2\\" + nameDir;
            Directory.CreateDirectory($"{addresNew.Text}\\logs");
        }

        //выбор папки с файлами
        private void Btn_addres_old_Click(object sender, RoutedEventArgs e)
        {
            var obj = this.addresOld;
            SelectDyrectory(obj);
        }

        //выбор конечной папки для перемещения файлов
        private void Btn_addres_new_Click(object sender, RoutedEventArgs e)
        {
            var obj = this.addresNew;
            SelectDyrectory(obj);
            Directory.CreateDirectory($"{addresNew.Text}\\logs");
        }

        private static void SelectDyrectory(TextBlock laeble)
        {
            CommonOpenFileDialog dialog = new()
            {
                InitialDirectory = "C:",
                IsFolderPicker = true
            };
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                laeble.Text = dialog.FileName;
            }
        }


        /// добавление форматов в List                                                                   
        private void AddFiles(object sender, RoutedEventArgs e)
        {
            var chbox = sender as CheckBox;
            if(((bool)chbox.IsChecked) && (!TypeFiles.Contains(chbox.Content)))
            {
                TypeFiles.Add((string)chbox.Content);
            }
            else
                TypeFiles.Remove((string)chbox.Content);
        }

        //удаление файлов
        private void DeleteFile(object sender, RoutedEventArgs e)
        {
            if(TypeFiles.Count == 0)
                System.Windows.Forms.MessageBox.Show("Вы не выбрали форматы файлов!", "Ошибка 1");
            else
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(
                    "Удалить файлы полностью?",
                    "Удаление!", MessageBoxButtons.YesNo);
                if(dialogResult == System.Windows.Forms.DialogResult.Yes)

                    foreach(string format in TypeFiles)
                    {
                        string[] _files = Directory.GetFiles(addresOld.Text, format);
                        foreach(string _file in _files)
                        {
                            File.Delete(_file);
                        }
                    }

                else
                {
                    foreach(string format in TypeFiles)
                    {
                        string[] _files = Directory.GetFiles(addresOld.Text, format);
                        foreach(string _file in _files)
                        {
                            FileSystem.DeleteFile(@_file, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                        }
                    }
                }
            }
        }

        //перемещение файлов
        private void FailMowe(object sender, RoutedEventArgs e)
        {
            string pathFile = Path.GetFullPath($"{addresNew.Text}/logs/{nameDir}.txt");
            using(StreamWriter log = File.Exists(pathFile) ? File.AppendText(pathFile) : File.CreateText(pathFile))
            {
                string text = $"{nameDir} --- Операция Перемещения --- {TypeFiles.Count} --- {addresNew.Text}";
                log.WriteLine(text);
            }

            //ошибка 2
            if(addresNew.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Вы не выбрали путь до хранилища!", "Ошибка 2");
                return;
            }

            //ошибка 1
            else if(TypeFiles.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Вы не выбрали форматы файлов!", "Ошибка 1");
                return;
            }
            else
            {
                foreach(var format in TypeFiles)
                {
                    string[] _files = Directory.GetFiles(addresOld.Text, format);
                    foreach(string _file in _files)
                    {
                        string pathFale = addresNew.Text + "\\" + _file[(_file.LastIndexOf("\\") + 1)..];
                        File.Move(@_file, pathFale);
                    }
                }
                System.Windows.Forms.MessageBox.Show("Вы переместили выбранные файлы по следующему пути: " + addresNew.Text, "Перемещение!");
            }
        }

        //сканировать рабочую область и создать чекбоксы соответствующие файлам
        private void ScanWorkPath(object sender, RoutedEventArgs e)
        {
            CheckBoxConteiner.Children.Clear();
            TypeFiles.Clear();
            List<string> nameCheckBox = new();

            string[] _files = Directory.GetFiles(addresOld.Text);
            foreach(string _file in _files)
            {
                int begin = _file.LastIndexOf(".");
                nameCheckBox.Add($"*{_file[begin..]}");
            }
            nameCheckBox = nameCheckBox.Distinct().ToList();

            foreach(string bf in BlockFormat)
            {
                if(nameCheckBox.Contains(bf))
                {
                    nameCheckBox.Remove(bf);
                };
            }

            foreach(string _file in nameCheckBox)
            {
                CheckBox checkBox = new()
                {
                    Content = _file
                };
                checkBox.Click += AddFiles;
                CheckBoxConteiner.Children.Add(checkBox);
            }
        }

        //выбрать все чекбоксы
        protected internal void SelectAll(object sender, RoutedEventArgs e)
        {
            UIElementCollection checkBoxes = CheckBoxConteiner.Children;
            foreach(UIElement element in checkBoxes)
            {
                var obj = (element as CheckBox);
                obj.IsChecked = !obj.IsChecked;
                AddFiles(obj, null);
            }
        }
    }
}
