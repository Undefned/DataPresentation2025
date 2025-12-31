using Lab2.Program;
namespace Lab2.Map.LinkedList;

// public class Node<TKey, TValue>
// {
//     public TKey? Key { get; set; }
//     public TValue? Value { get; set; }
//     public Node<TKey, TValue>? Next { get; set; }

//     public Node() { }
//     public Node(TKey? key, TValue? value, Node<TKey, TValue>? next = null)
//     {
//         Key = key;
//         Value = value;
//         Next = next;
//     }
// }

public class Node<TKey, TValue>
{
    public Addressee Data; // Данные узла — объект LetterObject
    public Node<TKey, TValue> Next;         // Ссылка на следующий узел

    // Конструктор: инициализация данных и ссылки на следующий узел
    public Node(Addressee data, Node<TKey, TValue> next)
    {
        Data = data;
        Next = next;
    }

    // Конструктор: инициализация данных по массивам символов и ссылки на следующий узел
    public Node(char[] name, char[] address, Node<TKey, TValue> next)
    {
        Data = new Addressee(name, address);
        Next = next;
    }
}