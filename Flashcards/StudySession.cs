using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class StudySession
    {
        internal int StudySessionID { get; set; }
        internal int StackID { get; set; }
        internal DateOnly Date { get; set; } 
        internal string StackName { get; set; }
        internal int Correct { get; set; }
        internal int Total { get; set; }
        internal double Score { get; set; }

        private StudySession(int stackID, DateOnly date, string stackName, int correct, int total )
        { 
            this.StackID = stackID;
            this.Date = date;
            this.StackName = stackName;
            this.Correct = correct;
            this.Total = total;
            this.Score = correct/total;
            this.StudySessionID = Data.EnterStudySession(this);
        }


        public static void Study(List<DTO_StackAndCard> study)
        {
            DTO_StackAndCard stack = study[1];
            int stackID = stack.StackID;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            string stackName = stack.StackName;
            int correct = 0;
            int total = study.Count;

            
            foreach (DTO_StackAndCard card in study)
            {
                Console.WriteLine($@"No: {card.CardNumberInStack}");
                Console.WriteLine($@"{card.CardFront}");
                string input = Console.ReadLine();
                bool match = Helpers.CompareStrings(input, card.CardBack);
               if (match == true)
               {
                    Console.WriteLine("Correct");
                    correct++;
                }
               else if(match == false)
               {
                    Console.WriteLine("Incorrect");
                    Console.WriteLine($@"You entered{input}");
                    Console.WriteLine($@"The correct awnser is {card.CardBack}");
                }


            }
            StudySession thisStudySession = new StudySession(stackID,date,stackName,correct,total);
            //Display study session
        }
    }

  
}
