using SchoolManagementProjectWPF.Commands;
using SchoolManagementProjectWPF.DataBase;
using SchoolManagementProjectWPF.Models;
using SchoolManagementProjectWPF.ViewModels.Global;
using SchoolManagementProjectWPF.Views.Pages;
using SchoolManagementProjectWPF.Views.Windows;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementProjectWPF.ViewModels.ForPages;

public class ShowClassroomPageViewModel : BaseViewModel
{
	public ShowClassroomPageViewModel()
	{
		ChangeThemeCommand=new RelayCommand(ChangeThemeColor);
		BackCommand = new RelayCommand(BackCommandExecute);

		AddStudentCommand = new RelayCommand(AddStudentCommandExecute, AddStudentCommandCanExecute);
		RemoveStudentCommand = new RelayCommand(RemoveStudentCommandExecute, ChangeStudentCommandCanExecute);
		EditStudentCommand = new RelayCommand(EditStudentCommandExecute, ChangeStudentCommandCanExecute);
	}

	private Classroom? currentClassroom;
	private int sortIndex;

	public Classroom? CurrentClassroom { get => currentClassroom; set { currentClassroom = value; OnPropertyChanged(); } }
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


	#region Add Student Command

	public ICommand AddStudentCommand { get; set; }

	public bool AddStudentCommandCanExecute(object? obj)
	{
		if (obj is not null) return true;
		return false;
	}
	public void AddStudentCommandExecute(object? obj)
	{
		var NextView = App.Container!.GetInstance<StudentRegisterPageView>();
		var NextViewModel = App.Container.GetInstance<StudentRegisterPageViewModel>();

		Student newSt = new();

		NextViewModel!.isEdit = false;
		NextViewModel!.EditStudent = newSt;
		NextViewModel!.CopyEditStudent = new Student();
		NextViewModel!.ClassId = CurrentClassroom!.ID;

		NextView!.DataContext = NextViewModel;

		MainWindowView? win = App.Container?.GetInstance<MainWindowView>();

		win?.Navigate(NextView);
	}

	#endregion

	#region Remove Student Command

	public ICommand RemoveStudentCommand { get; set; }
	public void RemoveStudentCommandExecute(object? obj)
	{
		try
		{
			if (App.Container!.GetInstance<ShowClassroomPageView>().BaseListView.SelectedValue is Student st)
			{
				AppDbContex.School!.GetClassroom(st.ClassID!.Value)!.Students!.Remove(st);

				DbOperations.WriteClassrooms(AppDbContex.School!);

				MessageBox.Show("Student successfully removed.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
				throw new Exception();
		}
		catch { MessageBox.Show("Student not removed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
	}

	#endregion

	#region Edit Student Command

	public ICommand EditStudentCommand { get; set; }
	public bool ChangeStudentCommandCanExecute(object? obj)
	{
		if (App.Container!.GetInstance<ShowClassroomPageView>().BaseListView.SelectedValue is Student)
			return true;
		return false;
	}
	public void EditStudentCommandExecute(object? obj)
	{
		var NextView = App.Container!.GetInstance<StudentRegisterPageView>();
		var NextViewModel = App.Container.GetInstance<StudentRegisterPageViewModel>();

		if (App.Container!.GetInstance<ShowClassroomPageView>().BaseListView.SelectedValue is Student teacher)
		{
			NextViewModel!.isEdit = true;
			NextViewModel!.EditStudent = teacher;
			NextViewModel!.CopyEditStudent = teacher.Clone() as Student;
			NextView!.DataContext = NextViewModel;

			MainWindowView? win = App.Container?.GetInstance<MainWindowView>();

			win?.Navigate(NextView);
		}
	}

	#endregion


	#region List Sort Function

	public void SortStudents(int index)
	{
		try
		{
			if (index == 0)
				CurrentClassroom!.Students = new(CurrentClassroom!.Students?.OrderBy(s => s.Name)!);
			else if (index == 1)
				CurrentClassroom!.Students = new(CurrentClassroom!.Students?.OrderBy(s => s.Surname)!);
			else if (index == 2)
				CurrentClassroom!.Students = new(CurrentClassroom!.Students?.OrderByDescending(s => s.Name)!);
			else if (index == 3)
				CurrentClassroom!.Students = new(CurrentClassroom!.Students?.OrderByDescending(s => s.Surname)!);

			App.Container!.GetInstance<AdminPageView>().BaseListView.ItemsSource = CurrentClassroom!.Students;
			App.Container!.GetInstance<AdminPageView>().BaseListView.Items.Refresh();
		}
		catch
		{
			MessageBox.Show("Students not sorted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	#endregion


}
