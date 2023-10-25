using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Flashcards.GlobalVariables;

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

                        Console.WriteLine($@"Stack {green}{newStack.StackName}{resetColor} with ID {green}{newStack.StackID}{resetColor} has been created");
                        Console.ReadKey();
                    }
                }

            }

        }

        internal static void ViewStacks()
        {
            Console.Clear();
            string pageText = "View Stacks";

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
                    Menu.OpenMenu(pageText);

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
                            option = (option == (numberOfItems + 1) ? 1 : option + 1);
                            break;
                        case ConsoleKey.UpArrow:
                            option = (option == 1 ? (numberOfItems + 1) : option - 1);
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
                    Console.WriteLine($"Item at index {option - 2}: {selectedStack.StackID} {selectedStack.StackName}");
                    Console.ReadKey();
                    isSelected = false;
                }
            }
        }

        internal static void ViewCards()
        {
            Console.Clear();
            string pageText = "View Stacks";

            ConsoleKeyInfo key;
            int option = 1;
            bool exitMenu = false;
            bool viewCards = false;
            bool isSelected = false;
            string color = $"{checkMark}{green}   ";


            List<Stack> stacks = Data.LoadStacks();
            Stack selectedStack = new Stack();
            int numberOfItems = stacks.Count;
            int index = 1;


            while (!exitMenu)
            {
                while (!isSelected)
                {
                    Menu.OpenMenu(pageText);

                    Console.WriteLine("==================");
                    Console.WriteLine($@"|{(option == index ? color : "    ")}BACK     {resetColor}   |");
                    Console.WriteLine("==================");
                    index++;


                    foreach (Stack stack in stacks)
                    {

                        Console.WriteLine($@" {(option == index ? color : "    ")}{stack.StackName}{resetColor}");
                        index++;

                        if (viewCards && selectedStack.StackID == stack.StackID)
                        {
                            List<Card> cards = Data.LoadCards(stack.StackID);
                            numberOfItems = numberOfItems + cards.Count;
                            foreach (Card card in cards)
                            {
                                Console.WriteLine($@"       {(option == index ? color : "    ")}{card.Front}{resetColor}");
                                index++;
                            }
                        }
                    }

                    index = 1;//reset index
                    key = Console.ReadKey(true);


                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:
                            option = (option == (numberOfItems + 1) ? 1 : option + 1);
                            break;
                        case ConsoleKey.UpArrow:
                            option = (option == 1 ? (numberOfItems + 1) : option - 1);
                            break;
                        case ConsoleKey.Enter:
                            isSelected = true;
                            break;
                    }
                index = 1;
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
                    selectedStack = stacks[option - 2];
                    Console.WriteLine($"Item at index {option - 2}: {selectedStack.StackID} {selectedStack.StackName}");
                    viewCards = true;
                    Console.ReadKey();
                    isSelected = false;
                }
            }
        }


        internal static void DeleteStacks()
        {
            Console.Clear();
            string pageText = "Delete Stacks";
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
                    stacks = Data.LoadStacks();
                    Menu.OpenMenu(pageText);

                    Console.WriteLine("==================");
                    Console.WriteLine($@"|{(option == index ? color : "    ")}BACK     {resetColor}   |");
                    Console.WriteLine("==================");
                    index++;


                    foreach (Stack stack in stacks)
                    {

                        Console.WriteLine($@" {(option == index ? color : "    ")}{stack.StackName}{resetColor}");
                        index++;

                            index = 1;//reset index
                        key = Console.ReadKey(true);


                        switch (key.Key)
                        {
                            case ConsoleKey.DownArrow:
                                option = (option == (numberOfItems + 1) ? 1 : option + 1);
                                break;
                            case ConsoleKey.UpArrow:
                                option = (option == 1 ? (numberOfItems + 1) : option - 1);
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
                        Stack selectedStack = stacks[option - 2];
                        DeleteStackConfirm(selectedStack);
                        isSelected = false;
                    }
                }
            }

        }
        

        internal static void DeleteStackConfirm(Stack stackToDel)
        {
            Console.Clear();

            ConsoleKeyInfo key;
            int option = 1;
            bool exitMenu = false;
            bool isSelected = false;
            string color = $"{checkMark}{green}   ";
            string color2 = $"{checkMark}{red}   ";

            int index = 1;


            while (!exitMenu)
            {
                while (!isSelected)
                {
                    Console.Clear();

                    (int left, int top) = Console.GetCursorPosition();
                    Console.Clear();
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine($@"Do you want to {red}delete{resetColor} the {red}{stackToDel.StackName}{resetColor} Stack");
                    Console.WriteLine($@"This will delete any associated flashcards as well as any related recorded study sessions.");
                    Console.WriteLine();
                    Console.WriteLine($@"{(option == 1 ? color : "    ")}Back     {resetColor}");
                    Console.WriteLine($@"{(option == 2 ? color2 : "    ")}Delete   {resetColor}");

                    key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:
                            option = (option == 2 ? 1 : option + 1);
                            break;
                        case ConsoleKey.UpArrow:
                            option = (option == 1 ? 2 : option - 1);
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
                        break;
                    case 2:
                        Data.DelStack(stackToDel);
                        Console.WriteLine();
                        Console.WriteLine($@"Stack {red}{stackToDel.StackName}{resetColor} has been deleted. Press any key to continue.");
                        Console.ReadKey();
                        exitMenu = true;
                        break;
                }
            }

        }


        internal static Stack ChooseStack()
        {
            Stack selectedStack = new Stack();
            Console.Clear();
            string pageText =$@"Please select which stack you would like to {green}Add{resetColor} a {green}Flash Card{resetColor} to.";
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
                    stacks = Data.LoadStacks();
                    Menu.OpenMenu(pageText);

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
                            option = (option == (numberOfItems + 1) ? 1 : option + 1);
                            break;
                        case ConsoleKey.UpArrow:
                            option = (option == 1 ? (numberOfItems + 1) : option - 1);
                            break;
                        case ConsoleKey.Enter:
                            isSelected = true;
                            break;
                    }
                }

                if (option == 1)
                {
                    exitMenu = true;
                    break;
                }
                else
                {
                    selectedStack = stacks[option - 2];
                    exitMenu = true;
                    break;
                }
            }
            return selectedStack;
        }

        internal static void AddCard()
        {
            Stack selectedStack = ChooseStack();
            Console.WriteLine($@"You Choose Stack {selectedStack.StackName}");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($@"Please enter the Front of the FlashCard.");
            string front = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($@"Please enter the Back of the FlashCard.");
            string back = Console.ReadLine();
            front = Helpers.Sanitize(front);
            back = Helpers.Sanitize(back);
            Card newCard = new Card(selectedStack.StackID,front,back);
        }
    }
}
