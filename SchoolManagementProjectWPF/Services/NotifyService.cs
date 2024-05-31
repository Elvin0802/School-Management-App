using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SchoolManagementProjectWPF.Services;

public abstract class NotifyService : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;
	public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
