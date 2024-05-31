using SchoolManagementProjectWPF.Services;

namespace SchoolManagementProjectWPF.Models;

public class User : NotifyService
{
	public Guid? id;
	private string? userName;
	private string? email;
	private string? password;

	public Guid? ID { get => id; set { id=value; OnPropertyChanged(); } }
	public string? Email { get => email; set { email=value; OnPropertyChanged(); } }
	public string? Password { get => password; set { password=value; OnPropertyChanged(); } }
	public string? UserName { get => userName; set { userName=value; OnPropertyChanged(); } }

	public User()
	{
		ID = Guid.NewGuid();
	}

}
