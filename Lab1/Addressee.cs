namespace Lab1;

public class Addressee
{
    private const int MaxCharsInName = 20;
    private const int MaxCharsInAdresse = 50;
    // private string Name { get; set; }
    // private string Addresse { get; set; }
     private char[] Name = new char[MaxCharsInName];
    private char[] Addresse = new char[MaxCharsInAdresse];
    public Addressee(string name, string addresse)
    {
        int nameSize = Math.Min(name.Length, MaxCharsInName);
        for (int i = 0; i < nameSize; i++)
        {
            Name[i] = name[i];
        }

        int addresseSize = Math.Min(addresse.Length, MaxCharsInAdresse);
        for (int i = 0; i < addresseSize; i++)
        {
            Addresse[i] = addresse[i];
        }
        // Name = name.Length > MaxCharsInName ? name.Substring(0, MaxCharsInName) : name;
        // Addresse = addresse.Length > MaxCharsInAdresse ? addresse.Substring(0, MaxCharsInAdresse) : addresse;
    }

    public bool Equals(Addressee? other)
    {
        if (other == null) return false;
        
        for (int i = 0; i < Name.Length; i++)
        {
            if (Name[i] != other.Name[i]) return false;
        }

        for (int i = 0; i < Addresse.Length; i++)
        {
            if (Addresse[i] != other.Addresse[i]) return false;
        }

        return true;
    }

    public override string ToString()
    {
        return $"{new string(Name)} - {new string(Addresse)}";
    }
}