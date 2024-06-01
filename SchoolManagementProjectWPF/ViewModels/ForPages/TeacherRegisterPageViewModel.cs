using SchoolManagementProjectWPF.Commands;
using SchoolManagementProjectWPF.DataBase;
using SchoolManagementProjectWPF.Models;
using SchoolManagementProjectWPF.Services;
using SchoolManagementProjectWPF.ViewModels.Global;
using SchoolManagementProjectWPF.Views.Pages;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementProjectWPF.ViewModels.ForPages;

public class TeacherRegisterPageViewModel : BaseViewModel
{
	public TeacherRegisterPageViewModel()
	{
		ChangeThemeCommand=new RelayCommand(ChangeThemeColor);
		BackCommand = new RelayCommand(BackCommandExecute);

		VerifyEmailCommand = new RelayCommand(VerifyEmailCommandExecute, VerifyEmailCommandCanExecute);

		SaveCommand = new RelayCommand(SaveCommandExecute, SaveCommandCanExecute);
		ChangePPCommand = new RelayCommand(ChangePPExecute);
	}

	private Teacher? copyEditTeacher;
	private Teacher? editTeacher;

	public bool isEdit;
	public string? verifyCode;
	public string? sendedCode;

	public Notification? VerifyNotification { get; set; } = new() { HeaderText="School Management Email Vertification System" };
	public Teacher? CopyEditTeacher { get => copyEditTeacher; set { copyEditTeacher = value; OnPropertyChanged(); } }
	public Teacher? EditTeacher { get => editTeacher; set { editTeacher = value; OnPropertyChanged(); } }
	public string? VerifyCode { get => verifyCode; set { verifyCode = value; OnPropertyChanged(); } }


	#region Verify Email Command

	public ICommand VerifyEmailCommand { get; set; }
	private bool VerifyEmailCommandCanExecute(object? obj)
	{
		if (CheckService.CheckEmail(CopyEditTeacher!.Email))
			return true;
		return false;
	}
	private void VerifyEmailCommandExecute(object? obj)
	{
		try
		{
			sendedCode = Random.Shared.Next(1001, 9998).ToString();

			VerifyNotification!.Message = $"Your Verify Code : {sendedCode}";

			Network.SendNotificationToEmail(VerifyNotification, CopyEditTeacher!.Email!);
			MessageBox.Show("Code Sended.");
		}
		catch { MessageBox.Show("Error in Code Sender"); }
	}

	#endregion

	#region Save Command

	public ICommand SaveCommand { get; set; }
	private bool SaveCommandCanExecute(object? obj)
	{
		if (CopyEditTeacher is not null)
			if (CheckService.CheckPerson(CopyEditTeacher))
				if (AppDbContex.CheckUserName(CopyEditTeacher.UserName) == false)
					if (CopyEditTeacher.Email == EditTeacher?.Email || (!(string.IsNullOrEmpty(VerifyCode)) && verifyCode == sendedCode))
						return true;

		return false;
	}
	private void SaveCommandExecute(object? obj)
	{
		try
		{
			EditTeacher?.SetValue(CopyEditTeacher!);

			if (!isEdit)
				AppDbContex.School!.Teachers!.Add(EditTeacher!);

			DbOperations.WriteTeachers(AppDbContex.School!);

			App.Container!.GetInstance<AdminPageView>().BaseListView.Items.Refresh();

			VerifyCode = "";

			MessageBox.Show("Teacher Added.");
		}
		catch
		{
			MessageBox.Show("Error in Teacher Save");
		}

		BackCommandExecute(obj);
	}

	#endregion

	#region Change PP Command

	public ICommand ChangePPCommand { get; set; }
	private void ChangePPExecute(object? obj)
	{
		try { CopyEditTeacher!.ProfilePicturePath = GetImage(); }
		catch { MessageBox.Show("Error in Change PP"); }
	}

	#endregion


}

