using SchoolManagementProjectWPF.Services;

namespace SchoolManagementProjectWPF.Models;


public class Exam : NotifyService
{
	private Guid id = Guid.Empty;
	private double? grade;
	private int? questionCount;
	private string? subject;
	private DateTime? writedTime;
	private DateTime? sharedTime;

	public Guid ID { get => id; set { id = value; OnPropertyChanged(); } }
	public double? ExamGrade { get => grade; set { grade = value; OnPropertyChanged(); } }

	public int? QuestionCount { get => questionCount; set { questionCount = value; OnPropertyChanged(); } }

	public string? Subject
	{
		get => subject; set { subject = value; OnPropertyChanged(); }
	}
	public DateTime? WritedTime
	{
		get => writedTime; set { writedTime = value; OnPropertyChanged(); }
	}
	public DateTime? SharedTime
	{
		get => sharedTime; set { sharedTime = value; OnPropertyChanged(); }
	}

	public Exam()
	{
		ID = Guid.NewGuid();
	}

	public Exam(string subject, int questionCount, DateTime sharedTime) : this()
	{
		Subject = subject;
		SharedTime = sharedTime;
		QuestionCount = questionCount;
	}

	public void TakeExam(int correctAnswerCount)
	{
		WritedTime = DateTime.Now;
		ExamGrade = 100.0 * correctAnswerCount / questionCount;
	}
}
