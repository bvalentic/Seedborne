using System;

namespace Seedborne
{
    public class MenuOptions
    {
        public static bool Begin(ConsoleRetriever consoleRetriever)
        {
            return KeepGoing("Welcome to Seedborne! \nWould you like to play? ", consoleRetriever);
        }

        public static bool KeepGoing(string inputQuestion, ConsoleRetriever consoleRetriever)
        {
            
            bool? playing = null;
            while (playing == null)
            {
                Console.Write(inputQuestion);
                string response = consoleRetriever.GetResponse();
                playing = CheckYesOrNo(response);
            }
            return (bool)playing;
        }

        public static bool? CheckYesOrNo(string response)
        {
            while (true)
            {
                if (response.Length > 0 && (response.ToLower() == "y" || response.ToLower() == "yes"))
                {
                    return true;
                }
                else if (response.Length > 0 && (response.ToLower() == "n" || response.ToLower() == "no"))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("I'm sorry, I didn't understand. Please answer \"yes\" or \"no\".");
                    return null;
                }
            }
        }

        public static void Exit()
        {
            Console.WriteLine("Goodbye!");
            Console.ReadKey();
        }
    }
}
