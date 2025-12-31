using System.Transactions;
using Lab3.Interfce;

namespace Lab3.CloseHash;
public class Dictionary : IDictionary
{
    private const int Capacity = 69;
    private const int MaxSizeName = 10;
    private char[][] _array = new char[Capacity][];

    public void Delete(char[] x)
    {
        int hash = Hash(x);
        
        for (int i = 0; i < Capacity; i++)
        {
            int index = (hash + i) % Capacity;
            
            if (_array[index] == null)
                return;
                
            if (!IsDeleted(index) && IsEquals(_array[index], x))
            {
                _array[index][0] = '\0';
                return;
            }
        }
    }

    public void Insert(char[] x)
    {
        int hash = Hash(x);
        
        for (int i = 0; i < Capacity; i++)
        {
            int index = (hash + i) % Capacity;
            
            if (_array[index] == null || IsDeleted(index))
            {
                _array[index] = CopyName(x);
                return;
            }
            
            // Если уже есть такой элемент
            if (IsEquals(_array[index], x))
                return;
        }
        
        Console.WriteLine("Ошибка: словарь переполнен!");
    }

    public void Makenull()
    {
        for (int i = 0; i < _array.Length; i++)
        {
            _array[i] = null!;
        }
    }

    public bool Member(char[] x)
    {
        int hash = Hash(x);
        
        for (int i = 0; i < Capacity; i++)
        {
            int index = (hash + i) % Capacity;  //  линейное пробирование
            
            if (_array[index] == null)
                return false;  // Элемента никогда не было
                
            if (!IsDeleted(index) && IsEquals(_array[index], x))
                return true;  // Нашли
        }
        
        return false;  // Прошли весь массив
    }

    public void Print()
    {
        Console.WriteLine("\nPrint:\n");
        for (int i = 0; i < Capacity; i++)
        {
            if (_array[i] == null || _array[i][0] == '\0')  // Сначала null!
                continue;

            Console.WriteLine($"{i} - {new string(_array[i])}");
        }
        Console.WriteLine();
    }

    //---------extended private methods
    private int Hash(char[] name)
    {
        int sum = 0;

        for (int i = 0; i < name.Length && name[i] != '\0'; i++)
            sum += name[i];

        return sum % Capacity;
    }

    private int HashNext(int hash)
    {
        return (hash+1) % Capacity;
    }

    private bool IsEquals(char[] name1, char[] name2)
    {
        if (name1 == null && name2 == null) return true;
        if (name1 == null || name2 == null) return false;
        
        // Сравниваем до первого \0 или до конца
        for (int i = 0; i < MaxSizeName; i++)
        {
            char c1 = (i < name1.Length) ? name1[i] : '\0';
            char c2 = (i < name2.Length) ? name2[i] : '\0';
            
            if (c1 != c2) return false;
            if (c1 == '\0' || c2 == '\0') break;
        }
        
        return true;
    }

    private char[] CopyName(char[] name)
    {
        char[] copy = new char[MaxSizeName];
        int length = Math.Min(name.Length, MaxSizeName);
        Array.Copy(name, copy, length);
        return copy;
    }

    private bool IsDeleted(int index)
    {
        return _array[index] != null && _array[index][0] == '\0';
    }
}