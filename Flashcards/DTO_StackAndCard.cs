using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class DTO_StackAndCard
    {

        internal String StackName;
        internal int CardNumberInStack;
        internal String CardFront;
        internal String CardBack;

        public DTO_StackAndCard()
        {

        }


        public DTO_StackAndCard(Card card, Stack stack)
        {
            StackName = stack.StackName;
            CardNumberInStack = card.NoInStack;
            CardFront = card.Front;
            CardBack = card.Back;
        }
    }
}
