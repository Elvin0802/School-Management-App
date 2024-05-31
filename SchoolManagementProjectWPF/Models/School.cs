using SchoolManagementProjectWPF.Services;
using System.Collections.ObjectModel;

namespace SchoolManagementProjectWPF.Models;


public class School : NotifyService
{
	private Guid id = Guid.Empty;
	private string? name;
	private string? address;
	private ObservableCollection<Teacher>? teachers;
	private ObservableCollection<Classroom>? classrooms;

	public Guid ID
	{
		get => id; set
		{
			if (id != value)
			{
				id = value;
				OnPropertyChanged();
			}
		}
	}
	public string? Name { get => name; set { name = value; OnPropertyChanged(); } }
	public string? Address { get => address; set { address = value; OnPropertyChanged(); } }
	public ObservableCollection<Teacher>? Teachers { get => teachers; set { teachers = value; OnPropertyChanged(); } }
	public ObservableCollection<Classroom>? Classrooms { get => classrooms; set { classrooms = value; OnPropertyChanged(); } }

	public School()
	{
		ID = Guid.NewGuid();
		Name = "Step IT Academy";
		Address = "Baku, Azerbaijan.";

		Teachers = new();
		Classrooms = new();
	}

	public void RemoveTeacher(string userName)
	{
		foreach (var t in Teachers!)
			if (t.UserName == userName)
				Teachers.Remove(t);
	}

	public Classroom? GetClassroom(Guid classID)
	{
		foreach (var c in Classrooms!)
			if (c.ID == classID)
				return c;

		return null;
	}
	public Person? GetTeacher(string userName, string password)
	{
		foreach (var t in Teachers!)
			if (t.UserName == userName && t.Password==password)
				return t;

		return null;
	}
	public bool CheckTeacher(string userName)
	{
		foreach (var t in Teachers!)
			if (t.UserName == userName)
				return true;
		return false;
	}

	public bool AddStudentToClass(Guid classId, Student? st)
	{
		try
		{
			foreach (var cl in Classrooms!)
			{
				if (cl.ID == classId)
				{
					cl.AddStudent(st!);
					return true;
				}
			}
			return false;
		}
		catch { return false; }
	}

}
