using System;

namespace Practice9
{
    class Практика_Задание_8_11
    {
        static void Main(string[] args)
        {
            // Создание списка.
            int size = InputInt("Введите размер списка: ");
            while (size < 2)
            {
                Console.WriteLine("Необходимо ввести число 2 или более");
                size = InputInt("Введите размер списка: ");
            }
            DoublyLinkedList list = DoublyLinkedList.MakeList(size);
            DoublyLinkedList.Show(list);

            // Поиск вхождения в список.
            int search = InputInt("\n\nВведите число для проверки вхождения: ");
            list.Search(search);

            // Удаление элемента из списка.
            int remove = InputInt("\nВведите элемент, который необходимо удалить: ");
            list.Remove(remove);
            DoublyLinkedList.Show(list);

            Console.WriteLine("\n\nPress something to exit");
            Console.ReadKey();
            Random r = new Random();
        }

        static int InputInt(string msg, string errorMsg = "Необходимо ввести целое число")
        {
            int result = 0;

            Console.Write(msg);
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine(errorMsg);
            }

            return result;
        }
    }

    sealed class DoublyLinkedList
    {
        private int Value; // данные
        private DoublyLinkedList Left; // ссылка на предыдущий элемент
        private DoublyLinkedList Right; // ссылка на следующий элемент
        public DoublyLinkedList()
        {
            Value = 0;
            Left = null;
            Right = null;
        }
        public DoublyLinkedList(int value)
        {
            this.Value = value;
            Left = null;
            Right = null;
        }
        public void Search(int number)
        {
            if (Right == null)
            {
                // Ветка, если элемент является последним в списке.
                if (Value == number)
                {
                    Console.WriteLine("Число {0} найдено в списке", number);
                }
                else
                {
                    Console.WriteLine("Число {0} не найдено в списке", number);
                }
            }
            else
            {
                // Ветка, если элемент не последний.
                if (Value == number)
                {
                    Console.WriteLine("Число {0} найдено в списке", number);
                }
                else
                {
                    Right.Search(number); // проход по списку, отправка правого элемента
                }
            }
        }
        public void Remove(int item)
        {
            if (Right == null)
            {
                // Ветка, если элемент последний.
                if (Value == item)
                {
                    Left.Right = null;
                    Left = null;
                }
                else
                {
                    Console.WriteLine("Не было найдено элемента {0}", item);
                }
            }
            else
            {
                // Ветка, если элемент не последний
                if (Value == item)
                {
                    if (Left == null)
                    {
                        // Ветка, если элемент первый.
                        Right.Left.Value = Right.Value;
                        Right.Remove(Right.Value);
                    }
                    else
                    {
                        // Ветка, если элемент не первый.
                        Right.Left = Left;
                        Left.Right = Right;
                        Right = null;
                        Left = null;
                    }
                }
                else
                {
                    Right.Remove(item); // проход по списку, отправка правого элемента
                }
            }
        }
        public static DoublyLinkedList MakeList(int size)
        {
            if (size > 1)
            {
                DoublyLinkedList point = GenerateList(size); // создание списка "с конца"
                return HeadToLeft(point); // перенос головы списка из конца в начало
            }
            else
            {
                Console.WriteLine("Длина списка должна быть больше 1");
                return null;
            }
        }
        private static DoublyLinkedList GenerateList(int size)
        {
            if (size < 1)
            {
                return null;
            }

            DoublyLinkedList point = new DoublyLinkedList(size);
            point.Left = GenerateList(--size);
            if (point.Left != null)
            {
                point.Left.Right = point;
            }

            return point;
        }
        private static DoublyLinkedList HeadToLeft(DoublyLinkedList point)
        {
            if (point.Left == null)
            {
                return point;
            }
            return HeadToLeft(point.Left);
        }
        public static void Show(DoublyLinkedList point)
        {
            if (point != null)
            {
                Console.Write(point.Value + " ");
                Show(point.Right);
            }
        }
    }
}

