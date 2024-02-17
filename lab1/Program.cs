using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Xml.Linq;
class Program
{
    // Функция хэширования - сумма цифр элемента
    static int HashFunction(int value)
    {
        // Суммируем цифры числа
        int sum = 0;
        value %= 1000;
        while (value != 0)
        {
            sum += value % 10;
            value /= 10;
        }

        // Возвращаем остаток от деления суммы на t
        return sum;
    }
    static void quadracticProb(int[] hashTable, int[] elements, out int allCount) 
    {
        allCount = 0;
        // Хэширование ключей и обработка коллизий методом квадратичного опробовывания
        for (int i = 0; i < elements.Length; ++i)
        {
            int deg = 1;
            int fl = 0; //Счетчик для неудачных попыток

            if (hashTable[HashFunction(elements[i])] == -1)
            {
                //Присвоение хэш-таблице, если ячейка пуста
                hashTable[HashFunction(elements[i])] = elements[i];
                allCount++;
            }
            else
            {
                allCount++;

                while (fl < hashTable.Length)
                {
                    int index = (HashFunction(elements[i]) + deg * deg) % hashTable.Length;

                    if (hashTable[index] == -1)
                    {
                        // Квадратичное пробирование
                        hashTable[index] = elements[i];
                        deg = 1;
                        fl = 0;
                        allCount++;
                        break;
                    }
                    else
                    {
                        deg++;
                        fl++;
                        allCount++;
                    }

                    if (fl >= 30)
                    {
                        // Переключение на линейное опробирование после 30 неудачных попыток
                        deg = 1;
                        while (hashTable[index] != -1)
                        {
                            index = (HashFunction(elements[i]) + deg) % hashTable.Length;
                            deg++;
                            fl++;
                            allCount++;
                        }
                        hashTable[index] = elements[i];
                        break;
                    }
                }
            }
        }
    }
    static int find( int[] hashTable, int findValue)
    {
        int hashFindValue = HashFunction(findValue);


        if  (hashTable[hashFindValue] <= 0) 
        {
            Console.Write($"Элемента в таблице нет - пустая ячейка\n");
            return -1;
        }
        else if (hashTable[hashFindValue] == findValue)
        {
            Console.Write($"Индекс - { hashFindValue}, Значение - { findValue} \n");
            return -2;
        }
        else
        {
            int fl = 0;
            int deg = 1;
            while (hashTable[hashFindValue] != findValue) 
            {
                if (fl < 30)
                {
                    hashFindValue = (HashFunction(findValue) + deg * deg) % hashTable.Length;
                    fl++;
                    deg++;
                }
                else if ((fl >= 30) && (fl < hashTable.Length))
                {
                    hashFindValue = (HashFunction(findValue) + deg) % hashTable.Length;
                    deg++;
                    fl++;
                }
                 else
                {
                    break;
                }
            }

            if (hashTable[hashFindValue] == findValue)
            {
                Console.Write($"Найден при помощи устранения коллизий\n");
                Console.Write($"Индекс - { hashFindValue}, Значение - { findValue} \n");
                return -2;
            }
            else if (hashTable[hashFindValue] == -1)
            {
                Console.Write($"Найдена пустая ячейка в ходе устранения коллизии\n");
                return hashFindValue;
            }
            else if (fl >=30)
            {
                Console.Write($"Данное число отсутствует\n");
                return -1;
            }
        }
        return 0;
    }

