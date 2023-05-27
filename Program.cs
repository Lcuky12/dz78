using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace ConsoleApp84
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            bool isPlaying = true;
            bool isAlive = true;
            char hero = '&';
            char coins = '.';
            char symbol = '@';
            char symbol1 = '$';
            char symbol2 = '%';
            int heroX = 0;
            int heroY = 0;
            int coinsX = 0;
            int coinsY = 0;
            int allCoins = 0;
            int collectCoins = 0;
            int heroDX = 0;
            int heroDY = 1;
            int enemyX = 0;
            int enemyY = 0;
            int enemyDX = 0;
            int enemyDY = -1;
            int enemy1X = 0;
            int enemy1Y = 0;
            int enemy1DX = 0;
            int enemy1DY = -1;
            int enemy2X = 0;
            int enemy2Y = 0;
            int enemy2DX = 0;
            int enemy2DY = -1;

            char[,] map = ReadMap("map1", ref heroX, ref heroY, ref coinsX, ref coinsY, ref allCoins, ref enemyX, ref enemyY, ref enemy1X, ref enemy1Y, ref enemy2X, ref enemy2Y, hero, coins, symbol, symbol1);
 
            DrawMap(map);


            while (isPlaying)
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, 30);
                Console.WriteLine($"Собрано монет {collectCoins}/{allCoins}");
              
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    ChangeDirection(key, ref heroDX, ref heroDY);
                }
               
                if (map[heroX + heroDX, heroY + heroDY] != '#')
                {
                    Move (ref heroX, ref heroY, heroDX, heroDY,hero);
                    
                    CollectCoins(map, heroX, heroY, ref collectCoins);
                }

                if (map[enemyX + enemyDX, enemyY + enemyDY] != '#')
                {
                    MoveEnemy(map, ref enemyX, ref enemyY, enemyDX, enemyDY,symbol);
                    MoveEnemy(map, ref enemy1X, ref enemy1Y, enemy1DX, enemy1DY,symbol1);
                    MoveEnemy(map, ref enemy2X, ref enemy2Y, enemy2DX, enemy2DY,symbol2);
                }
                else
                {

                }

                System.Threading.Thread.Sleep(150);

                if(collectCoins == allCoins)
                {
                    isPlaying= false;
                }
            } 
            
            Console.SetCursorPosition(0,35);
        
            if(collectCoins == allCoins)
            {
                Console.WriteLine("Вы победили!");
            }
        }

        static void Move(ref int X, ref int Y, int DX, int DY, char hero)
        {
            Console.SetCursorPosition(Y,X);
            Console.Write(" ");

            X += DX;
            Y += DY;

            Console.SetCursorPosition(Y,X);
            Console.Write(hero);
        }

        static void MoveEnemy(char[,] map, ref int X, ref int Y, int DX, int DY, char symbol)
        {
            Console.SetCursorPosition(Y,X);
            Console.Write(map[X,Y]);

            X += DX;
            Y += DY;

            Console.SetCursorPosition(Y,X);
            Console.Write(symbol);
        }

        static void CollectCoins(char [,] map, int heroX, int heroY, ref int collectCoins)
        {
            if (map[heroX, heroY] == '.')
            {
                collectCoins++;
                map[heroX, heroY] = ' ';
            }
        }
        static void ChangeDirection(ConsoleKeyInfo key, ref int DX, ref int DY)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    DX = -1;
                    DY = 0;
                    break;
                case ConsoleKey.DownArrow:
                    DX = 1;
                    DY = 0;
                    break;
                case ConsoleKey.LeftArrow:
                    DX = 0;
                    DY = -1;
                    break;
                case ConsoleKey.RightArrow:
                    DX = 0;
                    DY = 1;
                    break;
            }

        }

        static void DrawMap(char[,] map)
        {
            for(int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }   
        }

        static char[,] ReadMap(string mapName, ref int heroX, ref int heroY, ref int coinsX, ref int coinsY, ref int allCoins, ref int enemyX, ref int enemyY, ref int enemy1X, ref int enemy1Y, ref int enemy2X, ref int enemy2Y, char hero,char coins, char symbol, char symbol1)
        {
            string[] newFile = File.ReadAllLines($"Maps/{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++) 
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];

                    if (map[i, j] == hero)
                    {
                        heroX = i;
                        heroY = j;
                    }

                    else if (map[i,j] == '%')
                    {
                        enemy2X = i;
                        enemy2Y = j;
                    }

                    else if (map[i,j] == '$')
                    {
                        enemy1X= i;
                        enemy1Y = j;
                    }
  
                    else if (map[i, j] == '@')
                    {
                        enemyX= i;
                        enemyY = j;
                    }
  
                    else if (map[i, j] == ' ')
                    {
                        map[i, j] = '.';
                        allCoins++;
                    }
                }
            }

            return map;
        }
    }
}
