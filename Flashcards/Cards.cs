using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class Cards
    {
        internal int CardID { get; set; }
        internal int StackID { get; set; }
        internal int NoInStack { get; set; }
        internal string? Front { get; set; }
        internal string? Back { get; set; }
    }
}
