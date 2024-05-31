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
	public Student? CurrentStudent { get => currentStudent; set { currentStudent = value; OnPropertyChanged(); } }

	#region Take Exam Command

	public ICommand TakeExamCommand { get; set; }

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

}
