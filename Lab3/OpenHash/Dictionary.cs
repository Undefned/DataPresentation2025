using Lab3.Interfce;

namespace Lab3.OpenHash;

public class Dictionary : IDictionary
{
    private const int Capacity = 69;
    private Node[] _array = new Node[Capacity];

    public void Delete(char[] x)
    {
        if (!Member(x)) return;

        int hash = Hash(x);
        Node current = _array[hash];
        
        // Обработка удаления головы списка
        if (current != null && IsEquals(current.Value, x))
        {
            _array[hash] = current.Next!;
            return;
        }
        
        // Удаление из середины или конца списка
        Node previous = null!;
        while (current != null)
        {
            if (IsEquals(current.Value, x))
            {
                if (previous != null)
                {
                    previous.Next = current.Next;
                }
                return;
            }
            previous = current;
            current = current.Next!;
        }
    }

    public void Insert(char[] x)
    {
        if (Member(x)) return;
        
        int hash = Hash(x);
        Node head = _array[hash];
        _array[hash] = new Node(x, head);
    }

    public void Makenull()
    {
        for (int i = 0; i < Capacity; i++)
        {
            _array[i] = null!; // Обнуляем головы, а все остальные элементы оторвутся от корня, GC схавает их, слава шарпам!
        }
    }

    public bool Member(char[] x)
    {
        int hash = Hash(x);
        Node current = _array[hash];

        while (current != null)
        {
            if (IsEquals(current.Value, x)) return true;
            current = current.Next!;
        }

        return false;
    }

    public void Print()
    {
        for (int i = 0; i < Capacity; i++)
        {
            Node current = _array[i];
            if (current != null)
            {
                Console.Write($"{i}: ");
                while (current != null)
                {
                    Console.Write($"{new string(current.Value)}");
                    if (current.Next != null)
                        Console.Write(" -> ");
                    else
                        Console.Write(" -> null");
                    current = current.Next!;
                }
                Console.WriteLine();
            }
        }
    }

    //---------extended private methods
    private int Hash(char[] name)
    {
        int sum = 0;

        for (int i = 0; i < name.Length && name[i] != '\0'; i++)
            sum += (int)name[i];

        return sum % Capacity;
    }
    
    private bool IsEquals(char[] name1, char[] name2)
    {
        if (name1.Length != name2.Length) return false;
        if (name1 == null || name2 == null) return name1 == name2;

        for (int i = 0; i < name1.Length; i++)
            if (name1[i] != name2[i]) return false;

        return true;
    }
}