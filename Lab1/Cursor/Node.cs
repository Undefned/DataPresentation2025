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
}