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

public class TeacherPageViewModel : BaseViewModel
{
	public TeacherPageViewModel()
	{
		BackCommand = new RelayCommand(BackCommandExecute);
		ChangeThemeCommand = new RelayCommand(ChangeThemeColor);

		AddExamCommand = new RelayCommand(AddExamCommandExecute);
		RemoveExamCommand = new RelayCommand(RemoveExamCommandExecute);
		EditMyDataCommand = new RelayCommand(EditMyDataCommandExecute);
		MyClassroomCommand = new RelayCommand(ShowMyClassroomCommandExecute);
	}

	private Teacher? currentTeacher;
	private ObservableCollection<Exam>? exams;
	private int sortIndex;
	public Teacher? CurrentTeacher { get => currentTeacher; set { currentTeacher = value; OnPropertyChanged(); } }
	public ObservableCollection<Exam>? Exams { get => exams; set { exams = value; OnPropertyChanged(); } }
	public int SortIndex
	{
		get => sortIndex;
		set
		{
			try
			{
				sortIndex=value;
				OnPropertyChanged();
				SortExams(value);
			}
			catch { MessageBox.Show("Error in Sorting."); }
		}
	}



	#region Add Exam Command

	public ICommand AddExamCommand { get; set; }
	public bool CanExecute(object? obj)
	{
		if (obj is not null) return true;
		return false;
	}
	public void AddExamCommandExecute(object? obj)
	{
		var NextView = App.Container!.GetInstance<AddExamPageView>();
		var NextViewModel = App.Container.GetInstance<AddExamPageViewModel>();

		CheckClass();

		NextViewModel.Classroom = AppDbContex.School!.GetClassroom(CurrentTeacher!.ClassID!.Value);

		NextView!.DataContext = NextViewModel;

		MainWindowView? win = App.Container?.GetInstance<MainWindowView>();

		win?.Navigate(NextView);
	}

	#endregion

	#region My Classroom Command

	public ICommand MyClassroomCommand { get; set; }
	public void ShowMyClassroomCommandExecute(object? obj)
	{
		try
		{
			var NextView = App.Container!.GetInstance<ShowClassroomPageView>();
			var NextViewModel = App.Container.GetInstance<ShowClassroomPageViewModel>();

			CheckClass();

			if (AppDbContex.School!.GetClassroom(CurrentTeacher!.ClassID!.Value) is Classroom classroom)
			{
				NextViewModel!.CurrentClassroom = classroom;

				NextView!.DataContext = NextViewModel;

				App.Container!.GetInstance<ShowClassroomPageView>().BaseListView.ItemsSource = NextViewModel!.CurrentClassroom!.Students;
				App.Container!.GetInstance<ShowClassroomPageView>().BaseListView.Items.Refresh();

				MainWindowView? win = App.Container?.GetInstance<MainWindowView>();

				win?.Navigate(NextView);
			}
		}
		catch { MessageBox.Show("Error in EditMyDataCommandExecute for Teacher", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
	}

	#endregion

	#region Edit My Data Command

	public ICommand EditMyDataCommand { get; set; }

	public void EditMyDataCommandExecute(object? obj)
	{
		try
		{
			var NextView = App.Container!.GetInstance<TeacherRegisterPageView>();
			var NextViewModel = App.Container.GetInstance<TeacherRegisterPageViewModel>();

			NextViewModel!.isEdit = true;
			NextViewModel!.EditTeacher = CurrentTeacher;
			NextViewModel!.CopyEditTeacher = CurrentTeacher!.Clone() as Teacher;
			NextView!.DataContext = NextViewModel;

			MainWindowView? win = App.Container?.GetInstance<MainWindowView>();

			win?.Navigate(NextView);
		}
		catch
		{
			MessageBox.Show("Error in EditMyDataCommandExecute for Teacher", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	#endregion

	#region Remove Exam Command

	public ICommand RemoveExamCommand { get; set; }
	public void RemoveExamCommandExecute(object? obj)
	{
		try
		{
			if (App.Container!.GetInstance<TeacherPageView>().BaseListView.SelectedIndex is int index && index >= 0)
			{
				CheckClass();

				AppDbContex.School!.GetClassroom(CurrentTeacher!.ClassID!.Value)!.RemoveExam(Exams![index]);

				DbOperations.WriteTeachers(AppDbContex.School!);

				MessageBox.Show("Exam successfully removed.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else throw new Exception();
		}
		catch { MessageBox.Show("Exam not removed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
	}

	#endregion

	public void CheckClass()
	{
		if (CurrentTeacher!.ClassID is null)
		{
			MessageBox.Show("You are not Head Teacher", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
			return;
		}
	}

	public void SortExams(int index)
	{
		try
		{
			if (index == 0)
				Exams = new(Exams?.OrderBy(s => s.Subject)!);
			else if (index == 1)
				Exams = new(Exams?.OrderBy(s => s.QuestionCount)!);
			else if (index == 2)
				Exams = new(Exams?.OrderByDescending(s => s.Subject)!);
			else if (index == 3)
				Exams = new(Exams?.OrderByDescending(s => s.QuestionCount)!);

			App.Container!.GetInstance<AdminPageView>().BaseListView.ItemsSource = Exams;
			App.Container!.GetInstance<AdminPageView>().BaseListView.Items.Refresh();
		}
		catch
		{
			MessageBox.Show("Exams not sorted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

}
