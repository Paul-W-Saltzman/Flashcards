using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class StudySessionReport
    {
        internal int StackID;
        internal String StackName;
        internal int YEAR;
        internal int January;
        internal int February;
        internal int March;
        internal int April;
        internal int May;
        internal int June;
        internal int July;
        internal int August;
        internal int September;
        internal int October;
        internal int November;
        internal int December;


        internal static void ShowReport(List<StudySessionReport> studySessions)
        {

                Console.WriteLine("|Stack  |  Year  |  JAN  |  FEB  |  MAR  |  APR  |  MAY  |  JUN  |  JUL  |  AUG  |  SEP  |  OCT  |  NOV  |  DEC  |");
            foreach (StudySessionReport report in studySessions) 
            {
                Console.WriteLine($@"|{report.StackName}|{report.YEAR}|{report.January}|{report.February}|{report.March}|{report.April}|{report.May}|{report.June}|{report.July}|{report.August}|{report.September}|{report.October}|{report.November}|{report.December}|");
            }
            Console.ReadKey();
        }
    }
}
