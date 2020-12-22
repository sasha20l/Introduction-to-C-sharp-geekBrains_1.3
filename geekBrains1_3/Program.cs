using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geekBrains1_3
{
    class Program
    {
        static int scorePlayer;
        static int scoreEnemy;

        static void Main(string[] args)
        {
            string player;
            string enemyPlayer;
            Cell[,] cellPlayer = new Cell[10, 10];
            Cell[,] cellEnemy = new Cell[10, 10];
            Ship[] shipPlayer = new Ship[10];
            Ship[] shipEnemy = new Ship[10];


            Title();
            player = Names("Как тебя зовут ?");
            enemyPlayer = Names("Как зовут твоего противника ?");
            Console.Clear();
            cellPlayer = FieldCreation(cellPlayer); //Создание игрового поля игрока
            cellEnemy = FieldCreation(cellEnemy); //Создание игрового поля противника


            ViewField(cellPlayer, "Игровое поле " + player);

            ArmyFormation(cellPlayer, cellEnemy, player, enemyPlayer, shipPlayer, shipEnemy);

            while (true)
            {
                Play(cellPlayer, cellEnemy, player, enemyPlayer, shipPlayer, shipEnemy);
            }
        }

        enum Direction
        {
            UP,
            Down,
            Left,
            Right
        }

        static void ArmyFormation(Cell[,] cellPlayer, Cell[,] cellEnemy, string player, string enemyPlayer, Ship [] shipPlayer, Ship[] shipEnemy)
        {
            boat(cellPlayer, player, shipPlayer);
            destroyer(cellPlayer, player, shipPlayer);
            cruiser(cellPlayer, player, shipPlayer);
            battleship(cellPlayer, player, shipPlayer);
            ViewField(cellPlayer, "Игровое поле " + player);

            boatEnemy(cellEnemy, enemyPlayer, shipEnemy);
            destroyerEnemy(cellEnemy, enemyPlayer, shipEnemy);
            cruiserEnemy(cellEnemy, enemyPlayer, shipEnemy);
            battleshipEnemy(cellEnemy, enemyPlayer, shipEnemy);
            ViewField(cellEnemy, "Игровое поле " + enemyPlayer);
            Console.WriteLine("Вы и Ваш соперник расставили корабли на поле боя");
            Console.ReadLine();
            Console.Clear();

        }

        static void Play(Cell[,] cellPlayer, Cell[,] cellEnemy, string player, string enemyPlayer, Ship[] shipPlayer, Ship[] shipEnemy)
        {
            Random rnd = new Random();
            int coordinatX;
            int coordinatY;

            bool gameFlag = true;
            while (gameFlag)
            {
                ViewField(cellEnemy, enemyPlayer);
                Console.WriteLine("Укажите координаты по которым будете вести огонь.");

                coordinatX = CoordinateInput("Координата по Y");
                coordinatY = CoordinateInput("Координата по X");

                if (cellEnemy[coordinatX, coordinatY].hit)
                {
                    Console.WriteLine($"Вы уже вели огонь по координатам Y = {coordinatX}, X = {coordinatY}");
                    continue;
                }else if (cellEnemy[coordinatX, coordinatY].ship == false)
                {
                    Console.Clear();
                    cellEnemy[coordinatX, coordinatY].hit = true;
                    cellEnemy[coordinatX, coordinatY].view = "V";
                    ViewField(cellEnemy, "Игровое поле " + enemyPlayer);
                    Console.WriteLine($"Y = {coordinatX}, X = {coordinatY} - Промах");
                    gameFlag = false;
                    Console.ReadLine();
                }else if (cellEnemy[coordinatX, coordinatY].ship == true)
                {

                    Console.Clear();
                    cellEnemy[coordinatX, coordinatY].hit = true;
                    cellEnemy[coordinatX, coordinatY].view = "X";
                    ViewField(cellEnemy, "Игровое поле " + enemyPlayer);

                    string nameShips = cellEnemy[coordinatX, coordinatY].ships.name;
                    for (int i = 0; i < shipEnemy.Length; i++)
                    {
                        if (shipEnemy[i].name == nameShips)
                        {
                            shipEnemy[i].life -= 1;
                            scorePlayer += shipEnemy[i].score;
                            if (shipEnemy[i].life > 0) Console.WriteLine($"Y = {coordinatX}, X = {coordinatY} - Попал!!!");
                            else Console.WriteLine($"Y = {coordinatX}, X = {coordinatY} - Убил!!!");
                            ViewField(cellEnemy, "Игровое поле " + enemyPlayer);
                            gameFlag = false;
                            Console.ReadLine();

                            if (scorePlayer == 164)
                            {
                                Console.WriteLine($"Вы побеждаете со счетом 164 очка, у противника {scoreEnemy} очков");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
            
                    }

                }
            }


            gameFlag = true;
            while (gameFlag)
            {
                coordinatX = rnd.Next(0, 9);
                coordinatY = rnd.Next(0, 9);

                if (cellPlayer[coordinatX, coordinatY].hit)
                {
                    continue;
                }
                else if (cellPlayer[coordinatX, coordinatY].ship == false)
                {
                    Console.Clear();
                    cellPlayer[coordinatX, coordinatY].hit = true;
                    cellPlayer[coordinatX, coordinatY].view = "V";
                    ViewField(cellPlayer, "Игровое поле " + player);
                    Console.WriteLine($"Противник ударил по координатам Y = {coordinatX}, X = {coordinatY} - и Промахнулся");
                    gameFlag = false;
                    Console.ReadLine();
                }
                else if (cellPlayer[coordinatX, coordinatY].ship == true)
                {

                    Console.Clear();
                    cellPlayer[coordinatX, coordinatY].hit = true;
                    cellPlayer[coordinatX, coordinatY].view = "A";
                    ViewField(cellPlayer, "Игровое поле " + player);

                    string nameShips = cellPlayer[coordinatX, coordinatY].ships.name;
                    for (int i = 0; i < shipPlayer.Length; i++)
                    {
                        if (shipPlayer[i].name == nameShips)
                        {
                            shipPlayer[i].life -= 1;
                            scoreEnemy += shipEnemy[i].score;
                            if (shipPlayer[i].life > 0) Console.WriteLine($"Противник ударил по координатам Y = {coordinatX}, X = {coordinatY} - Попал!!!");
                            else Console.WriteLine($"Противник ударил по координатам Y = {coordinatX}, X = {coordinatY} - Убил!!!");
                            ViewField(cellPlayer, "Игровое поле " + player);
                            gameFlag = false;
                            Console.ReadLine();

                            if (scoreEnemy == 164)
                            {
                                Console.WriteLine($"Вы проигрываете компьютеру со счетом {scorePlayer} очка, у противника 164 очков");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }

                    }

                }
            }

        }

        static void Title() // титульный экран
        {
            Console.WriteLine(@"
                    ░░░░░░░░░░░░░░░▄▄░░░░░░░░░░░░░░░
                    ░░░░░░░░░░░░░░████░░░░░░░░░░░░░░
                    ░░░░░░░░░░░░░░████░░░░░░░░░░░░░░
                    ░░░░░▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄░░░░░
                    ░░░░░██░░░░░░░░░░░░░░░░░░██░░░░░
                    ░░░░░██▄▄░░▄▄░░▄▄░░▄▄░░▄▄██░░░░░
                    ░░░░░░░▀█░░██░░██░░██░░█▀░░░░░░░
                    ░░░░░░░░█░░░░░░░░░░░░░░█░░░░░░░░
                    ░░░░░░░░▀░▄▄▄██░░██▄▄▄░▀░░░░░░░░
                    ░░░░▄▄▄████████░░████████▄▄▄░░░░
                    ░░░████████████░░████▀███████░░░
                    ░░░░███████████░░███▄░▄█████░░░░
                    ░░░░░██████████░░██▀█░█▀███░░░░░
                    ░░░░░░█████████░░███▄▄▄███░░░░░░
                    ░░░░░░░████████░░████████░░░░░░░
                    ░░░░░░░▀░░░▀██▀░░▀██▀░░░▀░░░░░░░
                    ▄▄███▄▄▄███▄▄▄▄██▄▄▄▄███▄▄▄███▄▄
                    ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀");
            Console.WriteLine(@"                  Добро пожаловать в игру Морской бой!
                    Нажмите клавишу для продолжения.");
            Console.ReadKey();
        }

        static string Names(string player) // запрос имен
        {
            Console.WriteLine(player);
            return Console.ReadLine();
        }

        static Cell[,] FieldCreation(Cell[,] cell) // создание игровых ячеек на поле.
        {
            int[] coord = new int[2];
            for (var i = 0; i < cell.GetLength(0); i++)
            {
                for (var j = 0; j < cell.GetLength(0); j++)
                {
                    coord[0] = i;
                    coord[1] = j;
                    cell[i, j] = new Cell("O", coord, false, false, true);
                }
            }
            return cell;
        }


        static void ViewField(Cell[,] cell, string comment) //Визуализация игрового поля
        {
            System.Console.WriteLine(comment + "\n");
            System.Console.WriteLine(" Y\n");

            for (var i = 0; i < cell.GetLength(0); i++)
            {
                System.Console.Write(" " + i);
                for (var j = 0; j < cell.GetLength(0); j++)
                {
                    if (j == 0) System.Console.Write("   ");
                    System.Console.Write($"{cell[i, j].view} ");

                }
                System.Console.WriteLine();

            }
            System.Console.WriteLine("\n     0 1 2 3 4 5 6 7 8 9  X");
            //Console.ReadLine();
        }


        static void boat(Cell[,] cellPlayer, string player, Ship []shipArray) //Размещение однопалубных лодок игроком
        {
            System.Console.WriteLine();
            int limit = 4;
            string name = "однопалубных лодок";
            int coordinatX;
            int coordinatY;


            for (int i = 0; i < limit; i++)
            {
                Ship ship = new Ship("Лодка № " + i, "boat", 1, 1, 5);
                shipArray[i] = ship;
                while (true)
                {
                    System.Console.WriteLine($"Размести {i + 1} из {limit} {name} на игровом поле");
                    coordinatX = CoordinateInput("Координата по Y");
                    coordinatY = CoordinateInput("Координата по X");

                    if (cellPlayer[coordinatX, coordinatY].ship)
                    {
                        System.Console.WriteLine($"Клетка по координатам Х= {coordinatX} Y= {coordinatY} Уже занята");
                        continue;
                    }
                    else if (!cellPlayer[coordinatX, coordinatY].accommodation)
                    {
                        System.Console.WriteLine($"Клетка по координатам Х= {coordinatX} Y= {coordinatY} Слишком близко к другому кораблю");
                        continue;
                    }
                    cellPlayer[coordinatX, coordinatY].ship = true;
                    cellPlayer[coordinatX, coordinatY].ships = ship;
                    cellPlayer[coordinatX, coordinatY].view = "X";
                    BanForPlacement(coordinatX, coordinatY, ship, cellPlayer);
                    ViewField(cellPlayer, "Игровое поле " + player);
                    break;
                }
            }
        }

        static void boatEnemy(Cell[,] cellPlayer, string player, Ship[] shipArray) //Размещение однопалубных лодок компьютером
        {
            int limit = 4;
            string name = "однопалубных лодок";
            int coordinatX;
            int coordinatY;
            Random rnd = new Random();


            for (int i = 0; i < limit; i++)
            {
                Ship ship = new Ship("Лодка № " + i, "boat", 1, 1, 5);
                shipArray[i] = ship;
                while (true)
                {
                    coordinatX = rnd.Next(0, 9);
                    coordinatY = rnd.Next(0, 9);

                    if (cellPlayer[coordinatX, coordinatY].ship)
                    {  
                        continue;
                    }
                    else if (!cellPlayer[coordinatX, coordinatY].accommodation)
                    {
                        continue;
                    }
                    cellPlayer[coordinatX, coordinatY].ship = true;
                    cellPlayer[coordinatX, coordinatY].ships = ship;
                    cellPlayer[coordinatX, coordinatY].view = "O";
                    BanForPlacement(coordinatX, coordinatY, ship, cellPlayer);
                    break;

                }

            }

        }

        static void destroyer(Cell[,] cellPlayer, string player, Ship[] shipArray) //Размещение двупалубных эсминцев игроком
        {
            System.Console.WriteLine();
            int limit = 3; // количество лодок
            string name = "двупалубных эсминцев";
            
            for (int i = 0; i < limit; i++)
            {
                Ship ship = new Ship("Эсминец № " + i, "destroyer", 2, 2, 7);
                shipArray[4 + i] = ship;
                int[][] copyCoordXY = new int[ship.size][]; // временная переменная для храниния координат ячеек корабля
                arrangement(ship, i, limit, name, cellPlayer, player, shipArray);
            }

        }

        static void arrangement(Ship ship, int i, int limit, string name, Cell[,] cellPlayer, string player, Ship[] shipArray)
        {
            int coordinatX;
            int coordinatY;
            int direction; // направление лотки
            int[][] copyCoordXY = new int[ship.size][]; // временная переменная для храниния координат ячеек корабля
            while (true)
            {
                System.Console.WriteLine($"Размести {i + 1} из {limit} {name} на игровом поле");
                coordinatX = CoordinateInput("Координата по Y");
                coordinatY = CoordinateInput("Координата по X");
                direction = directionInput("Укажите направление корабля 0 - вверх, 1 - в право, 2 - вниз, 3 - в лево");
                ship.direction = direction == 0 ? "Left" : direction == 1 ? "Up" : direction == 2 ? "Right" : "Down";
                bool flagActiveShip = true; //Проверка размещен ли корабль полностью, или один из его ячеек находиться в недопустимом поле..
                try
                {
                    for (int j = 0; j < ship.size; j++)
                    {
                        if (cellPlayer[coordinatX, coordinatY].ship)
                        {
                            System.Console.WriteLine($"Клетка по координатам Х= {coordinatX} Y= {coordinatY} Уже занята");
                            flagActiveShip = false;
                            break;
                        }
                        else if (!cellPlayer[coordinatX, coordinatY].accommodation)
                        {
                            System.Console.WriteLine($"Клетка по координатам Х= {coordinatX} Y= {coordinatY} Слишком близко к другому кораблю");
                            flagActiveShip = false;
                            break;
                        }

                        copyCoordXY[j] = new int[2] { coordinatX, coordinatY };

                        coordinatX = Shift(ship, coordinatX, 1 + j, "X");
                        coordinatY = Shift(ship, coordinatY, 1 + j, "Y");

                    }
                    if (flagActiveShip)
                    {
                        foreach (int[] k in copyCoordXY)
                        {
                            cellPlayer[k[0], k[1]].ship = true;
                            cellPlayer[k[0], k[1]].ships = ship;
                            cellPlayer[k[0], k[1]].view = "X";
                            BanForPlacement(k[0], k[1], ship, cellPlayer);// отмечаем клетки, где нельзя размещать другие корабли (слишком близко)
                        }
                        ViewField(cellPlayer, "Игровое поле " + player);
                        break;
                    }
                }
                catch
                {
                    System.Console.WriteLine($"Клетка по координатам Х= {coordinatX} Y= {coordinatY} и заданному направлению выходит за границы поля");
                }

            }

        }

        static void arrangementEnemy(Ship ship, int i, int limit, string name, Cell[,] cellPlayer, string player, Ship[] shipArray)
        {
            int coordinatX;
            int coordinatY;
            int direction; // направление лотки
            int[][] copyCoordXY = new int[ship.size][]; // временная переменная для храниния координат ячеек корабля
            Random rnd = new Random();
            while (true)
            {
                coordinatX = rnd.Next(0, 9);
                coordinatY = rnd.Next(0, 9);
                direction = rnd.Next(0, 3);
                ship.direction = direction == 0 ? "Left" : direction == 1 ? "Up" : direction == 2 ? "Right" : "Down";
                bool flagActiveShip = true; //Проверка размещен ли корабль полностью, или один из его ячеек находиться в недопустимом поле..
                try
                {
                    for (int j = 0; j < ship.size; j++)
                    {
                        if (cellPlayer[coordinatX, coordinatY].ship)
                        {
                            flagActiveShip = false;
                            break;
                        }
                        else if (!cellPlayer[coordinatX, coordinatY].accommodation)
                        {
                            flagActiveShip = false;
                            break;
                        }

                        copyCoordXY[j] = new int[2] { coordinatX, coordinatY };

                        coordinatX = Shift(ship, coordinatX, 1 + j, "X");
                        coordinatY = Shift(ship, coordinatY, 1 + j, "Y");

                    }
                    if (flagActiveShip)
                    {
                        foreach (int[] k in copyCoordXY)
                        {
                            cellPlayer[k[0], k[1]].ship = true;
                            cellPlayer[k[0], k[1]].ships = ship;
                            cellPlayer[k[0], k[1]].view = "O";
                            BanForPlacement(k[0], k[1], ship, cellPlayer);// отмечаем клетки, где нельзя размещать другие корабли (слишком близко)
                        }
                        break;
                    }
                }
                catch
                {
                }

            }

        }

        static void destroyerEnemy(Cell[,] cellPlayer, string player, Ship[] shipArray) //Размещение двупалубных эсминцев игроком
        {
            int limit = 3; // количество лодок
            string name = "двупалубных эсминцев";

            for (int i = 0; i < limit; i++)
            {
                Ship ship = new Ship("Эсминец № " + i, "destroyer", 2, 2, 7);
                shipArray[4 + i] = ship;
                int[][] copyCoordXY = new int[ship.size][]; // временная переменная для храниния координат ячеек корабля
                arrangementEnemy(ship, i, limit, name, cellPlayer, player, shipArray);
            }

        }



            static void cruiser(Cell[,] cellPlayer, string player, Ship[] shipArray) //Размещение трехпалубных крейсеров игроком
        {
            System.Console.WriteLine();
            int limit = 2; // количество лодок
            string name = "трехпалубных крейсеров";

            for (int i = 0; i < limit; i++)
            {
                Ship ship = new Ship("Крейсер № " + i, "cruiser", 3, 3, 9);
                shipArray[7 + i] = ship;
                int[][] copyCoordXY = new int[ship.size][]; // временная переменная для храниния координат ячеек корабля
                arrangement(ship, i, limit, name, cellPlayer, player, shipArray);

            }

        }

        static void cruiserEnemy(Cell[,] cellPlayer, string player, Ship[] shipArray) //Размещение трехпалубных крейсеров игроком
        {
            int limit = 2; // количество лодок
            string name = "трехпалубных крейсеров";

            for (int i = 0; i < limit; i++)
            {
                Ship ship = new Ship("Крейсер № " + i, "cruiser", 3, 3, 9);
                shipArray[7 + i] = ship;
                int[][] copyCoordXY = new int[ship.size][]; // временная переменная для храниния координат ячеек корабля
                arrangementEnemy(ship, i, limit, name, cellPlayer, player, shipArray);
            }
        }

        static void battleship(Cell[,] cellPlayer, string player, Ship[] shipArray) //Размещение четырехпалубного линкора игроком
        {
            System.Console.WriteLine();
            int limit = 1; // количество лодок
            string name = "четырехпалубных линкоров";

            for (int i = 0; i < limit; i++)
            {
                Ship ship = new Ship("Линкор № " + i, "battleship", 4, 4, 12);
                shipArray[9] = ship;
                int[][] copyCoordXY = new int[ship.size][]; // временная переменная для храниния координат ячеек корабля
                arrangement(ship, i, limit, name, cellPlayer, player, shipArray);

            }

        }

        static void battleshipEnemy(Cell[,] cellPlayer, string player, Ship[] shipArray) //Размещение четырехпалубного линкора игроком
        {
            int limit = 1; // количество лодок
            string name = "четырехпалубных линкоров";

            for (int i = 0; i < limit; i++)
            {
                Ship ship = new Ship("Линкор № " + i, "battleship", 4, 4, 12);
                shipArray[9] = ship;
                int[][] copyCoordXY = new int[ship.size][]; // временная переменная для храниния координат ячеек корабля
                arrangementEnemy(ship, i, limit, name, cellPlayer, player, shipArray);
            }
        }

        static int Shift(Ship ship, int coord, int numsShift, string xOry) //Сдвиг, определения следующего блока
        {
            int coordConst = 0;

            if (ship.direction == "Up" && xOry == "Y") coordConst += 1;
            else if (ship.direction == "Down" && xOry == "Y") coordConst -= 1;
            else if (ship.direction == "Left" && xOry == "X") coordConst -= 1;
            else if (ship.direction == "Right" && xOry == "X") coordConst += 1;

            return coord + coordConst;

        }

        static void BanForPlacement(int coordX, int coordY, Ship ship, Cell[,] cell) //вблизи кораблей нельзя размещать другие корабли. Выявление клеток где нельзя размещать корабли.
        {
            int coordXconst = Shift(ship, 0, 1, "X"); //Сдвиu, определения следующего блока корабля по Х
            int coordYconst = Shift(ship, 0, 1, "Y");//Сдвиг, определения следующего блока корабля по Y

            for (int i = 0; i < ship.size; i++)
            {
                BanForOnePlacement(coordX + coordXconst, coordY + coordYconst, cell); 
            }

        }

        static void BanForOnePlacement(int coordX, int coordY, Cell[,] cell) //Вокруг блока очерчиваем зону, куда нельзя ставить корабли;
        {
            int coordx;
            int coordy;

            for (int i = 0; i < 8; i++)
            {
                switch (i)
                {
                    case 0:
                        coordx = coordX - 1;
                        coordy = coordY;
                        if (coordx < 0 || coordx > 9 || coordy < 0 || coordy > 9) continue;
                        cell[coordx, coordy].accommodation = false;
                        break;
                    case 1:
                        coordx = coordX - 1;
                        coordy = coordY + 1;
                        if (coordx < 0 || coordx > 9 || coordy < 0 || coordy > 9) continue;
                        cell[coordx, coordy].accommodation = false;
                        break;
                    case 2:
                        coordx = coordX;
                        coordy = coordY + 1;
                        if (coordx < 0 || coordx > 9 || coordy < 0 || coordy > 9) continue;
                        cell[coordx, coordy].accommodation = false;
                        break;
                    case 3:
                        coordx = coordX + 1;
                        coordy = coordY + 1;
                        if (coordx < 0 || coordx > 9 || coordy < 0 || coordy > 9) continue;
                        cell[coordx, coordy].accommodation = false;
                        break;
                    case 4:
                        coordx = coordX + 1;
                        coordy = coordY;
                        if (coordx < 0 || coordx > 9 || coordy < 0 || coordy > 9) continue;
                        cell[coordx, coordy].accommodation = false;
                        break;
                    case 5:
                        coordx = coordX + 1;
                        coordy = coordY - 1;
                        if (coordx < 0 || coordx > 9 || coordy < 0 || coordy > 9) continue;
                        cell[coordx, coordy].accommodation = false;
                        break;
                    case 6:
                        coordx = coordX;
                        coordy = coordY - 1;
                        if (coordx < 0 || coordx > 9 || coordy < 0 || coordy > 9) continue;
                        cell[coordx, coordy].accommodation = false;
                        break;
                    case 7:
                        coordx = coordX - 1;
                        coordy = coordY - 1;
                        if (coordx < 0 || coordx > 9 || coordy < 0 || coordy > 9) continue;
                        cell[coordx, coordy].accommodation = false;
                        break;
                    default:
                        Console.WriteLine("Какая-то ошибка");
                        break;
                }

            }



        }

        static int CoordinateInput(string comment) //Проверка на вводимые значения от 0 до 9
        {
            string nums;
            string[] array = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            System.Console.WriteLine(comment);
            while (true)
            {
                System.Console.WriteLine("Введи числовое значение от 0 до 9");
                nums = Console.ReadLine();
                if (array.Contains(nums)) break;
            }
            return int.Parse(nums);
        }

        static int directionInput(string comment) //Проверка на вводимые значения от 0 до 3
        {
            string nums;
            string[] array = { "0", "1", "2", "3"};
            System.Console.WriteLine(comment);
            while (true)
            {
                System.Console.WriteLine("Введи числовое значение от 0 до 3");
                nums = Console.ReadLine();
                if (array.Contains(nums)) break;
            }
            return int.Parse(nums);
        }
    }

    public class Cell // Класс ячейка на игровом поле
    {
        public string view;
        public int[] coordinates = new int[2];
        public bool hit;
        public bool ship;
        public bool accommodation;
        public Ship ships;


        public Cell(string v, int[] c, bool h, bool s, bool a) { view = v; coordinates = c; hit = h; ship = s; accommodation = a; }
    }

    public class Ship // Класс корабль
    {
        public string name;
        public string type;
        public int size;
        public int life;
        public int score;
        public string direction;

        public Ship(string n, string t, int s, int l, int sc) { name = n; type = t; size = s; life = l; score = sc; }
        public Ship(string n, string t, int s, int l, int sc, string d) { name = n; type = t; size = s; life = l; score = sc; direction = d; }
    }
}
