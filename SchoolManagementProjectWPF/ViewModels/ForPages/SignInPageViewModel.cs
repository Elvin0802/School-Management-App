using SchoolManagementProjectWPF.Commands;
using SchoolManagementProjectWPF.DataBase;
using SchoolManagementProjectWPF.Models;
using SchoolManagementProjectWPF.ViewModels.Global;
using SchoolManagementProjectWPF.Views.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SchoolManagementProjectWPF.ViewModels.ForPages;

public class SignInPageViewModel : BaseViewModel
{
	public SignInPageViewModel()
	{
		ChangeThemeCommand=new RelayCommand(ChangeThemeColor);

		ConfirmCommand = new RelayCommand(ConfirmCommandExecute, ConfirmCommandCanExecute);
		CheckBoxCommand = new RelayCommand(ChangeThemeColor);

		User = new();
	}

	private User? user;
	public User? User { get => user; set { user=value; OnPropertyChanged(); } }

	#region Login Command
	public ICommand ConfirmCommand { get; set; }
	public ICommand CheckBoxCommand { get; set; }

	public bool ConfirmCommandCanExecute(object? obj)
	{
		if (obj is null) return false;
		return true;
	}
	public void ConfirmCommandExecute(object? obj)
	{
		object? o = AppDbContex.GetUser(User);

		Page? NextView = null;

		if (o is Student)
		{
			NextView = App.Container?.GetInstance<StudentPageView>();
			var NextViewModel = App.Container?.GetInstance<StudentPageViewModel>();

			NextViewModel!.CurrentStudent = o as Student;

			NextView!.DataContext = NextViewModel;
		}
		else if (o is Teacher)
		{
			NextView = App.Container?.GetInstance<TeacherPageView>();
			var NextViewModel = App.Container?.GetInstance<TeacherPageViewModel>();

			NextViewModel!.CurrentTeacher = o as Teacher;

			if (NextViewModel!.CurrentTeacher!.ClassID is not null)
				NextViewModel!.Exams = AppDbContex.School!.GetClassroom(NextViewModel.CurrentTeacher!.ClassID!.Value)?.GetExams();
			else
				NextViewModel!.Exams = new();

			App.Container!.GetInstance<TeacherPageView>().BaseListView.ItemsSource = NextViewModel!.Exams;
			App.Container!.GetInstance<TeacherPageView>().BaseListView.Items.Refresh();

			NextView!.DataContext = NextViewModel;
		}
		else if (o is Admin)
		{
			NextView = App.Container?.GetInstance<AdminPageView>();

			var NextViewModel = App.Container?.GetInstance<AdminPageViewModel>();

			NextView!.DataContext = NextViewModel;
		}
		else
		{
			MessageBox.Show("Wrong Data Entered !");
			return;
		}

		try
		{
			(obj as Page)!.NavigationService.Navigate(NextView);
		}
		catch
		{
			MessageBox.Show("Error in Change page");
		}

	}
	#endregion
}
