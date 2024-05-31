using Microsoft.Win32;
using SchoolManagementProjectWPF.Services;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SchoolManagementProjectWPF.ViewModels.Global;
public abstract class BaseViewModel : NotifyService
{
	#region Change Theme Command
	public ICommand ChangeThemeCommand { get; set; }
	public void ChangeThemeColor(object? obj)
	{
		var color = Application.Current.Resources["FrMode"];
		Application.Current.Resources["FrMode"] = Application.Current.Resources["BaMode"];
		Application.Current.Resources["BaMode"] = color;
	}
	#endregion

	#region Back Command
	public ICommand BackCommand { get; set; }
	public void BackCommandExecute(object? obj)
	{
		Page? page = obj as Page;
		if (page is not null && page.NavigationService.CanGoBack)
			page.NavigationService.GoBack();
	}
	#endregion

	protected string? GetImage()
	{
		var dialog = new OpenFileDialog();
		dialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

		if (dialog.ShowDialog() == true)
		{
			var originalFileName = dialog.FileName;
			using FileStream originalFile = new FileStream(originalFileName, FileMode.Open);

			var copyFileName =
				Directory.GetCurrentDirectory().Split("\\bin")[0]
				+ "\\Images\\ForUsers\\"
				+ Guid.NewGuid().ToString().Replace("-", "") + Random.Shared.Next(10000, 1000000000) + originalFileName.Split("\\").Last();

			using FileStream copyFile = new FileStream(copyFileName, FileMode.Create);
			originalFile.CopyTo(copyFile);
			copyFile.Close();

			return copyFileName;
		}
		return null;
	}

}
