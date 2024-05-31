using SchoolManagementProjectWPF.Commands;
using SchoolManagementProjectWPF.DataBase;
using SchoolManagementProjectWPF.Models;
using SchoolManagementProjectWPF.ViewModels.Global;
using SchoolManagementProjectWPF.Views.Pages;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementProjectWPF.ViewModels.ForPages;

public class AddExamPageViewModel : BaseViewModel
{
	public AddExamPageViewModel()
	{
		BackCommand = new RelayCommand(BackCommandExecute);
		ChangeThemeCommand = new RelayCommand(ChangeThemeColor);
		SaveExamCommand = new RelayCommand(SaveExamCommandExecute, CanExecute);

		App.Container!.GetInstance<AddExamPageView>().SubjectsCBox.ItemsSource = AppDbContex.Subjects!;
		App.Container!.GetInstance<AddExamPageView>().SubjectsCBox.Items.Refresh();
	}

	private Classroom? classroom;
	private int subjectIndex;
	private string? qCount;

	public int SubjectIndex { get => subjectIndex; set { subjectIndex = value; OnPropertyChanged(); } }
	public string? QCount { get => qCount; set { qCount = value; OnPropertyChanged(); } }
	public Classroom? Classroom { get => classroom; set { classroom = value; OnPropertyChanged(); } }


	#region Save Exam Command

	public ICommand SaveExamCommand { get; set; }
	public bool CanExecute(object? obj)
	{
		if (obj is not null) return true;
		return false;
	}
	public void SaveExamCommandExecute(object? obj)
	{
		try
		{
			Classroom!.AddExam(new(AppDbContex.Subjects![SubjectIndex], Convert.ToInt32(QCount), DateTime.Now));

			App.Container!.GetInstance<TeacherPageView>().BaseListView.Items.Refresh();

			MessageBox.Show("Exam Added", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
		}
		catch
		{
			MessageBox.Show("Exam Not Added.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
		finally
		{
			BackCommandExecute(obj);
		}
	}

	#endregion


}
