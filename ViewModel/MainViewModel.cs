using System;
using System.Reflection;
using Filer2.View.Base;

namespace Filer2.ViewModel
{
	class MainViewModel : ViewModelBase
    {
		//теккущая дата-время
		public static string nameDir = DateTime.Today.ToString();
	    
		#region записываем_номер_версии_программы_в_угол
		private Version _version = Assembly.GetExecutingAssembly().GetName().Version;

		public Version Version
		{
			get => _version;
			set => Set(ref _version, value);
		}
		#endregion

		#region адреса для радоты с файлами
		private static string _addresOldText = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

		public string AddresOldText 
		{
			get => _addresOldText;
			set => Set(ref _addresOldText, value);
		}
		
		private string _addresNewText = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Filer2\\" + nameDir[..10];


		public string AddresNewText
		{
			get => _addresNewText;
			set => Set(ref _addresNewText, value);
		}

		#endregion

	}
}
