using Newtonsoft.Json;
using System.IO;

class TodoService
{
    private List<TodoListe> listen = new List<TodoListe>();
    private TodoListe? aktiveListe = null;
    private const string DefaultDateipfad = "todos.json";

    public List<TodoListe> GetAlleListen()
    {
        return listen;
    }

    public TodoListe? GetAktiveListe()
    {
        return aktiveListe;
    }

    public void SetAktiveListe(TodoListe liste)
    {
        aktiveListe = liste;
    }

    public List<Todo> GetAlle()
    {
        return aktiveListe?.Todos ?? new List<Todo>();
    }

    public void ListeHinzufügen(string name)
    {
        listen.Add(new TodoListe { Name = name });
        Speichern();
    }

    public void Laden()
    {
        if (File.Exists(dateipfad))
        {
            string json = File.ReadAllText(dateipfad);
            listen = JsonConvert.DeserializeObject<List<TodoListe>>(json)!;
        }
    }

    public void Speichern()
    {
        string json = JsonConvert.SerializeObject(listen);
        File.WriteAllText(dateipfad, json);
    }

    public void HinzuFügen(string titel)
    {
        if (aktiveListe == null) return;
        int id = aktiveListe.Todos.Count + 1;
        aktiveListe.Todos.Add(new Todo(id, titel));
    }

    public void Löschen(int id)
    {
        if (aktiveListe == null) return;
        foreach (Todo todo in aktiveListe.Todos)
        {
            if (todo.Id == id)
            {
                aktiveListe.Todos.Remove(todo);
                return;
            }
        }
    }
}