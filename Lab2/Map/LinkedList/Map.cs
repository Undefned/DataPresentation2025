using System;
using Lab2.Map.Interfaces;

namespace Lab2.Map.LinkedList
{
    // Класс Map реализует интерфейс IMap для работы с ключами и значениями типа char[]
    // TKey и TValue - обобщенные параметры, хотя в данной реализации не используются
    public class Map<TKey, TValue> : IMap<char[], char[]>
    {
        private Node<TKey, TValue>? _head;  // Голова связного списка

        // Статический метод для сравнения двух массивов char
        // Сравнение происходит до терминального символа '\0'
        private static bool CompareCharArrays(char[] a1, char[] a2)
        {
            int len = Math.Min(a1.Length, a2.Length);  // Определяем минимальную длину

            // Поэлементное сравнение
            for (int i = 0; i < len; i++)
            {
                // Если оба массива достигли конца строки
                if (a1[i] == '\0' && a2[i] == '\0')
                    return true;

                // Если символы не совпадают
                if (a1[i] != a2[i])
                    return false;
            }
            
            // Если дошли до конца без различий
            return true;
        }

        // Реализация метода Assign из интерфейса IMap
        // Добавляет новую пару ключ-значение или обновляет существующую
        public void Assign(char[] name, char[] address)
        {
            // Если список пуст, создаем первый узел
            if (_head == null)
            {
                _head = new Node<TKey, TValue>(name, address, null!);
                return;
            }

            // Ищем узел с таким ключом
            (Node<TKey, TValue>? node, Node<TKey, TValue>? prev) = FindViaKey(name);

            // Если узел найден - обновляем значение
            if (node != null)
            {
                // создаем новый LetterObject вместо изменения старого массива!
                node.Data = new Addressee(name, address);
                return;
            }

            // Если узел не найден - добавляем новый в начало списка
            _head = new Node<TKey, TValue>(name, address, _head);
        }

        // Реализация метода Compute из интерфейса IMap
        // Ищет значение по ключу и возвращает через out-параметр
        public bool Compute(char[] name, out char[] address)
        {
            // Создаем массив для результата заданного размера
            address = new char[Addressee.ADDRESS_CAPACITY];
            
            // Ищем узел по ключу
            (Node<TKey, TValue>? node, Node<TKey, TValue>? prev) = FindViaKey(name);

            // Если узел не найден
            if (node == null)
            {
                address = Array.Empty<char>();  // Возвращаем пустой массив
                return false;
            }

            // Копируем адрес из найденного узла в выходной массив
            char[] nodeAddress = node.Data.GetAddress();
            for (int i = 0; i < nodeAddress.Length && i < address.Length; i++)
            {
                if (nodeAddress[i] == '\0') break;  // Прерываем на терминальном символе
                address[i] = nodeAddress[i];        // Копируем символ
            }

            return true;  // Успешно нашли
        }

        // Реализация метода MakeNull из интерфейса IMap
        // Очищает весь словарь
        public void MakeNull()
        {
            _head = null;  // Удаляем ссылку на голову списка
        }

        // Реализация метода Print из интерфейса IMap
        // Выводит все элементы словаря в консоль
        public void Print()
        {
            Node<TKey, TValue>? current = _head;  // Начинаем с головы
            
            // Проходим по всем узлам
            while (current != null)
            {
                current.Data.Print();       // Печатаем данные узла
                current = current.Next;     // Переходим к следующему
            }
        }

        // Дополнительный метод для обратной совместимости
        // Принимает массив для заполнения результатом (ref-like семантика)

        // Метод для поиска узла по ключу
        // Возвращает кортеж: найденный узел и предыдущий узел
        private (Node<TKey, TValue>? node, Node<TKey, TValue>? prev) FindViaKey(char[] key)
        {
            Node<TKey, TValue>? current = _head;  // Начинаем с головы списка
            Node<TKey, TValue>? prev = null;      // Предыдущий узел изначально null

            // Проходим по всем узлам списка
            while (current != null)
            {
                // Если ключи совпадают - возвращаем текущий узел
                if (CompareCharArrays(current.Data.GetName(), key))
                    return (current, prev);

                prev = current;           // Обновляем предыдущий узел
                current = current.Next;   // Переходим к следующему узлу
            }

            // Узел не найден
            return (null, null);
        }

        // public bool Compute(char[] name, char[] address)
        // {
        //     // Ищем узел по ключу
        //     (Node<TKey, TValue>? node, Node<TKey, TValue>? prev) = FindViaKey(name);

        //     // Если узел не найден
        //     if (node == null)
        //         return false;

        //     // Копируем адрес в переданный массив
        //     char[] nodeAddress = node.Data.GetAddress();
        //     for (int i = 0; i < nodeAddress.Length && i < address.Length; i++)
        //     {
        //         if (nodeAddress[i] == '\0') break;  // Прерываем на терминальном символе
        //         address[i] = nodeAddress[i];        // Копируем символ
        //     }

        //     return true;  // Успешно нашли
        // }
    }
}