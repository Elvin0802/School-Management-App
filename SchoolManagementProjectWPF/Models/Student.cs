using System.Collections.ObjectModel;

namespace SchoolManagementProjectWPF.Models;

public class Student : Person
{
	private ObservableCollection<Exam>? exams;
	private Guid? cId = null;

	public ObservableCollection<Exam>? Exams { get => exams; set { exams = value; OnPropertyChanged(); } }
	public Guid? ClassID { get => cId; set { cId=value; OnPropertyChanged(); } }
	public double GradeAvg { get => GetAvgExamGrade(); }

	public Student() : base()
	{
		Exams=new();
	}

	public Student(string? name, string? surname, string? fatherName, DateTime? birthDate,
				 int? gender, string? userName, string? email, string? password, string? address,
				 string? phoneNumber, string? profilePicturePath)
		 : base(name, surname, fatherName, birthDate, gender, userName, email, password, address, phoneNumber, profilePicturePath)
	{
		Exams=new();
	}

	public new object Clone()
	{
		return new Student
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

	private double GetAvgExamGrade()
	{
		try
		{
			return (byte)(Exams!.Sum(e => e.ExamGrade!.Value) / Exams!.Count);
		}
		catch { return 0; }
	}

	public void SetOwnerClassroom(Guid ClassroomID) => cId = ClassroomID;

}
