using System.Windows;

namespace TodoAppWPF;

public partial class App : Application
{
    public App()
    {
        AppDomain.CurrentDomain.UnhandledException += (s, ex) =>
        {
            MessageBox.Show(ex.ExceptionObject.ToString(), "Kritischer Fehler");
        };
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DispatcherUnhandledException += (s, ex) =>
        {
            MessageBox.Show(ex.Exception.ToString(), "Fehler");
            ex.Handled = true;
        };
    }
}