namespace InterfaceSegregation
{
    public class Document
    {

    }
    public interface IScanner
    {
        void Scan(Document d);
    }

    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IMultiFunctionalMachine : IPrinter, IScanner
    {
        //
    }
    public class Scanner : IScanner
    {
        public void Scan(Document d)
        {
            //
        }
    }

    public class PhotoCopier : IScanner, IPrinter
    {
        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }

    public class MultiFunctionalMachine : IMultiFunctionalMachine
    {
        public void Print(Document d)
        {
            //throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            //throw new NotImplementedException();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
