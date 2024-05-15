namespace DependencyInversion
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling,
    }

    public class Person
    {
        public string Name;
    }
    // abstraction
    public interface IRelationshipBrowser
    {
        public IEnumerable<Person> FindAllChildOf(string name);
    }
    //low level module
    public class Relationships : IRelationshipBrowser
    {
        private List<(Person,Relationship,Person)> relations = new List<(Person,Relationship,Person)> ();

        public void AddParentAndChild(Person parent, Person Child)
        {
            relations.Add((parent,Relationship.Parent, Child));
            relations.Add((Child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildOf(string name)
        {
            foreach (var rel in relations.Where(x => x.Item1.Name.Equals(name) && x.Item2.Equals(Relationship.Parent)))
            {
                yield return rel.Item3;
            }
        }

       // public List<(Person, Relationship, Person)> Relations => relations;
    }

    // high level module
    internal class Program
    {
        public Program(IRelationshipBrowser finder)
        {
            foreach(var i in finder.FindAllChildOf("John"))
            {
                Console.WriteLine($"John has a child called {i.Name}");
            }
        }

        //public Program(Relationships relationships)
        //{
        //    var relations = relationships.Relations;
        //    foreach(var rel in relations.Where(x => x.Item1.Name.Equals("John") && x.Item2.Equals(Relationship.Parent)))
        //    {
        //        Console.WriteLine($"John has a child called {rel.Item3.Name}");
        //    }
        //}

        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);
            new Program(relationships);
        }
    }
}
