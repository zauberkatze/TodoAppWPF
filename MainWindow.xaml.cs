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
            AktualisiereListen();
            this.Closing += MainWindow_Closing;
        }

        private void NeueListeButton_Click(object sender, RoutedEventArgs e)
        {
            string name = ListenNameBox.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Bitte einen Namen eingeben!");
                return;
            }

            service.ListeHinzufügen(name);
            ListenNameBox.Clear();
            isDirty = true;
            AktualisiereListen();
        }

        private void ListenAuswahl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ListenAuswahl.SelectedItem is TodoListe selected)
            {
                service.SetAktiveListe(selected);
                AktualisiereTodos();
            }
        }

        private void HinzufügenButton_Click(object sender, RoutedEventArgs e)
        {
            string titel = EingabeBox.Text;

            if (string.IsNullOrWhiteSpace(titel))
            {
                MessageBox.Show("Bitte einen Titel eingeben!");
                return;
            }

            if (service.GetAktiveListe() == null)
            {
                MessageBox.Show("Bitte zuerst eine Liste auswählen!");
                return;
            }

            service.HinzuFügen(titel);
            EingabeBox.Clear();
            isDirty = true;
            AktualisiereTodos();
        }

        private void AktualisiereListen()
        {
            ListenAuswahl.ItemsSource = null;
            ListenAuswahl.ItemsSource = service.GetAlleListen();
        }

        private void AktualisiereTodos()
        {
            todoListe.Clear();
            foreach (Todo todo in service.GetAlle())
            {
                todoListe.Add(todo);
            }
        }

        private void ListeLöschenButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListenAuswahl.SelectedItem is TodoListe selected)
            {
                var result = MessageBox.Show($"Liste '{selected.Name}' wirklich löschen?",
                    "Liste löschen", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    service.ListeLöschen(selected);
                    isDirty = true;
                    AktualisiereListen();
                    AktualisiereTodos();
                }
            }
            else
            {
                MessageBox.Show("Bitte zuerst eine Liste auswählen.", "Liste löschen", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ListBoxItem_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
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
                AktualisiereTodos();
            }
            else
            {
                MessageBox.Show("Bitte einen Eintrag auswählen.", "Löschen", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            if (!isDirty) return;

            var result = MessageBox.Show("Sie haben ungespeicherte Änderungen. Möchten Sie speichern?",
                "Ungespeicherte Änderungen", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

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
                service.Laden();
            }
        }
    }
}