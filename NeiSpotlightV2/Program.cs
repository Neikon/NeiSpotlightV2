using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

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
            //obtenemos el ancho de la pantalla
            var screenWidth = Screen.PrimaryScreen.Bounds.Width;
            //creamos nuestra carpeta donde guardad las fotos que vamos usando
            System.IO.Directory.CreateDirectory(destPath);
            //buscamos en nuestro directorio  el archivo mas nuevo
            var directory = new DirectoryInfo(path);
            //vamos a coger todas las imagenes buenas y copiarlas a nuestra carpeta, si ya esta en ella NO se sobreescribira
            
            for (int i = 0; i < directory.GetFiles().Length; i++)
            {
                var myFile = directory.GetFiles().ElementAt(i);
                var myImage = Image.FromFile(myFile.FullName);

                if (myImage.Width >= screenWidth  )
                {
                    var myDestFileJPG = System.IO.Path.Combine(destPath, myFile.Name + ".jpg");
                    //si el archivo existe en nuestra carpeta no hace nada
                    if (!System.IO.File.Exists(myDestFileJPG))
                    {
                        System.IO.File.Copy(myFile.FullName, myDestFileJPG, false);
                    }
                }
            }
            //De nuestra carpeta elegiremos la imagen mas reciente 
            var wallpaper = new DirectoryInfo(destPath).GetFiles().OrderByDescending(f => f.LastWriteTime).First();
            //Ponemos como wallpaper nuestra imagen
            SystemParametersInfo(0x0014, 0, wallpaper.FullName, 0x0001);
        }
    }
}

