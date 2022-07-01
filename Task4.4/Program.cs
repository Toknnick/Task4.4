using System;
using System.IO;

namespace Task4._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool isPlaying = true;
            int positionX;
            int positionY;
            int positionDirectX = 0;
            int positionDirectY = 0;
            int collectDots = 0;
            int allDots = 0;
            Console.WriteLine("После создания карты для выхода нажмите Enter.");
            Console.WriteLine("Введите номер карты:");
            string userInput = Console.ReadLine();
            char[,] map = ReadMap("map" + userInput, out positionX, out positionY, ref allDots);
            Console.Clear();
            DrawMap(map);

            while (isPlaying)
            {
                int bagPosition = 15;
                Console.SetCursorPosition(0, bagPosition);
                Console.WriteLine($"Собрано {collectDots}/{allDots}");

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo consoleKey = Console.ReadKey(true);
                    ChangeDirection(consoleKey, ref positionDirectX, ref positionDirectY, ref isPlaying);

                    if (map[positionX + positionDirectX, positionY + positionDirectY] != '#')
                    {
                        Move(ref positionX, ref positionY, positionDirectX, positionDirectY);
                        CollectDots(map, positionX, positionY, ref collectDots);
                    }
                }

                if (collectDots == allDots)
                {
                    isPlaying = false;
                    Console.Clear();
                    Console.WriteLine("Ты выйграл!");
                }
            }
        }

        static void CollectDots(char[,] map, int positionX, int positionY, ref int collectDots)
        {
            if (map[positionX, positionY] == '*')
            {
                collectDots++;
                map[positionX, positionY] = ' ';
            }
        }
    

        static void ChangeDirection(ConsoleKeyInfo consoleKey, ref int positionDirectX, ref int positionDirectY, ref bool isPlaying)
        {
            switch (consoleKey.Key)
            {
                case ConsoleKey.UpArrow:
                    positionDirectX = -1;
                    positionDirectY = 0;
                    break;
                case ConsoleKey.DownArrow:
                    positionDirectX = 1;
                    positionDirectY = 0;
                    break;
                case ConsoleKey.LeftArrow:
                    positionDirectX = 0;
                    positionDirectY = -1;
                    break;
                case ConsoleKey.RightArrow:
                    positionDirectX = 0;
                    positionDirectY = 1;
                    break;
                case ConsoleKey.Enter:
                    isPlaying = false;
                    break;
            }
        }

        static void Move(ref int positionX, ref int positionY, int positionDirectX, int positionDirectY)
        {
            Console.SetCursorPosition(positionY, positionX);
            Console.Write(" ");
            positionX += positionDirectX;
            positionY += positionDirectY;
            Console.SetCursorPosition(positionY, positionX);
            Console.Write('@');
        }

        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }
        }

        static char[,] ReadMap(string mapName, out int positionX, out int positionY, ref int allDots)
        {
            positionX = 0;
            positionY = 0;
            string[] newFile = File.ReadAllLines($"Maps/{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];

                    if (map[i, j] == '@')
                    {
                        positionX = i;
                        positionY = j;
                    }
                    else if (map[i, j] == ' ')
                    {
                        map[i, j] = '*';
                        allDots++;
                    }
                }
            }

            return map;
        }
    }
}
