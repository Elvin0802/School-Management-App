using SchoolManagementProjectWPF.Models;
using System.Collections.ObjectModel;

namespace SchoolManagementProjectWPF.DataBase;

public static class AppDbContex
{
	public static Admin? Admin { get; } = new("admin", "admin@gmail.com", "admin");
	public static School? School { get; set; } = new();
	public static ObservableCollection<string>? Subjects { get; set; } = new()
	{
		"Azerbaycan Dili",
		"Edebiyyat",
		"Fizika",
		"Kimya",
		"Riyaziyyat",
		"Cografiya",
		"Herbi",
		"Informatika",
		"Texnologiya",
		"Musiqi",
		"Tesviri Incesenet",
		"Idman",
		"Ingilis Dili",
		"Fransiz Dili",
		"Rus Dili",
		"Alman Dili",
		"STEAM",
	};


	//0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000


	public static User? GetUser(User? user)
	{
		try
		{
			if (user is null) return null;

			if (user.UserName == Admin?.UserName && user.Password==Admin!.Password) return Admin;

			User? obj = School?.GetTeacher(user.UserName!, user.Password!);

			if (obj is not null) return obj;

			foreach (var cr in School!.Classrooms!)
			{
				obj = cr.Students!.FirstOrDefault(st => st.UserName == user.UserName && st.Password == user.Password);

				if (obj is not null) return obj;
			}

			return obj;
		}
		catch
		{
			return null;
		}
	}
	public static bool CheckUserName(string? userName)
	{
		try
		{
			if (userName is null) return false;

			if (userName == Admin?.UserName) return true;
			if ((bool)School?.CheckTeacher(userName)!) return true;

			return false;
		}
		catch
		{
			return false;
		}
	}
}