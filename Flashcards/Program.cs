// Import the necessary functions from the Windows API
using System.Runtime.InteropServices;

[DllImport("kernel32.dll", SetLastError = true)]
static extern IntPtr GetStdHandle(int nStdHandle);

[DllImport("kernel32.dll", SetLastError = true)]
static extern int SetConsoleOutputCP(uint wCodePageID);

// Set the console font to Lucida Console (or any font that supports Unicode)
Console.OutputEncoding = System.Text.Encoding.UTF8;
// Set the console output code page to UTF-8
SetConsoleOutputCP(65001);

Console.WriteLine("Welcome to Flash Cards your study program.");

char upArrow = '\u2191';
char downArrow = '\u2193';
char checkMark = '\u2713';

string black = "\u001b[30m";
string red = "\u001b[31m";
string green = "\u001b[32m";
string yellow = "\u001b[33m";
string blue = "\u001b[34m";
string Magenta = "\u001b[35m";
string Cyan = "\u001b[36m";
string White = "\u001b[37m";
string resetColor = "\u001b[0m";

Console.WriteLine($@"Use {upArrow} and {downArrow} to navigate and press {green}Enter{resetColor} to select.");

ConsoleKeyInfo key;
int option = 1;
bool isSelected = false;
(int left, int top) = Console.GetCursorPosition();
string color = $"{checkMark}{green}   ";

Console.CursorVisible = false;

while (!isSelected)
{
    Console.SetCursorPosition(left, top);

    Console.WriteLine($@"{(option == 1 ? color : "    ")}Option 1{resetColor}");
    Console.WriteLine($@"{(option == 2 ? color : "    ")}Option 2{resetColor}");
    Console.WriteLine($@"{(option == 3 ? color : "    ")}Option 3{resetColor}");

    key = Console.ReadKey(true);

    switch(key.Key)

    {
        case ConsoleKey.DownArrow:
            option = (option == 3 ? 1 : option +1);
            break;

        case ConsoleKey.UpArrow:
            option = (option == 1 ? 3 : option -1);
            break;

        case ConsoleKey.Enter:
            isSelected = true;
            break;
    }

}

Console.WriteLine($@"You selected option {option}");

