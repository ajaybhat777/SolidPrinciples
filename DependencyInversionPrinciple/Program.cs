using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInversionPrinciple
{
    public enum Relationship{
        Parent,Child
    }

    public class Person{
        public string Name;
    }

    //inversion of control
    public interface IRelationshipBrowser{
        IEnumerable<Person> FindAllChildOf(string name);
    }
    //low-level
    public class Relationships: IRelationshipBrowser
    {
        //Advantage:this can change the underlaying datastructure
        private List<(Person,Relationship,Person)> relations = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent,Person child){
            relations.Add((parent,Relationship.Parent,child));
            relations.Add((child, Relationship.Child,parent));
        }

        public IEnumerable<Person> FindAllChildOf(string name)
        {
            return from r in relations.Where(
                             x => x.Item1.Name == name &&
                                  x.Item2 == Relationship.Parent
                         )
                   select r.Item3;
        }

        //public List<(Person,Relationship,Person)>Relations => relations;
    }

    public class Program
    {
        //Directly dependent on the low-level module
        // public Program(Relationships relationships){
        //     var relations= relationships.Relations;
        //     foreach(var r in relations.Where(
        //         x => x.Item1.Name =="Chris" &&
        //              x.Item2 == Relationship.Parent
        //     )){
        //         Console.WriteLine($"Chris has a child called {r.Item3.Name}");
        //     }
        // }

        //This is now dependent of some kind of abstraction
        public Program(IRelationshipBrowser browser)
        {
            foreach(var p in browser.FindAllChildOf("Chris")){
                Console.WriteLine($"Chris has a child called {p.Name}");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var parent=new Person{ Name= "Chris"};
            var child1=new Person{ Name= "sona"};
            var child2=new Person{ Name= "tina"};

            var relationships=new Relationships();
            relationships.AddParentAndChild(parent,child1);
            relationships.AddParentAndChild(parent,child2);

            new Program(relationships);
        }
    }
}
