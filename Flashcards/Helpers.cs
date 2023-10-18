using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal static class Helpers
    {

        internal static void setEnvironmentVariables()
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            static extern IntPtr GetStdHandle(int nStdHandle);

            [DllImport("kernel32.dll", SetLastError = true)]
            static extern int SetConsoleOutputCP(uint wCodePageID);

            // Set the console font to Lucida Console (or any font that supports Unicode)
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // Set the console output code page to UTF-8
            SetConsoleOutputCP(65001);

        }


    }
}
