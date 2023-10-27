using System;
using System.Collections.Generic;
using System.Linq;
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




        public static void studySession(List<DTO_StackAndCard> studySession)
        {
            foreach (DTO_StackAndCard card in studySession)
            {

            }
        }
    }

  
}
