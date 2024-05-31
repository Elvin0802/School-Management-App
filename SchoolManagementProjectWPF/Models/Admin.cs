namespace SchoolManagementProjectWPF.Models;

public class Admin : User
{

	public Admin(string? userName, string? email, string? password) : base()
	{
		Email = email;
		UserName = userName;
		Password = password;
	}


}
