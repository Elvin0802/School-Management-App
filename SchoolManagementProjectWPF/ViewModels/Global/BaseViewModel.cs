using Microsoft.Win32;
using SchoolManagementProjectWPF.Interfaces;
using SchoolManagementProjectWPF.Services;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SchoolManagementProjectWPF.ViewModels.Global;
public abstract class BaseViewModel : NotifyService, IGetImage
{
	#region Change Theme Command

	public ICommand ChangeThemeCommand { get; set; }
	public void ChangeThemeColor(object? obj)
	{
		try
		{
			var color = Application.Current.Resources["FrMode"];
			Application.Current.Resources["FrMode"] = Application.Current.Resources["BaMode"];
			Application.Current.Resources["BaMode"] = color;
		}
		catch
		{
			MessageBox.Show("Error in Get Change Theme Command", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
	#endregion

	#region Back Command

	public ICommand BackCommand { get; set; }
	public void BackCommandExecute(object? obj)
	{
		try
		{
			Page? page = obj as Page;
			if (page is not null && page.NavigationService.CanGoBack)
				page.NavigationService.GoBack();
		}
		catch
		{
			MessageBox.Show("Error in Get Back Command", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
	#endregion

	public string? GetImage()
	{
		try
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
		catch
		{
			MessageBox.Show("Error in Get Image", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return null;
		}
	}
}
