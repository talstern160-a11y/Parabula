using System;

namespace ParabolaProject
{
    // א. מחלקת Point
    public class Point
    {
        private double x;
        private double y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double GetX()
        {
            return this.x;
        }

        public double GetY()
        {
            return this.y;
        }

        public void SetX(double x)
        {
            this.x = x;
        }

        public void SetY(double y)
        {
            this.y = y;
        }

        public override string ToString()
        {
            return "(" + this.x + ", " + this.y + ")";
        }
    }

    // ב, ד, ו, ח. מחלקת Parabula
    public class Parabula
    {
        private double a;
        private double b;
        private double c;
        private static int count = 0; // מונה סטטי לספירת כמות האובייקטים שנוצרו

        // ב. בנאי
        public Parabula(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            count++;
        }

        // החזרת מספר הפרבולות שנוצרו
        public static int GetCount()
        {
            return count;
        }

        // חישוב ערך ה-y עבור x נתון
        public double CalculateY(double x)
        {
            return this.a * x * x + this.b * x + this.c;
        }

        // החזרת שורשי הפרבולה במערך
        public double[] GetRoots()
        {
            double delta = this.b * this.b - 4 * this.a * this.c;

            if (delta < 0)
            {
                return new double[0];
            }
            else if (delta == 0)
            {
                double root = -this.b / (2 * this.a);
                return new double[] { root };
            }
            else
            {
                double root1 = (-this.b + Math.Sqrt(delta)) / (2 * this.a);
                double root2 = (-this.b - Math.Sqrt(delta)) / (2 * this.a);
                return new double[] { root1, root2 };
            }
        }

        public override string ToString()
        {
            return "y = " + this.a + "x^2 + " + this.b + "x + " + this.c;
        }

        // ד. בדיקה האם נקודה נמצאת על הפרבולה
        public bool IsOnTheGraph(Point p)
        {
            return CalculateY(p.GetX()) == p.GetY();
        }

        // ו. החזרת נקודת המינימום או המקסימום (קודקוד הפרבולה)
        public Point GetMinMax()
        {
            double x = -this.b / (2 * this.a);
            double y = CalculateY(x);
            return new Point(x, y);
        }

        // ח. החזרת נקודות החיתוך בין שתי פרבולות
        public Point[] GetCuttingPoint(Parabula other)
        {
            double A = this.a - other.a;
            double B = this.b - other.b;
            double C = this.c - other.c;

            if (A == 0)
            {
                if (B == 0)
                {
                    return new Point[0];
                }
                double x = -C / B;
                double y = CalculateY(x);
                return new Point[] { new Point(x, y) };
            }

            double delta = B * B - 4 * A * C;

            if (delta < 0)
            {
                return new Point[0];
            }
            else if (delta == 0)
            {
                double x = -B / (2 * A);
                double y = CalculateY(x);
                return new Point[] { new Point(x, y) };
            }
            else
            {
                double x1 = (-B + Math.Sqrt(delta)) / (2 * A);
                double y1 = CalculateY(x1);

                double x2 = (-B - Math.Sqrt(delta)) / (2 * A);
                double y2 = CalculateY(x2);

                return new Point[] { new Point(x1, y1), new Point(x2, y2) };
            }
        }
    }

    // ג, ה, ז, ט. מחלקת הבדיקה
    public class TestParabula
    {
        // ה. פעולה חיצונית לבדיקה אם נקודה נמצאת על הגרף
        public static bool IsOnTheGraph(Parabula pr, Point p)
        {
            return pr.IsOnTheGraph(p);
        }

        // ז. פעולה חיצונית להחזרת נקודת המינימום/מקסימום
        public static Point GetMinMax(Parabula pr)
        {
            return pr.GetMinMax();
        }

        // ג + ט. תוכנית ראשית
        public static void Main(string[] args)
        {
            // קליטת פרבולה ראשונה
            Console.WriteLine("הכנס מקדמים לפרבולה הראשונה (a, b, c):");
            double a1 = double.Parse(Console.ReadLine());
            double b1 = double.Parse(Console.ReadLine());
            double c1 = double.Parse(Console.ReadLine());
            Parabula p1 = new Parabula(a1, b1, c1);

            // קליטת פרבולה שנייה
            Console.WriteLine("הכנס מקדמים לפרבולה השנייה (a, b, c):");
            double a2 = double.Parse(Console.ReadLine());
            double b2 = double.Parse(Console.ReadLine());
            double c2 = double.Parse(Console.ReadLine());
            Parabula p2 = new Parabula(a2, b2, c2);

            // חקירת פרבולה 1
            Console.WriteLine("\n--- חקירת פרבולה 1 ---");
            Console.WriteLine(p1.ToString());
            Point minMax1 = GetMinMax(p1);
            Console.WriteLine("נקודת קיצון: " + minMax1.ToString());
            double[] roots1 = p1.GetRoots();
            Console.WriteLine("מספר שורשים: " + roots1.Length);
            for (int i = 0; i < roots1.Length; i++)
            {
                Console.WriteLine("שורש " + (i + 1) + ": " + roots1[i]);
            }

            // חקירת פרבולה 2
            Console.WriteLine("\n--- חקירת פרבולה 2 ---");
            Console.WriteLine(p2.ToString());
            Point minMax2 = GetMinMax(p2);
            Console.WriteLine("נקודת קיצון: " + minMax2.ToString());
            double[] roots2 = p2.GetRoots();
            Console.WriteLine("מספר שורשים: " + roots2.Length);
            for (int i = 0; i < roots2.Length; i++)
            {
                Console.WriteLine("שורש " + (i + 1) + ": " + roots2[i]);
            }

            // נקודות חיתוך בין הפרבולות
            Console.WriteLine("\n--- נקודות חיתוך בין הפרבולות ---");
            Point[] intersections = p1.GetCuttingPoint(p2);
            Console.WriteLine("מספר נקודות חיתוך: " + intersections.Length);
            for (int i = 0; i < intersections.Length; i++)
            {
                Console.WriteLine("נקודה " + (i + 1) + ": " + intersections[i].ToString());
            }

            // כמות הפרבולות שנוצרו
            Console.WriteLine("\nסה\"כ פרבולות שנוצרו במהלך התוכנית: " + Parabula.GetCount());
        }
    }
}