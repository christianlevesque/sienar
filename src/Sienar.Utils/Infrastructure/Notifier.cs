namespace Sienar.Infrastructure;

/// <summary>
/// Stores transient notifications
/// </summary>
public class Notifier
{
	/// <summary>
	/// The registered notifications
	/// </summary>
	public List<Notification> Notifications { get; } = [];

	/// <summary>
	/// Used to display a success message to the user
	/// </summary>
	/// <param name="message">The message to display</param>
	public void Success(string message)
	{
		Notify(new Notification(message, NotificationType.Success));
	}

	/// <summary>
	/// Used to display a warning message to the user
	/// </summary>
	/// <param name="message">The message to display</param>
	public void Warning(string message)
	{
		Notify(new Notification(message, NotificationType.Warning));
	}

	/// <summary>
	/// Used to display an informational message to the user
	/// </summary>
	/// <param name="message">The message to display</param>
	public void Info(string message)
	{
		Notify(new Notification(message, NotificationType.Info));
	}

	/// <summary>
	/// Used to display an error message to the user
	/// </summary>
	/// <param name="message">The message to display</param>
	public void Error(string message)
	{
		Notify(new Notification(message, NotificationType.Error));
	}

	/// <summary>
	/// Used to display an arbitrary notification to the user
	/// </summary>
	/// <param name="notification">The notification to display</param>
	public void Notify(Notification notification)
	{
		Notifications.Add(notification);
	}
}
