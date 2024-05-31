using SchoolManagementProjectWPF.Models;
using System.Text.RegularExpressions;

namespace SchoolManagementProjectWPF.Services;

public class CheckService
{
	// Check Person object with regex. Return true if all data is correct. else false.
	public static bool CheckPerson(Person? p)
	{
		if (p == null) return false;

		if (CheckEmail(p.Email!) &&  CheckUserName(p.UserName!) && CheckPassword(p.Password!) && CheckText(p.Name!) && CheckText(p.Surname!)
			&& CheckText(p.FatherName!) && CheckAddress(p.Address!) && CheckDateTime(p.BirthDate) && CheckPhoneNumber(p.PhoneNumber!))
			return true;

		return false;
	}

	// Check name or surname or city with regex. Return true if correct. else false.
	public static bool CheckText(string? text)
	{
		if (text is null) return false;
		else
			return Regex.IsMatch(text, @"^(?=[A-Z])[A-Za-z]{2,49}$");
	}
	// Check name or surname or city with regex. Return true if correct. else false.
	public static bool CheckUserName(string? uName)
	{
		if (uName is null) return false;
		else
			return Regex.IsMatch(uName, @"^(?=[a-zA-Z])[-\w.]{4,31}([a-zA-Z\d]|(?<![-.])_)$");
	}
	// Check name or surname or city with regex. Return true if correct. else false.
	public static bool CheckAddress(string? uName)
	{
		if (uName is null) return false;
		else
			return Regex.IsMatch(uName, @"^(?:\d{1,5}\s)?[A-Za-z0-9'.,\s-]+$");
	}

	// Check email with regex. Return true if correct. else false.
	public static bool CheckEmail(string? email)
	{
		if (email is null) return false;
		else
			return Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
	}

	// Check password with regex. Return true if correct. else false.
	public static bool CheckPassword(string? password)
	{
		if (password is null) return false;
		else
			return Regex.IsMatch(password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?.!_@$%^&*-]).{6,31}$");
	}

	// Check Phone Number with regex. Return true if correct. else false.
	public static bool CheckPhoneNumber(string? PNumber)
	{
		if (PNumber is null) return false;
		else
			return Regex.IsMatch(PNumber, @"^(50|51|10|55|99|70|77)\s\d{3}\s\d{2}\s\d{2}$");
	}

	// Check Dtae Time with regex. Return true if correct ( min: 6, max: 66 ). else false.
	public static bool CheckDateTime(DateTime? dt)
	{
		if (dt is null) return false;
		else
			return dt!.Value.AddYears(6) < DateTime.Now && dt.Value.Year > DateTime.Now.AddYears(-66).Year;
	}
}