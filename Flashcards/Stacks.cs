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

        public Stack(string stackName)
        {
            StackName = stackName;
            StackID = Data.LoadStack(StackName);

        }
        
    }
}
