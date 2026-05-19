using System.Collections.ObjectModel;
using System.Windows;

namespace TodoAppWPF
{
    public partial class MainWindow : Window
    {
        private TodoService service = new TodoService();
        private ObservableCollection<Todo> todoListe = new ObservableCollection<Todo>();

        public MainWindow()
        {
            InitializeComponent();
            service.Laden();
            TodoListe.ItemsSource = todoListe;
            AktualisiereListe();
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
            // Speichern wenn der Benutzer einen CheckBox klickt
            if (e.OriginalSource is System.Windows.Controls.CheckBox)
            {
                service.Speichern();
            }
        }
    }
}