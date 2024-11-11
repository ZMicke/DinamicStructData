using System;

namespace DinamicStructConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stack<string> stack = new Stack<string>();
            string input = File.ReadAllText("input.txt");
            string[] commands = input.Split(' ');

            foreach (string command in commands)
            {
                if (command.StartsWith("1,"))
                {
                    string value = command.Substring(2);
                    stack.Push(value);
                }
                else if (command == "2")
                {
                    stack.Pop();
                }
                else if (command == "3")
                {
                    stack.Top();
                }
                else if (command == "4")
                {
                    stack.IsEmpty();
                }
                else if (command == "5")
                {
                    stack.Print();
                }
                else
                {
                    Console.WriteLine($"Unknown command: {command}");
                }
            }
        }
    }
}
