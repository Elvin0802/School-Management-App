using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace SchoolManagementProjectWPF.Models;

public static class DbOperations
{
	public static JsonSerializerOptions op = new() { WriteIndented = true };

	public static void WriteTeachers(School s)
		=> File.WriteAllText("Teachers.json", JsonSerializer.Serialize(s.Teachers!, op));
	public static void WriteClassrooms(School s)
		=> File.WriteAllText("Classrooms.json", JsonSerializer.Serialize(s.Classrooms!, op));

	public static void ReadTeachers(School s)
		=> s.Teachers = JsonSerializer.Deserialize<ObservableCollection<Teacher>>(File.ReadAllText("Teachers.json"));
	public static void ReadClassrooms(School s)
		=> s.Classrooms = JsonSerializer.Deserialize<ObservableCollection<Classroom>>(File.ReadAllText("Classrooms.json"));
}