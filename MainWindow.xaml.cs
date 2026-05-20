using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;

namespace TodoAppWPF
{
    public partial class MainWindow : Window
    {
        private TodoService service = new TodoService();
        private ObservableCollection<Todo> todoListe = new ObservableCollection<Todo>();
        private bool isDirty = false;

        public MainWindow()
        {
            InitializeComponent();
            service.Laden();
            TodoListe.ItemsSource = todoListe;
            AktualisiereListe();
            this.Closing += MainWindow_Closing;
        }

        private void HinzufügenButton_Click(object sender, RoutedEventArgs e)
        {
            string titel = EingabeBox.Text;

            if (string.IsNullOrWhiteSpace(titel))
            {
                MessageBox.Show("Bitte einen Titel eingeben!");
                return;
            }

            service.HinzuFügen(titel);
            EingabeBox.Clear();
            isDirty = true;
            AktualisiereListe();
        }

        private void AktualisiereListe()
        {
            todoListe.Clear();
            foreach (Todo todo in service.GetAlle())
            {
                todoListe.Add(todo);
            }
        }

        private void ListBoxItem_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Änderungen als ungespeichert markieren, wenn der Benutzer eine CheckBox klickt
            if (e.OriginalSource is System.Windows.Controls.CheckBox)
            {
                isDirty = true;
            }
        }

        private void SpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            service.Speichern();
            isDirty = false;
            MessageBox.Show("Liste gespeichert.", "Speichern", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LöschenButton_Click(object sender, RoutedEventArgs e)
        {
            if (TodoListe.SelectedItem is Todo selected)
            {
                service.Löschen(selected.Id);
                isDirty = true;
                AktualisiereListe();
            }
            else
            {
                MessageBox.Show("Bitte einen Eintrag auswählen, der gelöscht werden soll.", "Löschen", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            if (!isDirty) return;

            var result = MessageBox.Show("Sie haben ungespeicherte Änderungen. Möchten Sie speichern?", "Ungespeicherte Änderungen",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                service.Speichern();
                isDirty = false;
            }
            else if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                // Nein: Änderungen verwerfen — vom Datenträger neu laden, um gespeicherten Zustand wiederherzustellen
                service.Laden();
            }
        }
    }
}