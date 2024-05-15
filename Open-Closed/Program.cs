namespace Open_Closed
{
    // Here we can include new filter parameters by we
    public enum Color
    {
        Red, Green, Blue
    }
    public enum Size
    {
        Small, Medium, Large, Huge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            Name=name;
            Color=color;
            Size=size;
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(Product p);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color=color;
        }

        public bool IsSatisfied(Product p)
        {
            return p.Color.Equals(color);
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size=size;
        }

        public bool IsSatisfied(Product p)
        {
            return p.Size.Equals(size);
        }
    }
    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first=first;
            this.second=second;
        }

        public bool IsSatisfied(Product p)
        {
            return first.IsSatisfied(p) && second.IsSatisfied(p);
        }
    }
    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);
            Product[] products = { apple, tree, house };

            var bf = new BetterFilter();
            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
                Console.WriteLine($"- {p.Name} is green");

            var andSpec = new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large));
            foreach (var p in bf.Filter(products, andSpec))
            {
                Console.WriteLine($" - {p.Name} is big and blue");
            }
        }
    }
}
