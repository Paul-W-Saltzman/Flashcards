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

        internal StudySession()
        {

        }

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

        //internal static void LoadSeedDataStudySessions()
        //{
        //    StudySession(1)
        //    StudySession(1)
        //    StudySession(1)
        //    StudySession(2)
        //    StudySession(2)
        //    StudySession(2)
        //    StudySession(3)
        //    StudySession(3)
        //    StudySession(3)
        //    StudySession(4)
        //    StudySession(4)
        //    StudySession(4)
        //    StudySession(5)
        //    StudySession(5)
        //    StudySession(5)

        //}


        public static void Study(List<DTO_StackAndCard> study)
        {
            DTO_StackAndCard stack = study[0];
            int stackID = stack.StackID;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            string stackName = stack.StackName;
            int correct = 0;
            int total = study.Count;

            
            foreach (DTO_StackAndCard card in study)
            {
                Console.Clear();
                Helpers.ViewCard(card, true);
                Console.WriteLine();
                Console.Write("Awnser: ");
                Console.CursorVisible = true;
                string input = Console.ReadLine();
                bool match = Helpers.CompareStrings(input, card.CardBack);
                Console.CursorVisible = false;
               if (match == true)
               {
                    Console.WriteLine("Correct");
                    Console.ReadKey();
                    correct++;
                }
               else if(match == false)
               {
                    Console.WriteLine("Incorrect");
                    Console.WriteLine($@"You entered: {input}");
                    Console.WriteLine("The Awnser is:");
                    Helpers.ViewCard(card,false);
                    Console.ReadKey();
                }


            }
            StudySession thisStudySession = new StudySession(stackID,date,stackName,correct,total);
            Console.WriteLine($@"You got {correct} out of {total} correct.");
            Console.ReadLine(); 
        }
    }
}
