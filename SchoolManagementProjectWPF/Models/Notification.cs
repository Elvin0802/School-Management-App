using SchoolManagementProjectWPF.Services;

namespace SchoolManagementProjectWPF.Models;

public class Notification : NotifyService
{
	private DateTime? sendDate;
	private string? headerText;
	private string? message;
	public DateTime? SendDate { get => sendDate; set { sendDate = value; OnPropertyChanged(); } }
	public string? Message { get => message; set { message = value; OnPropertyChanged(); } }
	public string? HeaderText { get => headerText; set { headerText = value; OnPropertyChanged(); } }
	public Notification()
	{

	}
	public Notification(DateTime sendDate, string message, string headerText)
	{
		HeaderText=headerText;
		Message=message;
		SendDate=sendDate;
	}
	public override string ToString() => Message!;
}
