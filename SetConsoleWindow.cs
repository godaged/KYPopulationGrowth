using System;
using System.Runtime.InteropServices;

namespace KYPopulationGrowth
{
    static class ConsoleWindow
    {
        //got from internet to rezise console windows
        public static IntPtr HWND_BOTTOM = (IntPtr)1;
        public static IntPtr HWND_TOP = (IntPtr)0;

        public static uint SWP_NOSIZE = 1;
        public static uint SWP_NOZORDER = 4;

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, uint wFlags);

        //my method to resize console
        public static void SetSize()
        {
            var consoleWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            int cw = Console.LargestWindowWidth;
            int ch = Console.LargestWindowHeight;
            //move consolw window to top left corner
            SetWindowPos(consoleWnd, 0, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
            //resize the Console window to be with half of the screen and full height
            Console.SetWindowSize(cw / 2, ch);
        }

        public static void DrawWelcome()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            
            //Console.Write("Press any key to continue");
            //Console.ReadKey();

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(@"                _____         ____   _____             _____   ");
            Console.WriteLine(@"       |      ||      |      /      /     \  |\    /| |        ");
            Console.WriteLine(@"       |  /\  ||_____ |      |      |      | | \  / | |_____   ");
            Console.WriteLine(@"       | /  \ ||      |      |      |      | |  \/  | |        ");
            Console.WriteLine(@"       |/    \||_____ |_____ \____  \_____/  |      | |_____   ");
            Console.WriteLine(" ");
            DrawCodeLouisville();
        }
        public static void DrawExit()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.WriteLine(@"     _______                                      ____           ");
            Console.WriteLine(@"        |    |    |   /\   |\   | |   /   \    / /    \  |     | ");
            Console.WriteLine(@"        |    |____|  /__\  | \  | |__/     \__/  |     | |     | ");
            Console.WriteLine(@"        |    |    | |    | |  \ | |  \       /   |     | |     | ");
            Console.WriteLine(@"        |    |    | |    | |   \| |   \     /    \____/   \___/  ");
            Console.WriteLine(" ");
            DrawCodeLouisville();
        }
        public static void DrawCodeLouisville()
        {
            Console.WriteLine(@"                                       |                      ");
            Console.WriteLine(@"                    ____  ____    ____ |  _____               ");
            Console.WriteLine(@"               /   /     /    \  /    \| /     \   \          ");
            Console.WriteLine(@"              /    |     |     | |     | |_____/    \         ");
            Console.WriteLine(@"              \    |     |     | |     | |          /         ");
            Console.WriteLine(@"               \   \____ \____/  \____/| \_____/   /          ");
            Console.WriteLine(" ");
            Console.WriteLine(@"               ____              ____                    _____ ");
            Console.WriteLine(@"       |      /    \  |     | | /     \      / | |      |      ");
            Console.WriteLine(@"       |      |     | |     | | \___   \    /  | |      |_____ ");
            Console.WriteLine(@"       |      |     | |     | |     \   \  /   | |      |      ");
            Console.WriteLine(@"       |_____ \____/   \___/  | ____/    \/    | |_____ |_____ ");
            Console.WriteLine("");
        }
    }
}
