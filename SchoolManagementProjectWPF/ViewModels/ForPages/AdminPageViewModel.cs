using SchoolManagementProjectWPF.Commands;
using SchoolManagementProjectWPF.DataBase;
using SchoolManagementProjectWPF.Models;
using SchoolManagementProjectWPF.ViewModels.Global;
using SchoolManagementProjectWPF.Views.Pages;
using SchoolManagementProjectWPF.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementProjectWPF.ViewModels.ForPages;

public class AdminPageViewModel : BaseViewModel
{
	public AdminPageViewModel()
	{
		Teachers = AppDbContex.School!.Teachers;

		ChangeThemeCommand=new RelayCommand(ChangeThemeColor);
		BackCommand = new RelayCommand(BackCommandExecute);

		AllClassroomsCommand = new RelayCommand(AllClassroomsCommandExecute, AllClassroomsCommandCanExecute);
		AddTeacherCommand = new RelayCommand(AddTeacherCommandExecute, AddTeacherCommandCanExecute);
		RemoveTeacherCommand = new RelayCommand(RemoveTeacherCommandExecute, ChangeTeacherCommandCanExecute);
		EditTeacherCommand = new RelayCommand(EditTeacherCommandExecute, ChangeTeacherCommandCanExecute);

		App.Container!.GetInstance<AdminPageView>().BaseListView.ItemsSource = Teachers;
	}

	private ObservableCollection<Teacher>? teachers;
	private int sortIndex;
	public ObservableCollection<Teacher>? Teachers { get => teachers; set { teachers=value; OnPropertyChanged(); } }
	public int SortIndex
	{
		get => sortIndex;
		set
		{
			try
			{
				sortIndex=value;
				OnPropertyChanged();
				SortStudents(value);
			}
			catch { MessageBox.Show("Error in Sorting."); }
		}
	}

	#region All Classrooms Command

	public ICommand AllClassroomsCommand { get; set; }

	public bool AllClassroomsCommandCanExecute(object? obj)
	{
		if (obj is not null) return true;
		return false;
	}
	public void AllClassroomsCommandExecute(object? obj)
	{
		try
		{
			var NextView = App.Container!.GetInstance<AllClassroomsPageView>();
			var NextViewModel = App.Container.GetInstance<AllClassroomsPageViewModel>();

			NextView!.DataContext = NextViewModel;

			MainWindowView? win = App.Container?.GetInstance<MainWindowView>();

			win?.Navigate(NextView);
		}
		catch { MessageBox.Show("Error in All Classrooms Command Execute.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
	}
	#endregion

	#region Add Teacher Command

	public ICommand AddTeacherCommand { get; set; }

	public bool AddTeacherCommandCanExecute(object? obj)
	{
		if (obj is not null) return true;
		return false;
	}
	public void AddTeacherCommandExecute(object? obj)
	{
		try
		{
			var NextView = App.Container!.GetInstance<TeacherRegisterPageView>();
			var NextViewModel = App.Container.GetInstance<TeacherRegisterPageViewModel>();

			Teacher newTc = new();

			NextViewModel!.isEdit = false;
			NextViewModel!.EditTeacher = newTc;
			NextViewModel!.CopyEditTeacher = new Teacher();

			NextView!.DataContext = NextViewModel;

			MainWindowView? win = App.Container?.GetInstance<MainWindowView>();

			win?.Navigate(NextView);
		}
		catch { MessageBox.Show("Error in Add Teacher Command Execute.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
	}

	#endregion

	#region Remove Teacher Command

	public ICommand RemoveTeacherCommand { get; set; }
	public void RemoveTeacherCommandExecute(object? obj)
	{
		try
		{
			if (App.Container!.GetInstance<AdminPageView>().BaseListView.SelectedValue is Teacher teacher)
			{
				if (teacher.ClassID is not null)
				{
					MessageBox.Show("Teacher not removed, because this teacher has a classroom. \n " +
					"Change classroom head teacher. After remove this teacher.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}

				AppDbContex.School!.Teachers!.Remove(teacher);

				DbOperations.WriteTeachers(AppDbContex.School!);

				MessageBox.Show("Teacher successfully removed.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
				throw new Exception();
		}
		catch { MessageBox.Show("Teacher not removed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
	}

	#endregion

	#region Edit Teacher Command

	public ICommand EditTeacherCommand { get; set; }
	public bool ChangeTeacherCommandCanExecute(object? obj)
	{
		if (App.Container!.GetInstance<AdminPageView>().BaseListView.SelectedValue is Teacher)
			return true;
		return false;
	}
	public void EditTeacherCommandExecute(object? obj)
	{
		try
		{
			var NextView = App.Container!.GetInstance<TeacherRegisterPageView>();
			var NextViewModel = App.Container.GetInstance<TeacherRegisterPageViewModel>();

			if (App.Container!.GetInstance<AdminPageView>().BaseListView.SelectedValue is Teacher teacher)
			{
				NextViewModel!.isEdit = true;
				NextViewModel!.EditTeacher = teacher;
				NextViewModel!.CopyEditTeacher = teacher.Clone() as Teacher;
				NextView!.DataContext = NextViewModel;

				MainWindowView? win = App.Container?.GetInstance<MainWindowView>();

				win?.Navigate(NextView);
			}
		}
		catch
		{
			MessageBox.Show("Error in Edit Teacher Command Execute.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	#endregion

	#region List Sort Function

	public void SortStudents(int index)
	{
		try
		{
			if (index == 0)
				Teachers = new(Teachers?.OrderBy(s => s.Name)!);
			else if (index == 1)
				Teachers = new(Teachers?.OrderBy(s => s.Surname)!);
			else if (index == 2)
				Teachers = new(Teachers?.OrderByDescending(s => s.Name)!);
			else if (index == 3)
				Teachers = new(Teachers?.OrderByDescending(s => s.Surname)!);

			App.Container!.GetInstance<AdminPageView>().BaseListView.ItemsSource = Teachers;
			App.Container!.GetInstance<AdminPageView>().BaseListView.Items.Refresh();
		}
		catch
		{
			MessageBox.Show("Exams not sorted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	#endregion

}
