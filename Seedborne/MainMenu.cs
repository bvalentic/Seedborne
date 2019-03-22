using System;
using Seedborne.Objects;

namespace Seedborne
{
    public class MainMenu
    {
        public static void Main(string[] args)
        {
            ConsoleRetriever consoleRetriever = new ConsoleRetriever();
            bool playing = MenuOptions.Begin(consoleRetriever);

            while (playing)
            {
                Console.WriteLine("Great!");

                //actual game goes here
                bool generate = MenuOptions.KeepGoing("Generate world? ", consoleRetriever);
                if (generate)
                {
                    Console.Write("Input height: ");
                    int.TryParse(Console.ReadLine(), out var height);

                    Console.Write("Input length: ");
                    int.TryParse(Console.ReadLine(), out var length);

                    World world= new World(length, height);

                    world.FillWorld(consoleRetriever);
                }

                playing = MenuOptions.KeepGoing("Would you like to keep playing? ", consoleRetriever);
            }

            MenuOptions.Exit();

            
        }
    }
}
