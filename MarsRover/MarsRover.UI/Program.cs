using MarsRover.UI.Enums;
using MarsRover.UI.Model;
using System;
using System.Collections.Generic;

namespace MarsRover.UI
{
    static class Program
    {
        static void Main(string[] args)
        {
            var roverList = new List<Rover>();

            Console.WriteLine("Mars düzleminin gönderilecek Rover sayısını giriniz :");
            var roverNumber = Convert.ToInt32(Console.ReadLine());

            var mars = SetMarsMaxCoordinate(roverNumber);

            for (int i = 1; i <= roverNumber; i++)
            {
                var rover = SetRoverCoordinate(i, mars);
                Calculate(rover, mars);
                roverList.Add(rover);
            }

            foreach (var item in roverList)
            {
                Console.WriteLine($"{item.X} {item.Y} {item.Direction.ToString()}");
            }

            Console.ReadKey();
        }

        private static void Calculate(Rover rover, Mars mars)
        {
            foreach (var move in rover.RouteMap)
            {
                if (move.ToString().EnumChecker<MoveEnum>())
                {
                    MoveEnum moveEnum;
                    Enum.TryParse(move.ToString().ToUpper(), out moveEnum);

                    switch (moveEnum)
                    {
                        case MoveEnum.M:
                            Move(rover, mars);
                            break;
                        case MoveEnum.R:
                            TurnRight(rover);
                            break;
                        case MoveEnum.L:
                            TurnLeft(rover);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static Rover SetRoverCoordinate(int roverNumber, Mars mars)
        {
            Console.WriteLine($"{roverNumber}. Rover kordinatlarını giriniz :");
            var roverCordinate = Console.ReadLine();

            var roverCordinateArray = roverCordinate.Split(" ");

            if (roverCordinateArray.Length != 3 || !roverCordinateArray[0].IsPositiveNumber() || !roverCordinateArray[1].IsPositiveNumber() || !roverCordinateArray[2].EnumChecker<DirectionEnum>())
            {
                Console.WriteLine("Değerleri yanlış girdiniz lütfen tekrar giriniz. Örnek : 1 2 N");
                return SetRoverCoordinate(roverNumber, mars);
            }

            var roverX = int.Parse(roverCordinateArray[0]);
            var roverY = int.Parse(roverCordinateArray[1]);
            var roverDirection = Enum.Parse<DirectionEnum>(roverCordinateArray[2].ToUpper());

            if (roverX > mars.X)
                roverX = mars.X;
            if (roverY > mars.Y)
                roverY = mars.Y;

            Console.WriteLine($"{roverNumber}. Rover haraket rotasını giriniz :");
            var routeMap = Console.ReadLine();

            return new Rover(roverX, roverY, roverDirection, mars, routeMap);
        }

        public static bool EnumChecker<T>(this string enumValue)
        {
            var enums = typeof(T).GetEnumValues();

            foreach (var item in enums)
            {
                if (IsNumber(item.ToString())) return false;
                else if (item.ToString() == enumValue.ToUpper()) return true;
            }

            return false;
        }

        public static bool IsNumber(this string value)
        {
            try
            {
                Convert.ToInt32(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static Mars SetMarsMaxCoordinate(int roverNumber)
        {
            Console.WriteLine("Mars düzleminin X ve Y koordinatlarını giriniz :");
            var marsMaxCoordinate = Console.ReadLine();

            var marsCoordinateArray = marsMaxCoordinate.Split(" ");

            if (marsCoordinateArray.Length != 2 || !IsPositiveNumber(marsCoordinateArray[0]) || !IsPositiveNumber(marsCoordinateArray[1]))
            {
                Console.WriteLine("Değerleri yanlış girdiniz lütfen tekrar giriniz. Örn : 5 5");
                return SetMarsMaxCoordinate(roverNumber);
            }

            return new Mars(Convert.ToInt32(marsCoordinateArray[0]), Convert.ToInt32(marsCoordinateArray[1]));
        }

        public static bool IsPositiveNumber(this string value)
        {
            try
            {
                return Convert.ToInt32(value) > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public static void Move(Rover rover, Mars mars)
        {
            switch (rover.Direction)
            {
                case DirectionEnum.N:
                    if (rover.Y + 1 <= mars.Y)
                        rover.Y++;
                    break;
                case DirectionEnum.E:
                    if (rover.X + 1 <= mars.X)
                        rover.X++;
                    break;
                case DirectionEnum.S:
                    if (rover.Y - 1 >= 0)
                        rover.Y--;
                    break;
                case DirectionEnum.W:
                    if (rover.X - 1 >= 0)
                        rover.X--;
                    break;
            }
        }

        public static void TurnLeft(Rover rover)
        {
            switch (rover.Direction)
            {
                case DirectionEnum.N:
                    rover.Direction = DirectionEnum.W;
                    break;
                case DirectionEnum.W:
                    rover.Direction = DirectionEnum.S;
                    break;
                case DirectionEnum.S:
                    rover.Direction = DirectionEnum.E;
                    break;
                case DirectionEnum.E:
                    rover.Direction = DirectionEnum.N;
                    break;
                default:
                    break;
            }
        }

        public static void TurnRight(Rover rover)
        {
            switch (rover.Direction)
            {
                case DirectionEnum.N:
                    rover.Direction = DirectionEnum.E;
                    break;
                case DirectionEnum.E:
                    rover.Direction = DirectionEnum.S;
                    break;
                case DirectionEnum.S:
                    rover.Direction = DirectionEnum.W;
                    break;
                case DirectionEnum.W:
                    rover.Direction = DirectionEnum.N;
                    break;
                default:
                    break;
            }
        }
    }
}
