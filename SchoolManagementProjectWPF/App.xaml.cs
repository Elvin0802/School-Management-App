using SchoolManagementProjectWPF.DataBase;
using SchoolManagementProjectWPF.Models;
using SchoolManagementProjectWPF.ViewModels.ForPages;
using SchoolManagementProjectWPF.ViewModels.ForWindows;
using SchoolManagementProjectWPF.Views.Pages;
using SchoolManagementProjectWPF.Views.Windows;
using SimpleInjector;
using System.Windows;

namespace SchoolManagementProjectWPF;

public partial class App : Application
{
	public static Container? Container { get; set; } = new();

	private void StartSMS(object sender, StartupEventArgs e)
	{
		try
		{
			// read from DB
			DbOperations.ReadClassrooms(AppDbContex.School!);
			DbOperations.ReadTeachers(AppDbContex.School!);
			//------------

			RegisterOfViews();
			RegisterOfViewModels();

			var main = Container?.GetInstance<MainWindowView>();
			main!.DataContext = Container?.GetInstance<MainWindowViewModel>();

			var page = Container?.GetInstance<SignInPageView>();
			page!.DataContext = Container?.GetInstance<SignInPageViewModel>();

			main.MainFrame.Navigate(page);

			main.ShowDialog();
		}
		catch (Exception ex)
		{
			MessageBox.Show($"{ex.Message}", "Error in AppStart", MessageBoxButton.OK);
		}
	}

	public void RegisterOfViews()
	{
		Container?.RegisterSingleton<MainWindowView>();

		Container?.RegisterSingleton<AddClassroomPageView>();
		Container?.RegisterSingleton<AddExamPageView>();
		Container?.RegisterSingleton<AdminPageView>();
		Container?.RegisterSingleton<AllClassroomsPageView>();
		Container?.RegisterSingleton<MyExamsPageView>();
		Container?.RegisterSingleton<ShowClassroomPageView>();
		Container?.RegisterSingleton<SignInPageView>();
		Container?.RegisterSingleton<StudentPageView>();
		Container?.RegisterSingleton<StudentRegisterPageView>();
		Container?.RegisterSingleton<TeacherPageView>();
		Container?.RegisterSingleton<TeacherRegisterPageView>();

	}

	public void RegisterOfViewModels()
	{
		Container?.RegisterSingleton<MainWindowViewModel>();

		Container?.RegisterSingleton<AddClassroomPageViewModel>();
		Container?.RegisterSingleton<AddExamPageViewModel>();
		Container?.RegisterSingleton<AdminPageViewModel>();
		Container?.RegisterSingleton<AllClassroomsPageViewModel>();
		Container?.RegisterSingleton<MyExamsPageViewModel>();
		Container?.RegisterSingleton<ShowClassroomPageViewModel>();
		Container?.RegisterSingleton<SignInPageViewModel>();
		Container?.RegisterSingleton<StudentPageViewModel>();
		Container?.RegisterSingleton<StudentRegisterPageViewModel>();
		Container?.RegisterSingleton<TeacherPageViewModel>();
		Container?.RegisterSingleton<TeacherRegisterPageViewModel>();

	}
}
