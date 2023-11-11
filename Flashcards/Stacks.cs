using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class Stack
    {
        internal int StackID {get;set;}
        internal string StackName { get;set;}

        internal Stack() {}

        internal Stack(string stackName)
        {
            StackName = stackName;
            StackID = Data.EnterStack(StackName);
        }

        internal static void LoadSeedDataStacks()
        {
            Stack variableTypes = new Stack("Variable Types");
            Stack selectorCodes = new Stack("Selector Codes");
            Stack returnCodes = new Stack("Return Codes");
            Stack programingTermss = new Stack("Programing Terms");
            Stack french = new Stack("french");
            Stack vietnamese = new Stack("Vietnamese");
            Stack Spanish = new Stack("Spanish");

        }


    }
}
