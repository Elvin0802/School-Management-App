using SchoolManagementProjectWPF.Commands;
using SchoolManagementProjectWPF.DataBase;
using SchoolManagementProjectWPF.Models;
using SchoolManagementProjectWPF.ViewModels.Global;
using SchoolManagementProjectWPF.Views.Pages;
using SchoolManagementProjectWPF.Views.Windows;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementProjectWPF.ViewModels.ForPages;

public class StudentPageViewModel : BaseViewModel
{
	public StudentPageViewModel()
	{
		BackCommand = new RelayCommand(BackCommandExecute);
		ChangeThemeCommand = new RelayCommand(ChangeThemeColor);

		MyClassroomCommand = new RelayCommand(ClassroomCommandExecute);
		MyExamsCommand = new RelayCommand(MyExamsCommandExecute);
		EditMyDataCommand = new RelayCommand(EditMyDataCommandExecute);

	}

	private Student? currentStudent;
	public Student? CurrentStudent { get => currentStudent; set { currentStudent = value; OnPropertyChanged(); } }

	#region My Classroom Command
	public ICommand MyClassroomCommand { get; set; }
	public void ClassroomCommandExecute(object? param)
	{
		try
		{
			App.Container!.GetInstance<StudentPageView>().BaseListView.ItemsSource = AppDbContex.School!.GetClassroom(CurrentStudent!.ClassID!.Value)!.Students;
			App.Container!.GetInstance<StudentPageView>().BaseListView.Items.Refresh();
		}
		catch { MessageBox.Show("My Classroom Command Execute not worked.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
	}
	#endregion

	#region My Exams Command
	public ICommand MyExamsCommand { get; set; }
	public void MyExamsCommandExecute(object? param)
	{
		try
		{
			var NextView = App.Container!.GetInstance<MyExamsPageView>();
			var NextViewModel = App.Container.GetInstance<MyExamsPageViewModel>();

			NextViewModel!.CurrentStudent = CurrentStudent;
			NextView!.DataContext = NextViewModel;

			App.Container!.GetInstance<MyExamsPageView>().BaseListView.ItemsSource = CurrentStudent!.Exams;
			App.Container!.GetInstance<MyExamsPageView>().BaseListView.Items.Refresh();

			MainWindowView? win = App.Container?.GetInstance<MainWindowView>();

			win?.Navigate(NextView);
		}
		catch
		{
			MessageBox.Show("Error in EditMyDataCommandExecute for Studnet", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
	#endregion

	#region Edit My Data Command
	public ICommand EditMyDataCommand { get; set; }
	public void EditMyDataCommandExecute(object? param)
	{
		try
		{
			var NextView = App.Container!.GetInstance<StudentRegisterPageView>();
			var NextViewModel = App.Container.GetInstance<StudentRegisterPageViewModel>();

			NextViewModel!.isEdit = true;
			NextViewModel!.EditStudent = CurrentStudent;
			NextViewModel!.CopyEditStudent = CurrentStudent!.Clone() as Student;
			NextView!.DataContext = NextViewModel;

			MainWindowView? win = App.Container?.GetInstance<MainWindowView>();

			win?.Navigate(NextView);
		}
		catch
		{
			MessageBox.Show("Error in EditMyDataCommandExecute for Studnet", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
	#endregion

}
