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
        if (File.Exists(DefaultDateipfad))
        {
            string json = File.ReadAllText(DefaultDateipfad);
            listen = JsonConvert.DeserializeObject<List<TodoListe>>(json)!;
            // Ensure Todos lists are initialized after deserialization
            foreach (var l in listen)
            {
                if (l.Todos == null) l.Todos = new List<Todo>();
            }
        }
    }

    public void Speichern()
    {
        string json = JsonConvert.SerializeObject(listen);
        File.WriteAllText(DefaultDateipfad, json);
    }

    public void HinzuFügen(string titel)
    {
        try
        {
            if (aktiveListe == null) throw new InvalidOperationException("Keine aktive Liste zum Hinzufügen ausgewählt.");
            if (aktiveListe.Todos == null) aktiveListe.Todos = new List<Todo>();
            int id = aktiveListe.Todos.Count + 1;
            aktiveListe.Todos.Add(new Todo(id, titel));
        }
        catch (Exception ex)
        {
            Logger.Log("Fehler in TodoService.HinzuFügen: " + ex.ToString());
            throw;
        }
    }

    public void Löschen(int id)
    {
        if (aktiveListe == null) return;
        if (aktiveListe.Todos != null)
        {
            aktiveListe.Todos.RemoveAll(t => t.Id == id);
        }
    }

    public void ListeLöschen(TodoListe liste)
    {
        if (liste == null) return;
        if (listen.Contains(liste))
        {
            listen.Remove(liste);
            if (aktiveListe == liste)
            {
                aktiveListe = null;
            }
            Speichern();
        }
    }
}