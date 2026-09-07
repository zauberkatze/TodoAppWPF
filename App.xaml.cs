using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace TodoAppWPF;

public partial class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		this.DispatcherUnhandledException += App_DispatcherUnhandledException;
		AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
	}

	private void App_DispatcherUnhandledException(object? sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
	{
		Logger.Log("DispatcherUnhandledException: " + e.Exception.ToString());
		MessageBox.Show("Unerwarteter Fehler:\n" + e.Exception.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
		e.Handled = true;
	}

	private void CurrentDomain_UnhandledException(object? sender, UnhandledExceptionEventArgs e)
	{
		if (e.ExceptionObject is Exception ex)
		{
			Logger.Log("CurrentDomain_UnhandledException: " + ex.ToString());
		}
		else
		{
			Logger.Log("CurrentDomain_UnhandledException: Unknown exception object");
		}
	}

	private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
	{
		Logger.Log("UnobservedTaskException: " + e.Exception.ToString());
		e.SetObserved();
	}
}