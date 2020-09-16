using System;

namespace InterfaceSegregationPrinciple
{
    public class Document{

    }

    public interface IMachine{
        void Print(Document document);
        void Scan(Document document);
        void Email(Document document);
    }

    public class MultiFunctionMachine : IMachine
    {
        public void Email(Document document)
        {
            throw new NotImplementedException();
        }

        public void Print(Document document)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document document)
        {
            throw new NotImplementedException();
        }
    }

    //violation of InterfaceSeggragation beacuse the old machines cant do multiple things at a time
    //the below fails
    public class OldFashionedMachine : IMachine
    {
        public void Email(Document document)
        {
            throw new NotImplementedException();
        }

        public void Print(Document document)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document document)
        {
            throw new NotImplementedException();
        }
    }

    //Seggregate th operations into smaller interfaces
    public interface IPrinter{
        void Print(Document document);
    }
    public interface IScanner{
        void Scan(Document document);
    }

    //implement multiple/required functions 
    public class PhotoMachine : IScanner, IPrinter
    {
        public void Print(Document document)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document document)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMultiFunctionDevice: IScanner,IPrinter //
    {
        
    }

    public class MultiFunctionDevice:IMultiFunctionDevice
    {
        private IScanner scanner;
        private IPrinter printer;

        public MultiFunctionDevice(IScanner scanner,IPrinter printer)
        {
            this.scanner=scanner;
            this.printer=printer;
        }

        //delegate the functions of scanner and printer through IMultiFunctionDevice interface
        public void Print(Document document){
            printer.Print(document);
        }
        public void Scan(Document document){
            scanner.Scan(document);
        }//decorator pattern
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
