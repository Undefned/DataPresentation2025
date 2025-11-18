using System.Security.Cryptography.X509Certificates;

namespace Lab1.Cursor;

public class Node<T>
{
    public T? Value { get; set; }
    public int Next { get; set; }

    public Node(int index, int totalSize)
    {
        if (index < totalSize - 1)
        {
            Next = index + 1;
        }
        else
        {
            Next = -1;
        }
    }

    public Node(){}

    // public static Node<T>[]? InitializeNodes(int size)
    // {
    //     Node<T>[] Nodes = new Node<T>[size];

    //     // Инициализация цепочки свободных узлов
    //     for (int i = 0; i < size - 1; i++)
    //     {
    //         Nodes[i] = new Node<T>
    //         {
    //             Next = i + 1  // каждый узел указывает на следующий
    //         };
    //     }

    //     // Последний узел указывает на -1 (конец цепочки)
    //     Nodes[size - 1] = new Node<T>
    //     {
    //         Next = -1
    //     };
    //     return Nodes;
    // }
}