using Lab2.Queue.Array;
namespace Lab2.Program;

public class Program
{
    public static void Main()
    {
        Lab2.Stack.Tests.Array.RunTest();
        Lab2.Stack.Tests.ATDList.RunTest();
        Lab2.Stack.Tests.LinkedList.RunTest();
        Lab2.Queue.Tests.Array.RunTest();
        Lab2.Queue.Tests.ATDList.RunTest();
        Lab2.Queue.Tests.CircularLinkedList.RunTest();
        Lab2.Map.Tests.LinkedList.RunTest();


        // Lab2.Stack.Array.Stack<char> arrayStack = new();
        // Lab2.Stack.ATDList.Stack<char> atdStack = new();
        // Lab2.Stack.LinkedList.Stack<char> linkedStack = new();

        // Lab2.Queue.Array.Queue<char> arrayQueue = new();
        // Lab2.Queue.ATDList.Queue<char> atdSQueue = new();
        // Lab2.Queue.CircularyLinkedList.Queue<char> circularyLinkedQueue = new();

        // Lab2.Map.LinkedList.Map<char, int> linkedMap = new();

        // char[] arrayChars = "Some Text".ToCharArray();
    }
}