using SchoolManagementProjectWPF.Commands;
using SchoolManagementProjectWPF.DataBase;
using SchoolManagementProjectWPF.Models;
using SchoolManagementProjectWPF.Services;
using SchoolManagementProjectWPF.ViewModels.Global;
using SchoolManagementProjectWPF.Views.Pages;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementProjectWPF.ViewModels.ForPages;

public class StudentRegisterPageViewModel : BaseViewModel
{
	public StudentRegisterPageViewModel()
	{
		ChangeThemeCommand=new RelayCommand(ChangeThemeColor);
		BackCommand = new RelayCommand(BackCommandExecute);

		VerifyEmailCommand = new RelayCommand(VerifyEmailCommandExecute, VerifyEmailCommandCanExecute);
		SaveCommand = new RelayCommand(SaveCommandExecute, SaveCommandCanExecute);
		ChangePPCommand = new RelayCommand(ChangePPExecute);
	}

	private Student? copyEditStudent;
	private Student? editStudent;
	private Guid? classId;
	private string? verifyCode;
	private string? sendedCode;
	public bool isEdit;

	public Notification? VerifyNotification { get; set; } = new() { HeaderText="School Management Email Vertification System" };
	public Student? CopyEditStudent { get => copyEditStudent; set { copyEditStudent = value; OnPropertyChanged(); } }
	public Student? EditStudent { get => editStudent; set { editStudent = value; OnPropertyChanged(); } }
	public Guid? ClassId { get => classId; set { classId = value; OnPropertyChanged(); } }
	public string? VerifyCode { get => verifyCode; set { verifyCode = value; OnPropertyChanged(); } }


	#region Verify Email Command

	public ICommand VerifyEmailCommand { get; set; }

	private bool VerifyEmailCommandCanExecute(object? obj)
	{
		if (CheckService.CheckEmail(CopyEditStudent!.Email))
			return true;
		return false;
	}
	private void VerifyEmailCommandExecute(object? obj)
	{
		try
		{

			MessageBox.Show($"{CopyEditStudent!.Email}");

			sendedCode = Random.Shared.Next(1001, 9998).ToString();

			VerifyNotification!.Message = $"Your Verify Code : {sendedCode}";

			Network.SendNotificationToEmail(VerifyNotification, CopyEditStudent!.Email!);
			MessageBox.Show("Code Sended.");
		}
		catch { MessageBox.Show("Error in Code Sender"); }
	}

	#endregion

	#region Save Command

	public ICommand SaveCommand { get; set; }
	private bool SaveCommandCanExecute(object? obj)
	{
		if (CopyEditStudent is not null)
			if (CheckService.CheckPerson(CopyEditStudent))
				if (AppDbContex.CheckUserName(CopyEditStudent.UserName) == false)
					if (CopyEditStudent.Email == EditStudent?.Email || (!(string.IsNullOrEmpty(VerifyCode)) && verifyCode == sendedCode))
						return true;

		return false;
	}
	private void SaveCommandExecute(object? obj)
	{
		try
		{
			EditStudent?.SetValue(CopyEditStudent!);

			if (!isEdit)
			{
				EditStudent!.SetOwnerClassroom(ClassId!.Value);
				AppDbContex.School?.AddStudentToClass(ClassId!.Value, EditStudent);

				App.Container!.GetInstance<ShowClassroomPageView>().BaseListView.Items.Refresh();

				DbOperations.WriteClassrooms(AppDbContex.School!);

				VerifyCode = "";

				MessageBox.Show("Student Added.");
			}
		}
		catch { MessageBox.Show("Error in Save Student"); }
		finally
		{
			BackCommandExecute(obj);
		}
	}

	#endregion

	#region Change PP Command

	public ICommand ChangePPCommand { get; set; }
	private void ChangePPExecute(object? obj)
	{
		try { CopyEditStudent!.ProfilePicturePath = GetImage(); }
		catch { MessageBox.Show("Error in Code Sender"); }
	}

	#endregion

}
