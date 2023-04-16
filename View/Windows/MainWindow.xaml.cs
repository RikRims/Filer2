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

		//    

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
