using System;

namespace game25
{

    class Program
    {
        #region Проверка завершения игры
        private static bool Win(byte[] A)
        {
            bool temp = true;
            for (int i = 0; i < 25; i++)
            {
                if (i < 5 && ((A[i] & 1) != 1))
                {
                    temp = false;
                    break;
                }
                else
                {
                    if (i < 15 && i > 9 && ((A[i] & 2) != 2))
                    {
                        temp = false;
                        break;
                    }
                    else
                    {
                        if (i > 19 && ((A[i] & 4) != 4))
                        {
                            temp = false;
                            break;
                        }
                    }
                }
            }
            return temp;
        }
        #endregion

        #region Инициализация начального состояния


        private static byte[] Init()
        {
            Random rnd = new Random();
            byte[] A = new byte[25];
            int i = 0;
            long multiple = 1;
            bool flag = false;
            while ((multiple != 24300000 || !flag))
            {
                if (i % 10 < 5)
                {
                    byte x = (byte)rnd.Next(0, 3);
                    multiple /= A[i] + 1;
                    A[i] = (byte)(1 << x);
                    multiple *= A[i] + 1;
                }
                else
                {
                    if (i % 2 == 1)
                    {
                        A[i] = 1 << 3;
                    }
                }
                i++;
                if (i == 25)
                    flag = true;
                i %= 25;
            }
            return A;
        }
        #endregion

        #region Обмен
        public static void Change(ref byte[] A, int first, int second)
        {
            A[first] ^= A[second];
            A[second] ^= A[first];
            A[first] ^= A[second];
        }
        #endregion

        #region Возможность обмена
        private static bool OpportunityChange(byte[] A, int first, int second)
        {
            if (A[first] == 8 || A[second] == 8 || (A[first] != 0 && A[second] != 0))
                return false;
            if (Math.Abs(first - second) == 1 || Math.Abs(first - second) == 5)
                return true;
            else return false;
        }

        #endregion Перемещение курсора

        #region Смещение при нажатии на клавишу

        private static int Shift(int res, char x)
        {
            switch (x)
            {
                case 'w':
                case 'ц':
                    if (res % 5 != 0)
                        res -= 1;
                    break;
                case 'a':
                case 'ф':
                    if (res >= 5)
                        res -= 5;
                    break;
                case 's':
                case 'ы':
                    if (res % 5 != 4)
                        res += 1;
                    break;
                case 'd':
                case 'в':
                    if (res < 20)
                        res += 5;
                    break;
                default:
                    break;
            }
            return res;
        }

        #endregion

        #region Перемещение по массиву
        private static void Go(byte[] mass)
        {
            int res = 0;
            while (!Win(mass))
            {
                char x = Console.ReadKey(true).KeyChar;
                while (x != ' ')
                {
                    res = Shift(res, x);
                    ShowMass(mass, res);
                    x = Console.ReadKey(true).KeyChar;
                }
                char shift = Console.ReadKey(true).KeyChar;
                int second = Shift(res, shift);
                if (OpportunityChange(mass, res, second))
                {
                    Change(ref mass, res, second);
                    ShowMass(mass, -1);
                }
            }
            Console.Clear();
            Console.WriteLine("YOU WIN!");
            Console.Read();
        }

        #endregion

        #region Вывод массива в игровом поле

        public static void ShowMass(byte[] mass, int position)
        {
            Console.Clear();
            Console.WriteLine("Цель игры - переместить цветные фишки на столбцы своего цвета.");
            Console.WriteLine("Для перемещения курсора используйте клавиши: w, a, s, d.");
            Console.WriteLine("Для перемещения фишки нажмите пробел на выбранной фишке, затем одну из клавиш w,a,s,d для выбора направления перемещения.");

            Console.ForegroundColor = (ConsoleColor)1;
            Console.Write("B ");
            Console.ForegroundColor = (ConsoleColor)2;
            Console.Write("G ");
            Console.ForegroundColor = (ConsoleColor)4;
            Console.Write("R");
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();

            int i = 0, j = 0;
            while (j < 5)
            {
                i = 0;
                while (i < 25)
                {
                    
                    if (i + j == position)
                    {
                        int temp = 0;
                        switch (mass[i + j])
                        {
                            case 1:
                            case 2:
                            case 4:
                                temp = mass[i + j] + 8;
                                break;
                            case 8:
                                temp = mass[i + j] - 1;
                                break;
                            

                        }
                        
                        Console.ForegroundColor = (ConsoleColor)temp;
                        Console.Write((char)0x6f);
                        Console.ResetColor();
                        i += 5;
                    }
                    else
                    {
                        Console.ForegroundColor = (ConsoleColor)mass[i + j];
                        Console.Write((char)0x0f);
                        Console.ResetColor();
                        i += 5;
                    }
                }
                Console.WriteLine();
                j++;
            }
            //Console.Read();
        }

        #endregion

        #region Запуск приложения

        private static void Run()
        {
            byte[] mass = Init();
            while (Win(mass))
            {
                mass = Init();
            }


            ShowMass(mass, -1);

            Go(mass);
        }

        #endregion
        static void Main(string[] args)
        {
            Run();
        }
    }
}
