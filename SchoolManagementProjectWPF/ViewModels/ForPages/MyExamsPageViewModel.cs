using SchoolManagementProjectWPF.Commands;
using SchoolManagementProjectWPF.DataBase;
using SchoolManagementProjectWPF.Models;
using SchoolManagementProjectWPF.ViewModels.Global;
using SchoolManagementProjectWPF.Views.Pages;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementProjectWPF.ViewModels.ForPages;

public class MyExamsPageViewModel : BaseViewModel
{
	public MyExamsPageViewModel()
	{
		BackCommand = new RelayCommand(BackCommandExecute);
		ChangeThemeCommand = new RelayCommand(ChangeThemeColor);
		TakeExamCommand=new RelayCommand(TakeExamCommandExecute);
	}

	private Student? currentStudent;
	private int sortIndex;

	public Student? CurrentStudent { get => currentStudent; set { currentStudent = value; OnPropertyChanged(); } }
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


	#region Take Exam Command

	public ICommand TakeExamCommand { get; set; }
	public bool TakeExamCommandCanExecute(object? obj)
	{
		if (App.Container!.GetInstance<MyExamsPageView>().BaseListView.SelectedValue is Exam exam)
		{
			if (exam.WritedTime is null)
				return true;
		}
		return false;
	}
	public void TakeExamCommandExecute(object? obj)
	{
		try
		{
			if (App.Container!.GetInstance<MyExamsPageView>().BaseListView.SelectedValue is Exam exam)
			{
				var result = Random.Shared.Next(1, exam.QuestionCount!.Value);
				exam.TakeExam(result);

				DbOperations.WriteClassrooms(AppDbContex.School!);

				MessageBox.Show($"Exam Taked.\nQuestion Count : {exam.QuestionCount!.Value} \n Correct Answer Count : {result}" +
							$" \n Your Exam Grade : {exam.ExamGrade}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
				throw new Exception();
		}
		catch { MessageBox.Show("Exam not taked.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
	}
	#endregion

	public void SortExams(int index)
	{
		try
		{
			if (index == 0)
				CurrentStudent!.Exams = new(CurrentStudent!.Exams?.OrderBy(s => s.Subject)!);
			else if (index == 1)
				CurrentStudent!.Exams = new(CurrentStudent!.Exams?.OrderBy(s => s.QuestionCount)!);
			else if (index == 2)
				CurrentStudent!.Exams = new(CurrentStudent!.Exams?.OrderBy(s => s.ExamGrade)!);
			else if (index == 3)
				CurrentStudent!.Exams = new(CurrentStudent!.Exams?.OrderByDescending(s => s.Subject)!);
			else if (index == 4)
				CurrentStudent!.Exams = new(CurrentStudent!.Exams?.OrderByDescending(s => s.QuestionCount)!);
			else if (index == 5)
				CurrentStudent!.Exams = new(CurrentStudent!.Exams?.OrderByDescending(s => s.ExamGrade)!);

			App.Container!.GetInstance<AdminPageView>().BaseListView.ItemsSource = CurrentStudent!.Exams;
			App.Container!.GetInstance<AdminPageView>().BaseListView.Items.Refresh();
		}
		catch
		{
			MessageBox.Show("Exams not sorted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

}
