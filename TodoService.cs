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
        Console.WriteLine("Aufgabe hinzugefügt!");
    }

    // Aufgabe löschen
    public void Löschen(int id)
    {
        foreach (Todo todo in todos)
        {
            if (todo.Id == id)
            {
                todos.Remove(todo);
                Console.WriteLine("Aufgabe gelöscht!");
                return;
            }
        }
        Console.WriteLine("Aufgabe nicht gefunden!");
    }
}