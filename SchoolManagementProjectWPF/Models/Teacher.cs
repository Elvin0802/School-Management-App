using SchoolManagementProjectWPF.DataBase;

namespace SchoolManagementProjectWPF.Models;

public class Teacher : Person
{
	private Guid? cId = null;
	public Guid? ClassID { get => cId; set { cId=value; OnPropertyChanged(); } }
	public string? ClassName { get => GetClassName(); }

	public Teacher() : base()
	{

	}
	public Teacher(string? name, string? surname, string? fatherName, DateTime? birthDate,
				 int? gender, string? userName, string? email, string? password, string? address,
				 string? phoneNumber, string? profilePicturePath)
		 : base(name, surname, fatherName, birthDate, gender, userName, email, password, address, phoneNumber, profilePicturePath)
	{
	}

	public void SetOwnedClassroom(Guid ClassroomID)
	{
		cId = ClassroomID;
	}

	private string? GetClassName()
	{
		try
		{
			return AppDbContex.School!.GetClassroom(ClassID!.Value)!.Name!;
		}
		catch { return ""; }
	}

	public new object Clone()
	{
		return new Teacher
		{
			Name = this.Name,
			Surname = this.Surname,
			FatherName = this.FatherName,
			BirthDate = this.BirthDate,
			Gender = this.Gender,
			UserName = this.UserName,
			Email = this.Email,
			Password = this.Password,
			Address = this.Address,
			PhoneNumber = this.PhoneNumber,
			ProfilePicturePath = this.ProfilePicturePath
		};
	}

	public override string ToString() => $"{Name} / {UserName}";
}