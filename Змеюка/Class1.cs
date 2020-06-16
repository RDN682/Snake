using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Змеюка
{
    class Game
    {

        public static void Move(int n, int m, ref int xz, ref int yz, string move, ref string[,] map)
        {
            map[yz, xz] = " ";
            switch (move)
            {
                case "Up":
                    if (yz - 1 >= 0) yz -= 1;
                    break;

                case "Down":

                    if (yz + 1 < n) yz += 1;
                    break;
                case "Left":
                    if (xz - 1 >= 0) xz -= 1;
                    break;
                case "Right":
                    if (xz + 1 < m) xz += 1;
                    break;
            }
            map[yz, xz] = "^";
        }
        public static void EatGenerate(int n, int m, ref string[,] map, ref int xv, ref int yv, int xz, int yz, ref bool eat, int[] masx, int[] masy)
        {
            if (xv == xz && yv == yz)
            {
                eat = true;
                Random rand = new Random();
                bool s = true;
                while (s == true)
                {
                    xv = rand.Next(1, m - 2);
                    yv = rand.Next(1, n - 2);
                    int p = 0;
                    if (xv != xz && yv == yz || yv != yz && xv == xz || xv != xz && yv != yz)
                    {
                        for (int i = 0; i < masx.Length; i++)
                        {
                            if (xv != masx[i] && yv == masy[i] || yv != masy[i] && xv == masx[i] || xv != masx[i] && yv != masy[i])
                                p++;
                            if (p == masx.Length)
                                s = false;
                        }
                    }
                }
                map[yv, xv] = "*";
            }
        }
        public static void Tail(ref int[] masx, ref int[] masy, ref string[,] map, ref bool eat, ref int size, int xz, int yz)
        {
            if (eat == true)
            {
                size++;
                Array.Resize<int>(ref masx, size);
                Array.Resize<int>(ref masy, size);
                eat = false;
            }
            map[masy[size - 1], masx[size - 1]] = " ";
            for (int i = masx.Length - 1; i > 0; i--)
            {
                masx[i] = masx[i - 1];
                masy[i] = masy[i - 1];
            }
            masx[0] = xz;
            masy[0] = yz;
            for (int i = 0; i < size; i++)
            {
                map[masy[i], masx[i]] = "^";
            }
        }
        public static void Otrisovka(int n, int m, ref int xz, ref int yz, ref int yv, ref int xv, ref string[,] map)
        {
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    map[i, j] = " ";
                    if (i == 0 || j == 0 || i == n - 1 || j == m - 1)
                        map[i, j] = "#";
                }
            }
            yz = n - 2; xz = 1;
            map[yz, xz] = "^";
            while (true)
            {
                xv = rand.Next(1, m - 2);
                yv = rand.Next(1, n - 2);
                if (xv == xz && yv != yz || xv != xz && yv == yz || xv != xz && yv != yz)
                {
                    break;
                }
            }
            map[yv, xv] = "*";
        }
        public static void Win(int n, int m, string[,] map, ref bool game)
        {
            int p = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i != 0 || j != 0 || i != n - 1 || j != m - 1)
                    {
                        if (map[i, j] == "X")
                        {
                            p++;
                        }
                    }
                }
                if (p == map.Length - n * 2 - m * 2 + 4)
                {
                    game = false;
                    Console.WriteLine("Игра окончена ");
                    Console.WriteLine("Вы ВЫЙГРАЛИ");
                    Console.ReadLine();
                }
            }
        }
        public static void Lose(int n, int m, int[] masx, int[] masy, int xz, int yz, ref bool game, int size)
        {
            if (xz == 0 || xz == m - 1 || yz == 0 || yz == n - 1)
            {
                game = false;
                Console.WriteLine("Игра окончена ");
                Console.WriteLine("Вы проиграли(");
                Console.ReadLine();
            }
            for (int i = 1; i < size; i++)
            {
                if (masx[i] == xz && masy[i] == yz)
                {
                    game = false;
                    Console.WriteLine("Игра окончена ");
                    Console.WriteLine("Вы проиграли(");
                    Console.ReadLine();
                }
            }
        }

    }
}