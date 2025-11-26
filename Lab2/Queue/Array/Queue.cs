using Lab2.Queue.Interfaces;

namespace Lab2.Queue.Array;

public class Queue<T> : IQueue<T>
{
    private const int _capacity = 52;
    private T[] _array = new T[_capacity];
    private int _first = 0;
    private int _last = _capacity - 1;

    public T Dequeue()
    {
        T item = _array[_first];
        _first = Next(_first);

        return item;
    }


    public bool Empty()
    {
        return _first == _last;
    }

    public void Enqueue(T x)
    {
        // if (Empty())
        // {
        //     _first = _last = 0;
        // }
        // else
        // {
            _last = (_last + 1) % _capacity; // двигаем хвост по кругу
        // }
        _array[_last] = x;
    }

    public T Front()
    {
        // if (Empty())
        //     throw new InvalidOperationException("Queue is empty");
        return _array[_first];
    }

    public bool Full()
    {
        return Next(_last) == _first;
    }

    public void MakeNull()
    {
        _first = 0;
        _last = -1;
    }

    private int Next(int pos)
    {
        return (pos + 1) % _capacity;
    }
}