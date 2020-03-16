using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;

namespace NeiSpotlightV2
{
    class Program
    {
        //cargamos libreria para poder cambiar el wallpaper
        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uiAction, int uiParam,
        string pvParam, uint fWinIni);
        static void Main(string[] args)
        {
            //creamos las rutas para el usuario que esta ejecutando el programa
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";
            string destPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\NeiSpotlight";
            //creamos nuestra carpeta donde guardad las fotos que vamos usando
            System.IO.Directory.CreateDirectory(destPath);
            //buscamos en nuestro directorio  el archivo mas nuevo
            var directory = new DirectoryInfo(path);
            var myFile = directory.GetFiles()
                         .OrderByDescending(f => f.LastWriteTime)
                         .First();
            
            //comprobamos que ese archivo es una imagen horizontal (para no coger imagenes de formato movil), si no es horizontal cogerá la siguiente que suele ser horizontal
            if (Image.FromFile(myFile.FullName).Width < Image.FromFile(myFile.FullName).Height)
            {
                myFile = directory.GetFiles()
                         .OrderByDescending(f => f.LastWriteTime)
                         .ElementAt(1);
            }
            // cambiamos el nombre del archivo para añadirle la extension jpg
            string destFileName = myFile.Name + ".jpg";
            //creamos la ruta donde se va a guardar que nesecita ruta completa y nombre del archivo
            string destFile = System.IO.Path.Combine(destPath, destFileName);
            //creamos una copia de nuestra foto en nuestro directorio
            System.IO.File.Copy(myFile.FullName, destFile, true);
            //Ponemos como wallpaper nuestra imagen
            SystemParametersInfo(0x0014, 0, destFile, 0x0001);
        }
    }
}

