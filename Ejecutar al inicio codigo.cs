using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ejecutar_al_inicio
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory+ @"NeiSpotlight.exe");
             using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
             {

                 //key.SetValue("NeiSpotlight", AppDomain.CurrentDomain.BaseDirectory + @"NeiSpotlight.exe");
                 key.DeleteValue("NeiSpotlight", false);

             }

        }
    }
}
