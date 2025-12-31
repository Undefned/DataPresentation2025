using System;

namespace Lab2.Map.LinkedList
{
    public class Addressee
    {
        private readonly char[] _name;    // имя (до 20 символов)
        private readonly char[] _address; // адрес (до 50 символов)

        public const int NAME_CAPACITY = 20;
        public const int ADDRESS_CAPACITY = 50;

        // Конструктор из массивов char
        public Addressee(char[] name, char[] address)
        {
            _name = new char[NAME_CAPACITY];
            _address = new char[ADDRESS_CAPACITY];
            
            CopyArray(name, _name);
            CopyArray(address, _address);
        }

        // Геттеры
        public char[] GetName()
        {
            char[] copy = new char[NAME_CAPACITY];
            Array.Copy(_name, copy, NAME_CAPACITY);
            return copy;
        }

        public char[] GetAddress()
        {
            char[] copy = new char[ADDRESS_CAPACITY];
            Array.Copy(_address, copy, ADDRESS_CAPACITY);
            return copy;
        }

        // Строковые представления
        public string NameAsString => new string(_name).TrimEnd('\0');
        public string AddressAsString => new string(_address).TrimEnd('\0');

        // Печать
        public void Print()
        {
            Console.Write("Имя: ");
            PrintCharArray(_name);
            Console.Write(", Адрес: ");
            PrintCharArray(_address);
            Console.WriteLine();
        }

        // Сравнение
        public bool Equals(Addressee other)
        {
            if (other == null) return false;
            return CompareCharArrays(_name, other._name) && CompareCharArrays(_address, other._address);
        }

        public override string ToString() => $"Имя: '{NameAsString}', Адрес: '{AddressAsString}'";

        // Вспомогательные методы
        private static void CopyArray(char[] source, char[] destination)
        {
            int length = Math.Min(source.Length, destination.Length);
            
            for (int i = 0; i < length; i++)
            {
                if (source[i] == '\0') break;
                destination[i] = source[i];
            }
        }

        private static void PrintCharArray(char[] array)
        {
            foreach (char c in array)
            {
                if (c == '\0') break;
                Console.Write(c);
            }
        }

        private static bool CompareCharArrays(char[] a1, char[] a2)
        {
            int len = Math.Min(a1.Length, a2.Length);
            
            for (int i = 0; i < len; i++)
            {
                if (a1[i] == '\0' && a2[i] == '\0')
                    return true;
                
                if (a1[i] != a2[i])
                    return false;
            }
            
            return true;
        }
    }
}