    static int findWithoutMessages(int[] hashTable, int findValue)
    {
        int hashFindValue = HashFunction(findValue);


        if (hashTable[hashFindValue] <= 0)
        {
            Console.Write($"Элемента в таблице нет - пустая ячейка\n");
            return -1;
        }
        else if (hashTable[hashFindValue] == findValue)
        {
            Console.Write($"Индекс - { hashFindValue}, Значение - { findValue} \n");
            return hashFindValue;
        }
        else
        {
            int fl = 0;
            int deg = 1;
            while (hashTable[hashFindValue] != findValue)
            {
                if (fl < 30)
                {
                    hashFindValue = (HashFunction(findValue) + deg * deg) % hashTable.Length;
                    fl++;
                    deg++;
                }
                else if ((fl >= 30) && (fl < hashTable.Length))
                {
                    hashFindValue = (HashFunction(findValue) + deg) % hashTable.Length;
                    deg++;
                    fl++;
                }
                else
                {
                    break;
                }
            }

            if (hashTable[hashFindValue] == findValue)
            {
                Console.Write($"Найден при помощи устранения коллизий\n");
                Console.Write($"Индекс - { hashFindValue}, Значение - { findValue} \n");
                return hashFindValue;
            }
            else if (hashTable[hashFindValue] == -1)
            {
                Console.Write($"Найдена пустая ячейка в ходе устранения коллизии\n");
                return hashFindValue;
            }
            else if (fl >= 30)
            {
                Console.Write($"Данное число отсутствует\n");
                return -1;
            }
        }
        return 0;
    }
    static void add(int[] hashTable, int addValue)
    {
        int answer = find(hashTable, addValue);
        if (answer == -2)
        {
            Console.Write($"Значение уже существует\n");
        }
        else if (answer == -1)
        {
            Console.Write($"Добавлено в таблицу\n");
            if (hashTable[HashFunction(addValue)] == -1)
            {
                //Присвоение хэш-таблице, если ячейка пуста
                hashTable[HashFunction(addValue)] = addValue;
                Console.Write($"Добавлено по индексу {hashTable[HashFunction(addValue)]}\n");
            }
            else
            {
                int fl = 0;
                int deg = 1;
                while (fl < hashTable.Length)
                {
                    int index = (HashFunction(addValue) + deg * deg) % hashTable.Length;

                    if (hashTable[index] == -1)
                    {
                        // Квадратичное пробирование
                        hashTable[index] = addValue;
                        deg = 1;
                        fl = 0;
                        break;
                    }
                    else
                    {
                        deg++;
                        fl++;
                    }

                    if (fl >= 30)
                    {
                        // Переключение на линейное опробирование после 30 неудачных попыток
                        deg = 1;
                        while (hashTable[index] != -1)
                        {
                            index = (HashFunction(addValue) + deg) % hashTable.Length;
                            deg++;
                            fl++;
                        }
                        hashTable[index] = addValue;
                        break;
                    }
                }
            }
        }
        else {
            hashTable[answer] = addValue;
        }

        Console.WriteLine("Хэш-таблица:");
        for (int i = 0; i < hashTable.Length; ++i)
        {
            if (i % 5 == 0)
                Console.WriteLine();

            Console.Write($"{i})\t");

            if (hashTable[i] == -1)
                Console.Write("-----\t");
            else
                Console.Write($"{hashTable[i]}\t");
        }
    }
    static void delete(int [] hashTable, int deleteValue)
    {
        int answer = findWithoutMessages(hashTable, deleteValue);
        if (answer >= 0)
        {
            Console.WriteLine($"Значение {deleteValue} удалено из индекса {answer}\t");
            hashTable[answer] = -1;
        }

        Console.WriteLine("Хэш-таблица:");
        for (int i = 0; i < hashTable.Length; ++i)
        {
            if (i % 5 == 0)
                Console.WriteLine();

            Console.Write($"{i})\t");

            if (hashTable[i] == -1)
                Console.Write("-----\t");
            else
                Console.Write($"{hashTable[i]}\t");
        }
    }
    static void change(int [] hashTable, int changeValue)
    {
        int answer = findWithoutMessages(hashTable, changeValue);
        if (answer >= 0)
        {
            Console.WriteLine($"Значение {changeValue} удалено из индекса {answer}\t");
            hashTable[answer] = -1;
            Console.WriteLine($"Введите новое число:\t");
            int newValue = Convert.ToInt32(Console.ReadLine());
            add(hashTable, newValue);
        }
    }
    static void Main()
    {
        Random rd = new Random();
        int m = 53;
        Console.WriteLine($"Количество элементов: {m}");

        int n = 5;
        Console.WriteLine($"Размерность: {n} ");

        int min = 0, max = (int)Math.Pow(10, n);
        if (n != 1)
            min = (int)Math.Pow(10, n - 1);

        int t = m * 3 / 2;


        int[] elements = new int[m];
        int[] hashTable = new int[t];

        // Инициализация хэш-таблицы значениями -1
        for (int i = 0; i < t; ++i)
            hashTable[i] = -1;

        // Генерация случайных уникальных ключей
        for (int i = 0; i < m; ++i)
        {
            elements[i] = rd.Next(min, max);
            for (int j = 0; j < i; j++)
            {
                // Проверка на уникальность ключа
                if (elements[j] == elements[i])
                {
                    elements[i] = rd.Next(min, max);
                    j = -1;
                }
            }
        }


        Console.WriteLine("Входные элементы:");
        for (int i = 0; i < m; ++i)
        {

            if (i % 10 == 0)
                Console.WriteLine();
            Console.Write($"{elements[i] + " "}");
        }
        Console.WriteLine("\n");

        int allCount = 0;
        quadracticProb(hashTable, elements, out allCount);
        
        
        Console.WriteLine("Хэш-таблица:");
        for (int i = 0; i < t; ++i)
        {
            if (i % 5 == 0)
                Console.WriteLine();

            Console.Write($"{i})\t");

            if (hashTable[i] == -1)
                Console.Write("-----\t");
            else
                Console.Write($"{hashTable[i]}\t");
        }

        Console.WriteLine($"\n\nКоэффициент заполнения таблицы: {(float)m / t:F3}");
        Console.WriteLine($"Текущий размер таблицы: {t}");
        Console.WriteLine($"Общее число проб: {allCount - 1}");
        Console.WriteLine($"Среднее число проб, необходимых для размещения некоторого ключа в таблице: {(double)(allCount - 1) / m:F3}");




        
        int choice;
        bool flag = true;
        while (flag == true)
        {
            Console.WriteLine($"\nФункции:");
            Console.WriteLine($"Поиск - 1");
            Console.WriteLine($"Добавить - 2");
            Console.WriteLine($"Удалить - 3");
            Console.WriteLine($"Заменить - 4");
            Console.WriteLine($"Выход - 5");
            Console.WriteLine($"\nВведите номер функции:");
            choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1: Console.Write($"Введите искомое:"); int findValue = Convert.ToInt32(Console.ReadLine()); find(hashTable, findValue); break;
                case 2: Console.Write($"Введите добавляемое число:"); int addValue = Convert.ToInt32(Console.ReadLine()); add(hashTable,addValue); break;
                case 3: Console.Write($"Введите удаляемое число:"); int deleteValue = Convert.ToInt32(Console.ReadLine()); delete(hashTable, deleteValue); break;
                case 4: Console.Write($"Введите изменяемое число:"); int changeValue = Convert.ToInt32(Console.ReadLine()); change(hashTable, changeValue); break;
                case 5: flag = false; break;
            }
        }
        

        
    }
}


