using System.Data.SqlTypes;
using System.Drawing;

namespace LiskovSubstitution
{
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public Rectangle() 
        {
            
        }

        public Rectangle(int width, int height)
        {
            Width=width;
            Height=height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)} : {Width}, {nameof(Height)} : {Height}";
        }
    }
    public class Square : Rectangle
    {
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }
        public override int Height
        {
            set { base.Width = base.Height = value;}
        }
    }
    internal class Program
    {
        public static int Area(Rectangle r) => r.Width * r.Height;
        static void Main(string[] args)
        {
            Rectangle r = new Rectangle(10, 20);
            Console.WriteLine($"{r} has area {Area(r)}");

            Square sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has are {Area(sq)}");
        }
    }
}
