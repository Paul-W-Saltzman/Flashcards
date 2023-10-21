using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class ActionPages
    {

        internal static void AddStack()
        {
            bool stackCreated = false;
            Stack newStack = new Stack();

            while (!stackCreated)
            {
                Console.WriteLine("Please enter the name of the stack you wish to add");
                string newStackName = Console.ReadLine();
                Helpers.Sanitize(newStackName);
                if (newStackName.Length == 0)
                {
                    Console.WriteLine("Invalid Entry. Press Any Key to Continue.");
                    Console.ReadKey();
                }
                else
                {
                    if (Data.DoesStackExist(newStackName))
                    {
                        Console.WriteLine("That stack already exists. Press Any Key to Continue.");
                        Console.ReadKey();
                    }
                    else
                    {
                        newStack = new Stack(newStackName);
                        stackCreated = true;

                        Console.WriteLine($@"Stack {newStack.StackName} with ID {newStack.StackID} has been created");
                        Console.ReadKey();
                    }
                }
                
            }

        }

        internal static void ViewStacks()
        {
            Console.Clear();
            char upArrow = '\u2191';
            char downArrow = '\u2193';
            char checkMark = '\u2713';

            string green = "\u001b[32m";
            string resetColor = "\u001b[0m";

            ConsoleKeyInfo key;
            int option = 1;
            bool exitMenu = false;
            bool isSelected = false;
            string color = $"{checkMark}{green}   ";


            List<Stack> stacks = Data.LoadStacks();
            int numberOfItems = stacks.Count;
            int index = 1;


            while (!exitMenu)
            {
                while (!isSelected)
                {
                    Menu.OpenMenu();

                    Console.WriteLine("==================");
                    Console.WriteLine($@"|{(option == index ? color : "    ")}BACK     {resetColor}   |");
                    Console.WriteLine("==================");
                    index++;

                    foreach (Stack stack in stacks)
                    {
                        
                        Console.WriteLine($@" {(option == index ? color : "    ")}{stack.StackName}{resetColor}");
                        index++;
                    }
                    index = 1;//reset index
                    key = Console.ReadKey(true);


                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:
                            option = (option == (numberOfItems+1) ? 1 : option + 1);
                            break;
                        case ConsoleKey.UpArrow:
                            option = (option == 1 ? (numberOfItems+1) : option - 1);
                            break;
                        case ConsoleKey.Enter:
                            isSelected = true;
                            break;
                    }
                }

                if (option == 1)
                {
                    exitMenu = true;
                    isSelected = true;
                    break;
                }
                else
                {
                    Console.WriteLine($@"Option: {option}");
                    Stack selectedStack = stacks[option - 2];
                    Console.WriteLine($"Item at index {option -2}: {selectedStack.StackID} {selectedStack.StackName}");
                    Console.ReadKey();
                    isSelected = false;
                }

            }






        }
    }
}
