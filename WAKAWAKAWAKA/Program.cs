﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WAKAWAKAWAKA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            char[,] map = ReadMap("C:\\Users\\79532\\source\\repos\\WAKAWAKAWAKA\\WAKAWAKAWAKA\\bin\\Debug\\map.txt.txt");
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            Task.Run(() =>
            {
                while(true)
                {
                    pressedKey = Console.ReadKey();

                }
            });

            int pacmanX = 1;
            int pacmanY = 1;
            int score = 0;


            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Blue;
                DrawMap(map);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacmanX, pacmanY);
                Console.Write("@");


                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(35, 0);
                Console.Write($"Score: {score}");

                Thread.Sleep(1000);

                HandleInput(pressedKey, ref pacmanX, ref pacmanY, map, ref score);
            }

        }

        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines("C:\\Users\\79532\\source\\repos\\WAKAWAKAWAKA\\WAKAWAKAWAKA\\bin\\Debug\\map.txt.txt");

            char[,] map = new char[GetMaxLengthOfLine(file), file.Length];

            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = file[y][x];

            return map;
        }

        private static void DrawMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.Write("\n");
            }


        }

        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
        {
            int[] direction = GetDirection(pressedKey);

            int nextPacmanPositionX = pacmanX + direction[0];
            int nextPacmanPositionY = pacmanY + direction[1];

            char nextCell = map[nextPacmanPositionX, nextPacmanPositionY];

            if (nextCell == ' ' || nextCell == '.')
            {
                pacmanX = nextPacmanPositionX;
                pacmanY = nextPacmanPositionY;

                if (nextCell == '.')
                {
                    score++;
                    map[nextPacmanPositionX, nextPacmanPositionY] = ' ';
                }
            }

        }

        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.UpArrow)
                direction[1] = -1;
            else if (pressedKey.Key == ConsoleKey.DownArrow)
                direction[1] = 1;
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
                direction[0] = -1;
            else if (pressedKey.Key == ConsoleKey.RightArrow)
                direction[0] = 1;

            return direction;
        }

        private static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach (var line in lines)
                if (line.Length > maxLength)
                    maxLength = line.Length;
            return maxLength;
        }
    }
}
