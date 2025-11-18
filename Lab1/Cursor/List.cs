using Lab1.Interfaces;

namespace Lab1.Cursor;

public class List<T> : IList<T, Position>
{
    // Статический массив всех узлов - общий для всех экземпляров списка
    private static readonly Node<T>[] Nodes;
    // Максимальный размер списка
    private const int Size = 52;
    // Позиция первого элемента списка (-1 если список пуст)
    private Position _start = new Position(-1);
    // Позиция первого свободного узла в массиве
    private static int _space = 0;

    // Статический конструктор - инициализирует массив узлов один раз при загрузке класса
    static List()
    {
        Nodes = new Node<T>[Size];
        for (int i = 0; i < Size; i++)
        {
            Nodes[i] = new Node<T>(i, Size);
        }
        // Node<T>.InitializeNodes(Size);
    }
    
    /// <summary>
    /// Возвращает позицию после последнего элемента списка
    /// </summary>
    /// <returns>Позиция конца списка</returns>
    public Position End()
    {
        return new Position(-1);
    }

    /// <summary>
    /// Вставляет элемент в указанную позицию списка
    /// </summary>
    /// <param name="item">Элемент для вставки</param>
    /// <param name="position">Позиция для вставки (перед этим элементом)</param>
    /// <exception cref="Exception">Если нет свободного места или неверная позиция</exception>
    public void Insert(T item, Position position)
    {
        if (_space == -1)
        {
            Console.WriteLine("Нет свободного места в списке");
            return;
        }

        int freeIndex = _space;
        int nextFree = Nodes[freeIndex].Next;

        // всегда вставляем перед указанной позицией
        Nodes[freeIndex].Value = item;
        
        if (position.Posit == End().Posit) 
        {
            // Вставка в конец
            Nodes[freeIndex].Next = -1;
            
            if (IsEmpty()) 
            {
                _start.Posit = freeIndex;  // Первый элемент
            } 
            else 
            {
                // Находим последний элемент и обновляем его Next
                int lastIndex = LastPos();
                Nodes[lastIndex].Next = freeIndex;
            }
        }
        else 
        {
            // Вставка перед существующей позицией
            Nodes[freeIndex].Next = position.Posit;  // Новый узел указывает на старую позицию
            
            int previousIndex = GetPrevious(position.Posit);
            
            if (previousIndex == -1) 
            {
                // Вставка перед первым элементом = новое начало
                _start.Posit = freeIndex;
            } 
            else 
            {
                // Вставка в середину - предыдущий указывает на новый узел
                Nodes[previousIndex].Next = freeIndex;
            }
        }
        
        _space = nextFree;
    }

        
    /// <summary>
    /// Находит позицию первого вхождения элемента в списке
    /// </summary>
    /// <param name="item">Элемент для поиска</param>
    /// <returns>Позиция элемента или End() если не найден</returns>
    public Position Locate(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "Элемент не может быть null");
        int current = _start.Posit;
        while (current != -1)
        {
            if (Nodes[current].Value!.Equals(item))
                return new Position(current);
            current = Nodes[current].Next;
        }
        return End();  // элемент не найден
    }
    
    /// <summary>
    /// Возвращает элемент в указанной позиции
    /// </summary>
    /// <param name="position">Позиция элемента</param>
    /// <returns>Элемент в указанной позиции</returns>
    /// <exception cref="Exception">Если позиция неверная или равна End()</exception>
    public T Retrieve(Position position)
    {
        if (position.Posit == End().Posit)
            throw new Exception("Неверная позиция!");

        if (position.Posit != _start.Posit && GetPrevious(position.Posit) == -1)
            throw new Exception("Неверная позиция!");

        return Nodes[position.Posit].Value!;
    }

    /// <summary>
    /// Удаляет элемент в указанной позиции
    /// </summary>
    /// <param name="position">Позиция элемента для удаления</param>
    /// <exception cref="Exception">Если позиция неверная</exception>
    public void Delete(Position position)
    {
        if (position.Posit == End().Posit || 
            (position.Posit != _start.Posit && GetPrevious(position.Posit) == -1))
        {
            Console.WriteLine("Позиция не существует в списке");
            return;
        }

        int tmp;

        // Удаляем первый элемент
        if (position.Posit == _start.Posit)
        {
            tmp = _space;
            _space = position.Posit;
            _start = new Position(Nodes[_start.Posit].Next);
            Nodes[_space].Next = tmp;
            return;
        }

        // Для остальных случаев GetPrevious() уже гарантированно найдет предыдущий
        int prev = GetPrevious(position.Posit);
        
        // Обновляем ссылку предыдущего элемента
        Nodes[prev].Next = Nodes[position.Posit].Next;

        // Добавляем освободившуюся ячейку в список свободных
        tmp = _space;
        _space = position.Posit;
        Nodes[_space].Next = tmp;
    }

    /// <summary>
    /// Возвращает позицию следующего элемента после указанной позиции
    /// </summary>
    /// <param name="position">Текущая позиция</param>
    /// <returns>Следующая позиция или End() если текущая последняя</returns>
    public Position Next(Position position)
    {
        if (position.Posit == End().Posit)
            return new Position(-1);

        if (position.Posit != _start.Posit && GetPrevious(position.Posit) == -1)
        {
            Console.WriteLine("Неверная позиция для получения следующего элемента");
            return End();
        }

        return new Position(Nodes[position.Posit].Next);
    }
    
    /// <summary>
    /// Очищает список, делая его пустым
    /// </summary>
    public void Makenull()
    {
        if (IsEmpty())
            return;
        
        // Соединяем конец списка данных с началом свободных ячеек
        Nodes[LastPos()].Next = _space;
        // Все ячейки списка становятся свободными
        _space = _start.Posit;
        // Список становится пустым
        _start = new Position(-1);
    }
    
    /// <summary>
    /// Возвращает позицию первого элемента списка
    /// </summary>
    /// <returns>Позиция первого элемента или End() если список пуст</returns>
    public Position First()
    {
        return IsEmpty() ? End() : new Position(_start.Posit);
    }

    /// <summary>
    /// Возвращает индекс элемента в списке (0-based)
    /// </summary>
    /// <param name="item">Элемент для поиска</param>
    /// <returns>Индекс элемента или -1 если не найден</returns>
    public int IndexOf(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        int current = _start.Posit;
        int index = 0;

        while (current != -1)
        {
            if (Nodes[current].Value!.Equals(item))
                return index;

            current = Nodes[current].Next;
            index++;
        }

        return -1; // элемент не найден
    }



    /// <summary>
    /// Возвращает предыдущую позицию перед указанной позицией
    /// </summary>
    /// <param name="position">Текущая позиция</param>
    /// <returns>Предыдущая позиция или позиция -1 если предыдущего нет</returns>
    public Position Previous(Position position)
    {
        int prev;
        if (position.Posit < 0 || (prev = GetPrevious(position.Posit)) == -1) 
            throw new InvalidOperationException("Position not found in list");

        return new Position(prev);
    }



    /// <summary>
    /// Выводит все элементы списка в консоль
    /// </summary>
    public void PrintList()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Список пуст");
            return;
        }

        int cur = _start.Posit;

        while (cur != -1)
        {
            Console.WriteLine(Nodes[cur].Value?.ToString() ?? "null");

            int nextIndex = Nodes[cur].Next;
            cur = nextIndex;
        }

        Console.WriteLine();
    }

    //=== ВСПОМОГАТЕЛЬНЫЕ ПРИВАТНЫЕ МЕТОДЫ ===

    public int GetPrevious(int index)
    {
        int cur = _start.Posit;
        int prev = -1;
        while (cur != -1)
        {
            if (cur == index) return prev;
            prev = cur;
            cur = Nodes[cur].Next;
        }
        return -1;
    }

    /// <summary>
    /// Находит индекс последнего элемента в списке
    /// </summary>
    /// <returns>Индекс последнего элемента или -1 если список пуст</returns>
    private int LastPos()
    {
        int cur = _start.Posit;
        int prev = -1;
        while (cur != -1)
        {
            prev = cur;
            cur = Nodes[cur].Next;
        }
        return prev;
    }
    
    /// <summary>
    /// Проверяет, пуст ли список
    /// </summary>
    /// <returns>true если список пуст, иначе false</returns>
    private bool IsEmpty()
    {
        return _start.Posit == -1;
    }


    // /// <summary>
    // /// Находит индекс предыдущего элемента относительно указанного
    // /// </summary>
    // /// <param name="index">Индекс текущего элемента</param>
    // /// <returns>Индекс предыдущего элемента или -1 если не найден</returns>
    // private int GetPrevious(int index)
    // {
    //     int current = _start.Posit;
    //     int previous = -1;
    //     while (current != -1)
    //     {
    //         if (current == index) 
    //             return previous;
    //         previous = current;
    //         current = Nodes[current].Next;
    //     }
    //     return -1;
    // }
}