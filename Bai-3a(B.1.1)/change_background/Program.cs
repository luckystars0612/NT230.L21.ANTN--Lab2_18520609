using System;
using System.Runtime.InteropServices;

namespace change_background
{
    class Program
    {
        static void Main(string[] args)
        {
            string photo = @"F:\wallpaper2you_521074.jpg";
            DisplayPicture(photo);
        }
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, uint fWinIni);

        private const uint SPI_SETDESKWALLPAPER = 0x14;
        private const uint SPIF_UPDATEINIFILE = 0x1;
        private const uint SPIF_SENDWININICHANGE = 0x2;

        private static void DisplayPicture(string file_name)
        {
            uint flags = 0;
            if (!SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0, file_name, flags))
            {
                Console.WriteLine("Error");
            }
        }
    }
}
