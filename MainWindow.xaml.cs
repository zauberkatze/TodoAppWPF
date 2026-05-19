using System.Windows;

namespace TodoAppWPF
{
    public partial class MainWindow : Window
    {
        private TodoService service = new TodoService();

        public MainWindow()
        {
            InitializeComponent();
            service.Laden();
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
            TodoListe.Items.Clear();
            foreach (Todo todo in service.GetAlle())
            {
                string status = todo.Erledigt ? "✅" : "❌";
                TodoListe.Items.Add($"{todo.Id}. {status} {todo.Titel}");
            }
        }
    }
}