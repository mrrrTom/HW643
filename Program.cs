// Задача 43: Напишите программу, которая найдёт точку пересечения двух прямых, заданных уравнениями y = k1 * x + b1, y = k2 * x + b2; значения b1, k1, b2 и k2 задаются пользователем.
// b1 = 2, k1 = 5, b2 = 4, k2 = 9 -> (-0,5; -0,5)

namespace HW6
{
    public class ConsoleApp
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the intersect point finder for lines y = k1 * x + b1, y = k2 * x + b2!");
            Console.WriteLine("Insert coefficients for line equations: (ex: b1, k1, b2, k2)");
            var hasErrors = false;
            var coefficients = default(int[]);
            var input = Console.ReadLine();
            if (!(hasErrors = input == null))
            {
                coefficients = input.Split(',').Select(x => TryParse(x, ref hasErrors)).ToArray();
            }

            if (hasErrors || coefficients.Length != 4) 
            {
                Console.WriteLine("Sorry, program could not handle inserted value... Bye!");
            }

            Console.WriteLine(FindIntersection(coefficients[0], coefficients[1], coefficients[2], coefficients[3]));
        }

        static int TryParse(string input, ref bool hasErrors)
        {
            if (hasErrors) return 0;
            if (int.TryParse(input.Trim(), out var result))
            {
                return result;
            }

            hasErrors = true;
            return 0;
        }

        static LinesIntersection FindIntersection(int b1, int k1, int b2, int k2)
        {
            var devider = k2 - k1;
            var devidend = b1 - b2;
            if (devider == 0)
            {
                if (devidend == 0) return new LinesIntersection(null, LinesIntersection.IntersectionType.Line);
                return new LinesIntersection(null, LinesIntersection.IntersectionType.None);
            }

            var x = (double)devidend / (double)devider;
            var y = (double)k1*x + (double)b1;
            return new LinesIntersection(new Point(x, y), LinesIntersection.IntersectionType.Point);
        }
    }

    internal class LinesIntersection
    {
        internal enum IntersectionType
        {
            Undefined,
            Point,
            Line,
            None
        }


        private IntersectionType _type;
        private Point _p;

        public LinesIntersection (Point p, IntersectionType type)
        {
            if (type == IntersectionType.Point && p == null)
            {
                throw new ArgumentNullException("point");
            }
            
            _type = type;
            _p = p;
        }

        public override string ToString()
        {
            switch(_type)
            {
                case IntersectionType.Point:
                return _p.ToString();
                case IntersectionType.Line:
                var inf = (char)8734;
                return "(-" + inf + "; +" + inf + ")";
                case IntersectionType.None:
                return $"{(char)216}";
            }

            return string.Empty;
        }
    }

    internal class Point
    {
        private double _x;
        private double _y;

        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            return $"({_x}, {_y})";
        }
    }
}