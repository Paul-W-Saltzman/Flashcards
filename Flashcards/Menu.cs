using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class Menu
    {
        

        internal static void MainMenu()
        {
            char upArrow = '\u2191';
            char downArrow = '\u2193';
            char checkMark = '\u2713';

            string black = "\u001b[30m";
            string red = "\u001b[31m";
            string green = "\u001b[32m";
            string yellow = "\u001b[33m";
            string blue = "\u001b[34m";
            string Magenta = "\u001b[35m";
            string Cyan = "\u001b[36m";
            string White = "\u001b[37m";
            string resetColor = "\u001b[0m";

            Console.WriteLine($@"Use {upArrow} and {downArrow} to navigate and press {green}Enter{resetColor} to select.");

            ConsoleKeyInfo key;
            int option = 1;
            bool isSelected = false;
            bool exitMenu = false;
            (int left, int top) = Console.GetCursorPosition();
            string color = $"{checkMark}{green}   ";

            Console.CursorVisible = false;
            while (!exitMenu)
            {
                while (!isSelected)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to Flash Cards your study program.");
                    Console.SetCursorPosition(left, top);
                    
                    Console.WriteLine($@"{(option == 1 ? color : "    ")}EXIT    {resetColor}");
                    Console.WriteLine($@"{(option == 2 ? color : "    ")}Manage Stacks{resetColor}");
                    Console.WriteLine($@"{(option == 3 ? color : "    ")}Manage FlashCards{resetColor}");
                    Console.WriteLine($@"{(option == 4 ? color : "    ")}Study {resetColor}");
                    Console.WriteLine($@"{(option == 5 ? color : "    ")}View Study Sessions {resetColor}");

                    key = Console.ReadKey(true);



                    switch (key.Key)

                    {
                        case ConsoleKey.DownArrow:
                            option = (option == 5 ? 1 : option + 1);
                            break;

                        case ConsoleKey.UpArrow:
                            option = (option == 1 ? 5 : option - 1);
                            break;

                        case ConsoleKey.Enter:
                            isSelected = true;
                            break;
                    }
                }

                switch (option)
                {
                    case 1:
                        exitMenu = true;
                        isSelected = true;
                        break;
                    case 2:
                        Console.WriteLine(option);
                        Console.ReadLine();
                        isSelected = false;
                        break;
                    case 3:
                        Console.WriteLine(option);
                        isSelected = false;
                        Console.ReadLine();
                        break;
                }

            }
            Console.Clear();
            Console.WriteLine($@"Goodbye");
            Console.ReadLine();

        }



    }
}
