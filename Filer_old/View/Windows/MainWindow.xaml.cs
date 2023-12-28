using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using Microsoft.WindowsAPICodePack.Dialogs;
using CheckBox = System.Windows.Controls.CheckBox;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Filer2
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Directory.CreateDirectory($"{addresNew.Text}\\logs");
		}

		//выбор папки с файлами
		void Btn_adders_old_Click(object sender, RoutedEventArgs e)
		{
			var Address = addresOld;
			CompleteField(Address);
		}

		//выбор конечной папки для перемещения файлов
		void Btn_adders_new_Click(object sender, RoutedEventArgs e)
		{
			var Address = addresNew;
			CompleteField(Address);
		}


		private static void CompleteField(TextBlock Address)
		{
			try
			{
				CommonOpenFileDialog dialog = new()
				{
					InitialDirectory = Address.Text,
					IsFolderPicker = true
				};
				if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
				{
					Address.Text = dialog.FileName;
				}
			}
			catch(Exception e)
			{
				MessageBox.Show("Error!", e.Message);
			}
		}

		//список форматов полученных с помощью AddFiles
		private List<string> typeFiles = new();
		public List<string> TypeFiles { get => typeFiles; set => typeFiles = value; }

		#region форматы файлов что не стоит менять     https://open-file.ru/types/system/
		private static readonly List<string> BlockFormat = new()
	{
		"*.ini", "*.lnk", "*.0", "*.000", "*.1", "*.2", "*.208", "*.2fs", "*.3", "*.386", "*.3fs", "*.4", "*.5", "*.6", "*.7", "*.73u", "*.8", "*.89u", "*.8cu", "*.8xu", "*.adm", "*.adml","*.admx", "*.adv", "*.aml", "*.ani", "*.aos", "*.asec", "*.atahd", "*.b83", "*.b84", "*.bashrc", "*.bbfw", "*.bcd", "*.bin", "*.bio", "*.bk1", "*.bk2", "*.blf", "*.bmk", ".bom", ".bud", ".c32","*.cab", "*.cap", "*.cat", "*.cdmp", "*.cgz", "*.chg", "*.chk", "*.chs", "*.cht", "*.ci", "*.clb", "*.cm0012", "*.cm0013", "*.cmo", "*.cnt", "*.cpi", "*.cpl", "*.cpq", "*.cpr", "*.crash", "*.cur", "*.dat", "*.desklink", "*.dev", "*.dfu", "*.diagcab", "*.diagcfg", "*.diagpkg", "*.dic", ".diffbase", "*.dimax", "*.dit", "*.dll", "*.dlx", "*.dmp", "*.dock", "*.drpm", "*.drv", "*.dss", "*.dthumb", "*.dub", "*.dvd", "*.dyc", "*.ebd", "*.edj", "*.efi", "*.efires", "*.elf", "*.emerald", "*.escopy", "*.etl", "*.evt", "*.evtx", "*.ffa", "*.ffl", "*.ffo", "*.ffx", "*.fid", "*.firm", "*.fl1", "*.flg", "*.fota", "*.fpbf", "*.ftf", "*.ftg", "*.ftr", "*.fts", "*.fx", "*.gmmp", "*.grl", "*.group", "*.grp", "*.h1s", "*.hcd", "*.hdmp", "*.help", "*.hhc", "*.hhk", "*.hiv", "*.hlp", "*.hpj", "*.hsh", "*.htt", "*.hve", "*.icl", "*.icns", "*.ico", "*.idi", "*.idx", "*.ifw", "*.im4p", "*.ime", "*.img3", "*.inf_loc", "*.ins", "*.ion", "*.ioplist", "*.ipod", "*.iptheme", "*.its", "*.ius", "*.jetkey", "*.job", "*.jpn", "*.kbd", "*.kc", "*.kdz", "*.kext", "*.key", "*.kl", "*.ko", "*.kor", "*.ks", "*.kwi", "*.lex", "*.lfs", "*.library-ms", "*.lm", "*.lnk", "*.localized", "*.lockfile", "*.log1", "*.log2", "*.lpd", "*.lpd", "*.lst", "*.manifest", "*.mapimail", "*.mbn", "*.mbr", "*.mdmp", "*.me", "*.mem", "*.menu", "*.mi4", "*.mlc", "*.mmv", "*.mod", "*.msc", "*.msp", "*.msstyle", "*.msstyles", "*.mtz", "*.mui", "*.mum", "*.mun", "*.mydocs", "*.nb0", "*.nbh", "*.nfo", "*.nls", "*.nlt", "*.nt", "*.ntfs", "*.odex", "*.ozip", "*.panic", "*.pat", "*.pck", "*.pdr", "*.pfx", "*.pid", "*.pit", "*.pk2", "*.plasmoid", "*.pnf", "*.pol", "*.ppd", "*.ppm", "*.prefpane", "*.prf", "*.pro", "*.profile", "*.prop", "*.prt", "*.ps1", "*.ps2", "*.pwl", "*.qky", "*.qvm", "*.rc1", "*.rc2", "*.rco", "*.rcv", "*.reg", "*.reglnk", "*.rfw", "*.rmt", "*.roku", "*.rs", "*.ruf", "*.rvp", "*.saver", "*.sb", "*.sbf", "*.sbn", "*.scap", "*.scf", "*.schemas", "*.scr", "*.sdb", "*.sdt", "*.sefw", "*.self", "*.service", "*.sfcache", "*.shd", "*.shsh", "*.shsh2", "*.sin", "*.so.0", "*.spl", "*.sprx", "*.spx", "*.sqm", "*.str", "*.swp", "*.sys", "*.ta", "*.tbres", "*.tco2", "*.tdz", "*.tha", "*.theme", "*.thumbnails", "*.timer", "*.trashes", "*.trashinfo", "*.trx_dll", "*.uce", "*.vdex", "*.vga", "*.vgd", "*.vx_", "*.vxd", "*.wdf", "*.wdgt", "*.webpnp", "*.wer", "*.wgz", "*.wlu", "*.wph", "*.wpx", "*.xfb", "*.xrm-ms",
	};
		#endregion

		#region добавление форматов в List
		private void AddFiles(object sender, RoutedEventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			if(((bool)checkBox.IsChecked) && (!TypeFiles.Contains(checkBox.Content)))
			{
				TypeFiles.Add((string)checkBox.Content);
			}
			else
				TypeFiles.Remove((string)checkBox.Content);
		}
		#endregion

		#region сканировать рабочую область и создать чекбоксы соответствующие файлам
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
		#endregion

		#region удаление файлов
		private void DeleteFile(object sender, RoutedEventArgs e)
		{
			if(TypeFiles.Count == 0)
				MessageBox.Show("Вы не выбрали форматы файлов!", "Ошибка 1");
			else
			{
				DialogResult dialogResult = MessageBox.Show(
					"Удалить файлы полностью?",
					"Удаление!", MessageBoxButtons.YesNo);

				if(dialogResult == System.Windows.Forms.DialogResult.Yes)
				{
					foreach(string format in TypeFiles)
					{
						string[] _files = Directory.GetFiles(addresOld.Text, format);
						foreach(string _file in _files)
						{
							File.Delete(_file);
						}
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
		#endregion

		#region перемещение файлов
		private void FailMove(object sender, RoutedEventArgs e)
		{
			if(addresNew.Text == "")
			{
				MessageBox.Show("Вы не выбрали путь до хранилища!", "Ошибка 2");
				return;
			}

			else if(TypeFiles.Count == 0)
			{
				MessageBox.Show("Вы не выбрали форматы файлов!", "Ошибка 1");
				return;
			}
			else
			{
				foreach(var format in TypeFiles)
				{
					string[] _files = Directory.GetFiles(addresOld.Text, format);
					foreach(string _file in _files)
					{
						string pathFail = addresNew.Text + "\\" + _file[(_file.LastIndexOf("\\") + 1)..];
						File.Move(@_file, pathFail);
					}
				}
				MessageBox.Show("Вы переместили выбранные файлы по следующему пути: " + addresNew.Text, "Перемещение!");
			}
		}
		#endregion
	}
}