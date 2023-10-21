using System;
using System.Collections.Generic;
using System.Linq;
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
                newStackName = newStackName.Trim();
                if (Data.DoesStackExist(newStackName))
                {
                    Console.WriteLine("That stack already exists. Press Any Key to Continue.");
                    Console.ReadKey();
                }
                else
                {
                    newStack = new Stack(newStackName);
                    stackCreated = true;
                }
                Console.WriteLine($@"Stack {newStack.StackName} with ID {newStack.StackID} has been created");
                Console.ReadKey();


            }




        }
    }
}
