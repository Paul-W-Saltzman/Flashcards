using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class Card
    {
        internal int CardID { get; set; }
        internal int StackID { get; set; }
        internal int NoInStack { get; set; }
        internal string? Front { get; set; }
        internal string? Back { get; set; }

        internal Card() { }

        internal Card(int stackID, string front, string back)
        {
            Card creatingCard = new Card();
            creatingCard.StackID = stackID;
            creatingCard.NoInStack = Card.NextNoInStack(creatingCard.StackID);
            creatingCard.Front = front;
            creatingCard.Back = back;
            creatingCard.CardID = Data.EnterCard(creatingCard.StackID, creatingCard.NoInStack, creatingCard.Front, creatingCard.Back);
        }

        internal static int NextNoInStack(int stackID)
        {
            int noInStack = 0;
            List<Card> cards = Data.LoadCards(stackID);
            if(cards.Count == 0) 
            {
                noInStack = 1;
            }
            else if(cards.Count > 0) 
            {
                noInStack = cards.Count + 1;            
            }
            return noInStack;
        }

    }
}
