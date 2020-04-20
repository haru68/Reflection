using System;
using System.Reflection;
using System.Linq;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            var reflected = new TestClass(Environment.UserName);
            GetAllProperties(reflected);
            GetAllFields(reflected);
            GetAllMethods(reflected);
        }

        private static void GetAllProperties(object reflected)
        {
            Type objectType = reflected.GetType();

            PropertyInfo[] properties = objectType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            Console.WriteLine("Properties");
            foreach(var p in properties)
            {
                Console.WriteLine($"\t  {p.MemberType}, {p.PropertyType}, {p.Name}");
            }
        }

        private static void GetAllFields(object reflected)
        {
            Type objectType = reflected.GetType();

            FieldInfo[] fields = objectType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            Console.WriteLine("Attributes");
            foreach(var f in fields)
            {
                Console.WriteLine($"\t {f.MemberType}, {f.FieldType}, {f.Name}, IsPublic = {f.IsPublic}, IsPrivate = {f.IsPrivate}, Is static = {f.IsStatic}");
            }
        }

        private static void GetAllMethods(object reflected)
        {
            Type objectType = reflected.GetType();

            MethodInfo[] methods = objectType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            Console.WriteLine("Methods");
            foreach(var m in methods)
            {
                Console.WriteLine($"\t {m.MemberType}, {m.Name}, IsPublic = {m.IsPublic}, " +
                                    $"IsPrivate = {m.IsPrivate}, IsStatic = {m.IsStatic}");
            }
        }
    }

    public class TestClass
    {
        private int TestId { get; set; }
        public string Greeting { get => "Hello";}
        private string Author { get; set; }
        protected DateTime Date { get; set; }
        public string ThePublicProperty;
        private static string TheStaticProperty = "staticness";
        protected string TheProtectedProperty;

        public TestClass(string author)
        {
            Random rand = new Random();
            TestId = rand.Next(1, 100);
            Author = author;
            Date = DateTime.Today;
        }

        public void GetAuthorName()
        {
            Console.WriteLine("Author : " + Author);
        }

        private void SetId(int newId)
        {
            TestId = newId;
        }

        public void newAuthor(string name)
        {
            Author = name;
            SetId(TestId + 1);
            Date = DateTime.Now;
        }

        protected void Greet()
        {
            Console.WriteLine(Greeting + " " + Author);
        }

        public static void Yollo(string name)
        {
            Console.WriteLine("Coucou, " + name);
        }
    }
}
