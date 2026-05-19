class Todo
{
    public int Id;
    public string? Titel;
    public bool Erledigt;

    public Todo() { }

    public Todo(int id, string titel)
    {
        Id = id;
        Titel = titel;
        Erledigt = false;
    }

    public void Anzeigen()
    {
        string status = Erledigt ? "✅" : "❌";
        Console.WriteLine($"{Id}. {status} {Titel}");
    }
}