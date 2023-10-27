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
        internal static void DeleteCards()
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Card selectedCard = selectCard();

                if (selectedCard.CardID == 0)
                {
                    exitMenu = true;
                    break;
                }
                else
                {
                    Data.DeleteCard(selectedCard);
                    Card.reNumberCardsInStack(selectedCard.StackID);
                    Console.WriteLine($@"The Card {selectedCard.Front} Has been deleted. Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }
        internal static void ViewCards()
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Card selectedCard = selectCard();

                if (selectedCard.CardID == 0)
                {
                    exitMenu = true;
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Card Selected");

                    Console.WriteLine($@"Number in Stack: {selectedCard.NoInStack}");
                    Console.WriteLine($@"Front: {selectedCard.Front}");
                    Console.WriteLine($@"Back: {selectedCard.Back}");
                    Console.ReadKey();
                }
            }
        }
        internal static Card selectCard()
        {
            //this menu was hard so I've got more comments here for myself 
            Console.Clear();
            string pageText = "View Stacks";

            ConsoleKeyInfo key;
            int option = 1;
            bool isSelected = false;
            bool exitMenu = false;
            string color = $"{checkMark}{green}   ";


            List<Stack> stacks = Data.LoadStacks();

            Stack selectedStack = new Stack();//keeps track of the last selected stack
            Stack controllStack = new Stack();//keeps track of when the selected stack changes
            Stack lastStack = new Stack();// keeps track of the last stack printed on the screen
            Card selectedCard = new Card();

            bool loopAgain = false; //loop again to keep the page updated

            int numberOfItems = stacks.Count;//keep track of the number of options on the screen
            int index = 0;//index of each entu item
            int offset = 2;



            while (!exitMenu)
            {
                while (!isSelected)
                {
                    //resests at top of loop
                    loopAgain = false;
                    numberOfItems = stacks.Count;
                    index = 0;
                    //end of resets
                    Menu.OpenMenu(pageText);
                    index++;
                    Console.WriteLine("==================");
                    Console.WriteLine($@"|{(option == index ? color : "    ")}BACK     {resetColor}   |");
                    if (option == index)
                    {
                        selectedStack = new Stack();//clears out selected stack if this option is selected 
                        selectedCard = new Card();//reset Selected Card
                    }
                    Console.WriteLine("==================");

                    foreach (Stack stack in stacks)
                    {
                        controllStack = selectedStack;
                        index++;
                        Console.WriteLine($@" {(option == index ? color : "    ")}{stack.StackName}{resetColor}");
                        lastStack = stack;
                        if (option == index)
                        {
                            selectedStack = stack;
                            selectedCard = new Card();//reset Selected Card
                        }
                        //if you don't move down to a new stack act normally
                        if (selectedStack.StackID <= controllStack.StackID)
                        {

                            if (selectedStack == lastStack)
                            {
                                List<Card> cards = Data.LoadCards(selectedStack.StackID);
                                numberOfItems = numberOfItems + cards.Count;
                                offset = 2 + cards.Count;
                                foreach (Card card in cards)
                                {
                                    index++;
                                    Console.WriteLine($@"       {(option == index ? color : "    ")}{card.Front}{resetColor}");
                                    if (option == index)
                                    {
                                        selectedCard = card;
                                    }

                                }

                            }
                            else
                            {

                            }
                        }
                        //if you move down to a new stack I need to loop again and check the option to renumber the option
                        else
                        {
                            if (controllStack.StackID > 0)
                            {
                                List<Card> disapearingCards = Data.LoadCards(controllStack.StackID);
                                option = option - disapearingCards.Count;
                            }
                            loopAgain = true;
                        }
                    }


                    if (loopAgain == false)
                    {
                        key = Console.ReadKey(true);
                        controllStack = selectedStack;
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
                    else
                    {
                    }
                }
                if (option == 1)
                {
                    isSelected = true;
                    exitMenu = true;
                    break;
                }
                else
                {
                    if (selectedCard.CardID == 0)
                    {
                        isSelected = false;
                    }
                    else
                    {
                        isSelected = true;
                        exitMenu = true;
                    }
                }
            }
            return selectedCard;
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
