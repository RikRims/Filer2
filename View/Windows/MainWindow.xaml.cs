using System.Reflection;
using System;
using System.Windows;
using System.Windows.Input;

namespace Filer2
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			//TODO вынести Resourse в отдельный файл
			InitializeComponent();
		}

		private void Border_MouseDown(object sender, MouseButtonEventArgs e)//перемещение окна
		{
			if(e.LeftButton == MouseButtonState.Pressed)
				DragMove();
		}

		#region обработка_кнопок_управляющих_окном 
		private void ButtonMinimazed_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}

		private void WindowStateButton_Click(object sender, RoutedEventArgs e)
		{
			if(Application.Current.MainWindow.WindowState != WindowState.Maximized)
				Application.Current.MainWindow.WindowState = WindowState.Maximized;
			else
				Application.Current.MainWindow.WindowState = WindowState.Normal;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
		#endregion

		//    Directory.CreateDirectory($"{addresNew.Text}\\logs");

		#region удаление файлов
		//private void DeleteFile(object sender, RoutedEventArgs e)
		//{
		//    if(TypeFiles.Count == 0)
		//        System.Windows.Forms.MessageBox.Show("Вы не выбрали форматы файлов!", "Ошибка 1");
		//    else
		//    {
		//        DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(
		//            "Удалить файлы полностью?",
		//            "Удаление!", MessageBoxButtons.YesNo);
		//        if(dialogResult == System.Windows.Forms.DialogResult.Yes)
		//            foreach(string format in TypeFiles)
		//            {
		//                string[] _files = Directory.GetFiles(addresOld.Text, format);
		//                foreach(string _file in _files)
		//                {
		//                    File.Delete(_file);
		//                }
		//            }
		//        else
		//        {
		//            foreach(string format in TypeFiles)
		//            {
		//                string[] _files = Directory.GetFiles(addresOld.Text, format);
		//                foreach(string _file in _files)
		//                {
		//                    FileSystem.DeleteFile(@_file, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
		//                }
		//            }
		//        }
		//    }
		//} 
		#endregion

		#region перемещение файлов
		//private void FailMowe(object sender, RoutedEventArgs e)
		//{
		//    string pathFile = Path.GetFullPath($"{addresNew.Text}/logs/{nameDir}.txt");
		//    using(StreamWriter log = File.Exists(pathFile) ? File.AppendText(pathFile) : File.CreateText(pathFile))
		//    {
		//        string text = $"{nameDir} --- Операция Перемещения --- {TypeFiles.Count} --- {addresNew.Text}";
		//        log.WriteLine(text);
		//    }
		//    ошибка 2
		//    if(addresNew.Text == "")
		//    {
		//        System.Windows.Forms.MessageBox.Show("Вы не выбрали путь до хранилища!", "Ошибка 2");
		//        return;
		//    }
		//    ошибка 1
		//    else if(TypeFiles.Count == 0)
		//    {
		//        System.Windows.Forms.MessageBox.Show("Вы не выбрали форматы файлов!", "Ошибка 1");
		//        return;
		//    }
		//    else
		//    {
		//        foreach(var format in TypeFiles)
		//        {
		//            string[] _files = Directory.GetFiles(addresOld.Text, format);
		//            foreach(string _file in _files)
		//            {
		//                string pathFale = addresNew.Text + "\\" + _file[(_file.LastIndexOf("\\") + 1)..];
		//                File.Move(@_file, pathFale);
		//            }
		//        }
		//        System.Windows.Forms.MessageBox.Show("Вы переместили выбранные файлы по следующему пути: " + addresNew.Text, "Перемещение!");
		//    }
		//}
		#endregion

		//выбрать все чекбоксы ПОПРАВИТЬ!!!
		//protected internal void SelectAll(object sender, RoutedEventArgs e)
		//{
		//    UIElementCollection checkBoxes = CheckBoxConteiner.Children;
		//    foreach(UIElement element in checkBoxes)
		//    {
		//        var obj = (element as CheckBox);
		//        if(obj.IsChecked == true)
		//        {
		//            obj.IsChecked = false;
		//            AddFiles(obj, null);
		//        }
		//        else
		//        {
		//            obj.IsChecked = true;
		//            AddFiles(obj, null);
		//        }
		//    }
		//}
	}
}
