using SchoolManagementProjectWPF.Commands;
using SchoolManagementProjectWPF.DataBase;
using SchoolManagementProjectWPF.Models;
using SchoolManagementProjectWPF.ViewModels.Global;
using SchoolManagementProjectWPF.Views.Pages;
using SchoolManagementProjectWPF.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SchoolManagementProjectWPF.ViewModels.ForPages;

public class AllClassroomsPageViewModel : BaseViewModel
{
	public AllClassroomsPageViewModel()
	{
		BackCommand = new RelayCommand(BackCommandExecute);
		ChangeThemeCommand = new RelayCommand(ChangeThemeColor);

		AddClassroomCommand = new RelayCommand(AddClassroomCommandExecute);
		RemoveClassroomCommand = new RelayCommand(RemoveClassroomCommandExecute);
		EditClassroomCommand = new RelayCommand(EditClassroomCommandExecute);
		ShowClassroomCommand = new RelayCommand(ShowClassroomCommandExecute);

		CurrentClassroms = AppDbContex.School!.Classrooms;

		App.Container!.GetInstance<AllClassroomsPageView>().BaseListView.ItemsSource=CurrentClassroms;
		App.Container.GetInstance<AllClassroomsPageView>().BaseListView.Items.Refresh();
	}
	private ObservableCollection<Classroom>? currentClassroms;
	private int sortIndex;

	public ObservableCollection<Classroom>? CurrentClassroms { get => currentClassroms; set { currentClassroms = value; OnPropertyChanged(); } }
	public int SortIndex
	{
		get => sortIndex;
		set
		{
			try
			{
				sortIndex=value;
				OnPropertyChanged();
				SortClassrooms(value);
			}
			catch { MessageBox.Show("Error in Sorting."); }
		}
	}


	#region Add Classroom Command

	public ICommand AddClassroomCommand { get; set; }

	public void AddClassroomCommandExecute(object? obj)
	{
		var NextView = App.Container!.GetInstance<AddClassroomPageView>();
		var NextViewModel = App.Container.GetInstance<AddClassroomPageViewModel>();

		Classroom newCr = new();

		NextViewModel!.isEdit = false;
		NextViewModel!.EditClassroom = newCr;
		NextViewModel!.CopyEditClassroom = new Classroom();

		NextViewModel.GetEmptyTeachers();

		NextView!.DataContext = NextViewModel;

		MainWindowView? win = App.Container?.GetInstance<MainWindowView>();

		win?.Navigate(NextView);
	}
	#endregion

	#region Remove Classroom Command

	public ICommand RemoveClassroomCommand { get; set; }
	public void RemoveClassroomCommandExecute(object? obj)
	{
		try
		{
			if (App.Container!.GetInstance<AllClassroomsPageView>().BaseListView.SelectedValue is Classroom c)
			{
				c.Students!.Clear();
				c.Students = null;
				c.HeadTeacher!.ClassID=null;
				CurrentClassroms?.Remove(c);
				App.Container!.GetInstance<AdminPageView>().BaseListView.Items.Refresh();

				DbOperations.WriteClassrooms(AppDbContex.School!);
				DbOperations.WriteTeachers(AppDbContex.School!);

				MessageBox.Show("Classroom removed.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}
		catch { MessageBox.Show("Classroom not removed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
	}

	#endregion

	#region Show Classroom Command

	public ICommand ShowClassroomCommand { get; set; }
	public void ShowClassroomCommandExecute(object? obj)
	{
		var NextView = App.Container!.GetInstance<ShowClassroomPageView>();
		var NextViewModel = App.Container.GetInstance<ShowClassroomPageViewModel>();

		if (App.Container!.GetInstance<AllClassroomsPageView>().BaseListView.SelectedValue is Classroom classroom)
		{
			NextViewModel!.CurrentClassroom = classroom;

			NextView!.DataContext = NextViewModel;

			App.Container!.GetInstance<ShowClassroomPageView>().BaseListView.ItemsSource = NextViewModel!.CurrentClassroom.Students;
			App.Container!.GetInstance<ShowClassroomPageView>().BaseListView.Items.Refresh();

			MainWindowView? win = App.Container?.GetInstance<MainWindowView>();

			win?.Navigate(NextView);
		}
	}

	#endregion

	#region Edit Classroom Command

	public ICommand EditClassroomCommand { get; set; }
	public void EditClassroomCommandExecute(object? obj)
	{
		var NextView = App.Container!.GetInstance<AddClassroomPageView>();
		var NextViewModel = App.Container.GetInstance<AddClassroomPageViewModel>();

		if (App.Container!.GetInstance<AllClassroomsPageView>().BaseListView.SelectedValue is Classroom classroom)
		{
			NextViewModel!.isEdit = true;
			NextViewModel!.EditClassroom = classroom;
			NextViewModel!.CopyEditClassroom = classroom.Clone() as Classroom;

			NextViewModel.GetEmptyTeachers();

			NextView!.DataContext = NextViewModel;

			MainWindowView? win = App.Container?.GetInstance<MainWindowView>();

			win?.Navigate(NextView);
		}
	}

	#endregion


	#region List Sort Function

	public void SortClassrooms(int index)
	{
		if (index == 0)
			CurrentClassroms = new(CurrentClassroms?.OrderBy(s => s.Name)!);
		else if (index == 1)
			CurrentClassroms = new(CurrentClassroms?.OrderBy(s => s.StudentCount)!);
		else if (index == 2)
			CurrentClassroms = new(CurrentClassroms?.OrderByDescending(s => s.Name)!);
		else if (index == 3)
			CurrentClassroms = new(CurrentClassroms?.OrderByDescending(s => s.StudentCount)!);

		App.Container!.GetInstance<AllClassroomsPageView>().BaseListView.ItemsSource = CurrentClassroms;
		App.Container!.GetInstance<AllClassroomsPageView>().BaseListView.Items.Refresh();
	}

	#endregion

}
