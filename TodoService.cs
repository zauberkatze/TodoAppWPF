using Newtonsoft.Json;
using System.IO;

class TodoService
{
    private List<Todo> todos = new List<Todo>();
    private string dateipfad = "todos.json";
    
    public List<Todo> GetAlle()
    {
        return todos;
    }

    // Laden wenn Programm startet
    public void Laden()
    {
        if (File.Exists(dateipfad))
        {
            string json = File.ReadAllText(dateipfad);
            todos = JsonConvert.DeserializeObject<List<Todo>>(json)!;
        }
    }

    // Speichern
    public void Speichern()
    {
        string json = JsonConvert.SerializeObject(todos);
        File.WriteAllText(dateipfad, json);
    }

    // Aufgabe hinzufügen
    public void HinzuFügen(string titel)
    {
        int id = todos.Count + 1;
        todos.Add(new Todo(id, titel));
        Speichern();
        Console.WriteLine("Aufgabe hinzugefügt!");
    }

    // Alle Aufgaben anzeigen
    public void AlleAnzeigen()
    {
        if (todos.Count == 0)
        {
            Console.WriteLine("Keine Aufgaben vorhanden.");
            return;
        }

        foreach (Todo todo in todos)
        {
            todo.Anzeigen();
        }
    }

    // Aufgabe als erledigt markieren
    public void AlsErledigtMarkieren(int id)
    {
        foreach (Todo todo in todos)
        {
            if (todo.Id == id)
            {
                todo.Erledigt = true;
                Speichern();
                Console.WriteLine("Aufgabe erledigt!");
                return;
            }
        }
        Console.WriteLine("Aufgabe nicht gefunden!");
    }

    // Aufgabe löschen
    public void Löschen(int id)
    {
        foreach (Todo todo in todos)
        {
            if (todo.Id == id)
            {
                todos.Remove(todo);
                Speichern();
                Console.WriteLine("Aufgabe gelöscht!");
                return;
            }
        }
        Console.WriteLine("Aufgabe nicht gefunden!");
    }
}