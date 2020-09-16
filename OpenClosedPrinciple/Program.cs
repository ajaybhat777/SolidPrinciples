using System;
using System.Collections.Generic;

namespace OpenClosedPrinciple
{
    public enum Color{
        Red,Green,Yellow,Blue
    }
    public enum Size{
        Small,Large,Huge
    }

    public class Product{
        public string Name;
        public Color Color;
        public Size Size;
        public Product(string name,Color color,Size size)
        {
            Name=name;
            Color=color;
            Size=size;
        }
    }

    public class ProductFilter{
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products,Size size){
            foreach(var p in products){
                if(p.Size==size)
                    yield return p;
            }
        }

        //violation of OpenClosed Principle
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products,Color color){
            foreach(var p in products){
                if(p.Color==color)
                    yield return p;
            }
        }

        //violation of OpenClosed Principle
        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products,Size size,Color color){
            foreach(var p in products){
                if(p.Size==size && p.Color==color)
                    yield return p;
            }
        }
    }

    //Achieveing OpenClosed Principle
    public interface ISpecification<T>{
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>{
        IEnumerable<T> Filter(IEnumerable<T> items,ISpecification<T> spec);
    }

    // you can define as many product specification 
    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;
        public ColorSpecification(Color color)
        {
            this.color=color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Color==color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;
        public SizeSpecification(Size size)
        {
            this.size=size;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Size==size;
        }
    }

    public class AndSpecification<T>: ISpecification<T>{
        public ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first,ISpecification<T> second)
        {
            this.first=first;
            this.second=second;
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach(var prod in items)
                if(spec.IsSatisfied(prod))
                    yield return prod;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OpenClosed Priniciple");

            var shorts=new Product("Shorts", Color.Red,Size.Small);
            var jeans=new Product("Jeans",Color.Blue,Size.Large);
            var pants=new Product("Pant",Color.Red,Size.Huge);

            Product[] products={shorts,jeans,pants};

            //Without OpenClosed(normal brute force)
            ProductFilter productFilter=new ProductFilter();
            Console.WriteLine("Red Products");
            foreach(var p in productFilter.FilterByColor(products,Color.Red)){
                Console.WriteLine($"- {p.Name} is red");
            }
            //With openclosed principle
           var betterFilter=new BetterFilter();
           Console.WriteLine("Red Products using new filter");
           foreach(var p in betterFilter.Filter(products, new ColorSpecification(Color.Red))){
                Console.WriteLine($"- {p.Name} is red");
           }

           foreach(var p in betterFilter.Filter(products,new AndSpecification<Product>(new ColorSpecification(Color.Red),new SizeSpecification(Size.Huge))))
           {
                    Console.WriteLine($"{p.Name} is a Red colored product which is huge in size");
           }
        }   
    }
}
