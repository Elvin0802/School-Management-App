namespace SchoolManagementProjectWPF.Models;

public class Person : User, ICloneable
{
	#region Private Fields

	private string? name;
	private string? surname;
	private string? fatherName;
	private DateTime? birthDate;
	private int? gender;
	private string? address;
	private string? phoneNumber;
	private string? profilePicturePath;

	#endregion

	#region Properties

	public string? Name { get => name; set { name=value; OnPropertyChanged(); } }
	public string? Surname { get => surname; set { surname=value; OnPropertyChanged(); } }
	public DateTime? BirthDate { get => birthDate; set { birthDate=value; OnPropertyChanged(); } }
	public int? Gender { get => gender; set { gender=value; OnPropertyChanged(); } }
	public string? FatherName { get => fatherName; set { fatherName=value; OnPropertyChanged(); } }
	public string? Address { get => address; set { address=value; OnPropertyChanged(); } }
	public string? PhoneNumber { get => phoneNumber; set { phoneNumber=value; OnPropertyChanged(); } }
	public string? ProfilePicturePath { get => profilePicturePath; set { profilePicturePath=value; OnPropertyChanged(); } }

	#endregion


	public Person() : base()
	{
	}
	public Person(string? name, string? surname, string? fatherName, DateTime? birthDate, int? gender,
					string? userName, string? email, string? password, string? address,
					string? phoneNumber, string? profilePicturePath) : base()
	{
		Name = name;
		Surname = surname;
		FatherName = fatherName;
		BirthDate = birthDate;
		Gender = gender;
		UserName = userName;
		Email = email;
		Password = password;
		Address = address;
		PhoneNumber = phoneNumber;
		ProfilePicturePath = profilePicturePath;
	}
	public Person(Person p) : base()
	{
		this.Name = p.Name;
		this.Surname = p.Surname;
		this.FatherName = p.FatherName;
		this.BirthDate = p.BirthDate;
		this.Gender = p.Gender;
		this.UserName = p.UserName;
		this.Email = p.Email;
		this.Password = p.Password;
		this.Address = p.Address;
		this.PhoneNumber = p.PhoneNumber;
		this.ProfilePicturePath = p.ProfilePicturePath;
	}

	public object Clone()
	{
		return new Person
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

	public void SetValue(Person p)
	{
		this.Name = p.Name;
		this.Surname = p.Surname;
		this.FatherName = p.FatherName;
		this.BirthDate = p.BirthDate;
		this.Gender = p.Gender;
		this.UserName = p.UserName;
		this.Email = p.Email;
		this.Password = p.Password;
		this.Address = p.Address;
		this.PhoneNumber = p.PhoneNumber;
		this.ProfilePicturePath = p.ProfilePicturePath;
	}
}