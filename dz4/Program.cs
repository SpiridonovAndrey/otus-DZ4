using System.Drawing;
using System.Xml.Linq;

namespace dz4
{
    class Program
    {
        static void Main(string[] args)
        {
             Stack s = new Stack("a", "b", "c");
            // size = 3, Top = 'c'
            Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
            var deleted = s.Pop();
            // Извлек верхний элемент 'c' Size = 2
            Console.WriteLine($"Извлек верхний элемент '{deleted}' Size = {s.Size}");
            s.Add("d");
            // size = 3, Top = 'd'
            Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
            s.Pop();
            s.Pop();
            s.Pop();
            // size = 0, Top = null
            Console.WriteLine($"size = {s.Size}, Top = {(s.Top == null ? "null" : s.Top)}");
            s.Pop();

            var s1 = new Stack("a", "b", "c");
            s1.Merge(new Stack("1", "2", "3"));
            s1.Print();
            // в стеке s теперь элементы - "a", "b", "c", "3", "2", "1" <- верхний

            var s2 = Stack.Concat(new Stack("a", "b", "c"), new Stack("1", "2", "3"), new Stack("А", "Б", "В"));
            s2.Print();
            Console.WriteLine($"size = {s2.Size}, Top = '{s2.Top}'");
            // в стеке s теперь элементы - "c", "b", "a" "3", "2", "1", "В", "Б", "А" <- верхний
        }
        class StackExtensions
        {
            public void Merge(Stack s, Stack Elements)
            {
                while(s.Top!="null")
                {
                    Elements.Add(s.Top);
                    s.Pop();
                }
            }
        }

        class Stack: StackExtensions
        {

            public List<string> Elements { get; set; } = new List<string>();
            public int Size { get; set; }
            public string Top { get; set; }
            public Stack(params string[] NewElements)
            {
                foreach (string NewElement in NewElements)
                Elements.Add(NewElement);
                Top = Elements.Count() == 0 ? "null" : Elements.Last();
                Size = Elements.Count();
            }
            public void Add(string value)
            {
                Elements.Add(value);
                Top = Elements.Last();
                Size = Elements.Count();
            }
            public string Pop()
            {
                try
                {
                    string Element = Top;
                    Elements.RemoveAt(Elements.Count()-1);
                    Size = Elements.Count();
                    Top = Elements.Count() == 0 ? "null" : Elements.Last();
                    return Element;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Стек пустой");
                    return("");
                }
            }
            public void Merge(Stack s)
            {
                Merge(s, this);
            }
            internal static Stack Concat(params Stack[] NewElements)
            {
                Stack s = new Stack();
                foreach (Stack NewElement in NewElements)
                    s.Merge(NewElement);
                return s;
            }
            public void Print()
            {
                Console.Write("Stack: ");
                for (int i=0;i< Elements.Count(); i++)
                {
                    Console.Write(Elements.ElementAt(i));
                }
                Console.WriteLine();
            }

        }
    }
}