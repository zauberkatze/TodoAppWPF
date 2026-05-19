using System.ComponentModel;

class Todo : INotifyPropertyChanged
{
    public int Id;
    private string? titel;
    private bool erledigt;
    
    public string? Titel
    {
        get => titel;
        set
        {
            if (titel != value)
            {
                titel = value;
                OnPropertyChanged(nameof(Titel));
            }
        }
    }
    
    public bool Erledigt
    {
        get => erledigt;
        set
        {
            if (erledigt != value)
            {
                erledigt = value;
                OnPropertyChanged(nameof(Erledigt));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Todo() { }

    public Todo(int id, string titel)
    {
        Id = id;
        Titel = titel;
        Erledigt = false;
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Anzeigen()
    {
        string status = Erledigt ? "Ja" : "Nein";
        Console.WriteLine($"{Id}. {status} {Titel}");
    }
}