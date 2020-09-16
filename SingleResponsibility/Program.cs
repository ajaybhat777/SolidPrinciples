using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SingleResponsibility
{
    public class Remainder
    {
        private List<string> Remainders=new List<string>();
        
        public void AddRemainders(string remainder){
            Remainders.Add(remainder);
        }

        public void RemoveRemainder(int index){
            Remainders.RemoveAt(index);
        }

        public override string ToString()
        {
            return String.Join(Environment.NewLine,Remainders);
        }

        // The below commented functions must not define inside this class 
        // beacuse class must not have more responsibilities
        // single Responsibility Principle comes into picture

        // public void Save(string filename){
        // }

        // public static Remainder Load(string filename){
        // }
    }

    //class must have one single responsibility 
    public class Persistance{
        public void SaveToFile(Remainder remainder,string filename,bool overwrite=false){
            if(overwrite ||  File.Exists(filename))
                File.WriteAllText(filename,remainder.ToString());
        }
    }
    
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SingleRespnsibility Principle/Pattern");

            Remainder rem=new Remainder();
            rem.AddRemainders("Make Tea at 11");
            rem.AddRemainders("Meeting at 12");
            rem.AddRemainders("Lunch at 1");

            Console.WriteLine(rem);

            Persistance persistance= new Persistance();
            string filename=@"D:\remainders.txt";
            persistance.SaveToFile(rem,filename,true);

        }
    }
}
