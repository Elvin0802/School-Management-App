using SchoolManagementProjectWPF.Services;
using System.Collections.ObjectModel;

namespace SchoolManagementProjectWPF.Models;

public class Classroom : NotifyService, ICloneable
{
	private Guid? id;
	private string? name;
	private Teacher? headTeacher;
	private ObservableCollection<Student>? students;

	public Guid? ID { get => id; set { if (id != value) { id = value; OnPropertyChanged(); } } }
	public string? Name
	{
		get { return name; }
		set { name = value; OnPropertyChanged(); }
	}
	public ObservableCollection<Student>? Students
	{
		get { return students; }
		set { students = value; OnPropertyChanged(); }
	}
	public Teacher? HeadTeacher
	{
		get { return headTeacher; }
		set { headTeacher = value; OnPropertyChanged(); }
	}
	public int? StudentCount { get => Students?.Count; }

	public Classroom()
	{
		ID = Guid.NewGuid();
		name = "";
		Students = new();
		HeadTeacher = new();
	}
	public Classroom(string? name, Teacher? headTeacher) : this()
	{
		Name = name;
		HeadTeacher=headTeacher;
	}

	public void AddStudent(Student student)
	{
		Students?.Add(student);
	}

	public void AddExam(Exam e)
	{
		foreach (var s in Students!)
			s.Exams!.Add(e);
	}
	public void RemoveExam(Exam e)
	{
		foreach (var s in Students!)
			s.Exams!.Remove(e);
	}
	public ObservableCollection<Exam>? GetExams()
	{
		if (Students!.Count == 0) return new();

		return Students![0].Exams;
	}

	public override string ToString() => Name!;

	public void SetValue(Classroom cr)
	{
		this.Name = cr.Name;
		this.HeadTeacher = cr.HeadTeacher;
		this.Students = cr.Students;
	}

	public object Clone()
	{
		return new Classroom
		{
			Name = this.Name,
			HeadTeacher = this.HeadTeacher!.Clone() as Teacher,
			Students = this.Students
		};
	}
}
