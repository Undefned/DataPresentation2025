namespace Lab3.OpenHash;
public class Node
{
    public char[] Value {get; set;}
    public Node? Next {get; set;}
    public Node(char[] value, Node? next)
    {
        Value = value;
        Next = next;
    }
}