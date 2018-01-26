using System;

namespace MiwDrzewo
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new Node
            {
                Content = "Yolo",
                Question = "Czy noob"
            };

            var czyNoobY = new Node
            {
                Parent = root,
                Content = "Jestes noob"
            };
            
            var czyNoobN = new Node
            {
                Parent = root,
                Content = "Nie jestes noob"
            };

            root.Yes = czyNoobY;
            root.No = czyNoobN;

            Console.WriteLine(Node.GetCLIPSCode());
        }
    }
}