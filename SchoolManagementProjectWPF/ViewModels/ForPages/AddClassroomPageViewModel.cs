using SchoolManagementProjectWPF.Commands;
using SchoolManagementProjectWPF.DataBase;
using SchoolManagementProjectWPF.Models;
using SchoolManagementProjectWPF.ViewModels.Global;
using SchoolManagementProjectWPF.Views.Pages;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementProjectWPF.ViewModels.ForPages;

public class AddClassroomPageViewModel : BaseViewModel
{
	public AddClassroomPageViewModel()
	{
		BackCommand=new RelayCommand(BackCommandExecute);
		ChangeThemeCommand=new RelayCommand(ChangeThemeColor);

		SaveCommand = new RelayCommand(SaveCommandExecute, SaveCommandCanExecute);
	}

	private Classroom? copyEditClassroom;
	private Classroom? editClassroom;

	public Classroom? CopyEditClassroom { get => copyEditClassroom; set { copyEditClassroom = value; OnPropertyChanged(); } }
	public Classroom? EditClassroom { get => editClassroom; set { editClassroom = value; OnPropertyChanged(); } }
	public IEnumerable<Teacher>? emptyTeachers { get; set; }

	public bool isEdit;
	public int tIndex { get; set; }

	public void GetEmptyTeachers()
	{
		try
		{
			emptyTeachers = AppDbContex.School!.Teachers!.Where(t => t.ClassID is null);
			App.Container!.GetInstance<AddClassroomPageView>().TeachersCBox.ItemsSource = emptyTeachers;
			App.Container!.GetInstance<AddClassroomPageView>().TeachersCBox.Items.Refresh();
		}
		catch { MessageBox.Show("Error in get empty teachers."); }
	}


	#region Save Command

	public ICommand SaveCommand { get; set; }
	private bool SaveCommandCanExecute(object? obj)
	{
		if (CopyEditClassroom is not null && !(string.IsNullOrEmpty(CopyEditClassroom.Name)))
			return true;

		return false;
	}
	private void SaveCommandExecute(object? obj)
	{
		try
		{
			EditClassroom!.SetValue(CopyEditClassroom!);
			EditClassroom.HeadTeacher = emptyTeachers!.ElementAt(tIndex);

			AppDbContex.School!.Teachers!.First(t => t.ID == EditClassroom!.HeadTeacher.ID).SetOwnedClassroom(EditClassroom.ID!.Value);

			if (!isEdit)
				AppDbContex.School!.Classrooms!.Add(EditClassroom!);

			DbOperations.WriteClassrooms(AppDbContex.School!);
			DbOperations.WriteTeachers(AppDbContex.School!);

			MessageBox.Show("Proccess Successful.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
		}
		catch { MessageBox.Show("Error in Classroom Save"); }
		finally { BackCommandExecute(obj); }
	}

	#endregion

}
