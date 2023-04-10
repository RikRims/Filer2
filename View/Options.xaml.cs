using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Filer2.View;
/// <summary>
/// Логика взаимодействия для Options.xaml
/// </summary>
public partial class Options : UserControl
{
	public Options()
	{
		InitializeComponent();
	}

	//выбор папки с файлами
	void Btn_addres_old_Click(object sender, RoutedEventArgs e)
	{
		var obj = this.addresOld;
		SelectDyrectory(obj);
	}

	//выбор конечной папки для перемещения файлов
	void Btn_addres_new_Click(object sender, RoutedEventArgs e)
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

}
