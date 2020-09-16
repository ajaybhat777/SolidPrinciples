using System;

namespace LiskovSubstitutionPrinciple
{
    public class Rectangle
    {
        // public int Width;
        // public int Height;
        //make the base class properties virtual
        public virtual int Width{get;set;}
        public virtual int Height{get;set;}

        public Rectangle()
        {
            
        }
        public Rectangle(int height,int width)
        {
            this.Height=height;
            this.Width=width;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width} {nameof(Height)}:{Height}";
        }

    }

    public class Square: Rectangle{
        // public new int Height{
        //     set{base.Width=base.Height=value;}
        // }
        // public new int Width{
        //     set{base.Height=base.Width=value;}
        // }
        //make the properties of subclass override
        public override int Height{
            set{base.Width=base.Height=value;}
        }
        public override int Width{
            set{base.Height=base.Width=value;}
        }
    }
    public class Program
    {
        static public int Area(Rectangle r)=> r.Width * r.Height;
        static void Main(string[] args)
        {
            Console.WriteLine("Liskov Substitution Principle");
            Rectangle rectangle=new Rectangle();
            Console.WriteLine($"{rectangle} has an area {Area(rectangle)} ");

            Square square=new Square();
            square.Width=4;
            Console.WriteLine($"{square} HashCode an area {Area(square)}");

            //fails when the properties are not virtual
            //Works well when the peoperties of base are made virtual and override in subclass
            //base class holds the reference of its subclass
            Rectangle sq=new Square();
            sq.Width=4;
            Console.WriteLine($"{sq} HashCode an area {Area(sq)}");

            //workings: First it looks at base class then it like ohhh , its virtual
                        //then it looks at setter of subclass then works as wow
        }
    }
}
