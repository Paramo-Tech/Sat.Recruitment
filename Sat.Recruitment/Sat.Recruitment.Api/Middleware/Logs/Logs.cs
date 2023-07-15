using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using Microsoft.Extensions.Hosting;

namespace Sat.Recruitment.Api.Middleware.Logs
{
    public class Logs
    {
        public string Proceso { get; set; }
        public List<string> Lineas { get; set; }
        private readonly IWebHostEnvironment _environment;

        public Logs(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public void GrabarLogs()
        {
            string path = "";
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            path = baseDir + "Logs\\" + string.Format("{0:yyyyMMdd}", DateTime.Now) + "\\";

            try
            {

                if (_environment.IsDevelopment())
                {
                    path = Directory.GetCurrentDirectory() + "\\Logs\\" + string.Format("{0:yyyyMMdd}", DateTime.Now) + "\\";
                }
                else
                {
                    path = Directory.GetCurrentDirectory() + "//Logs//" + string.Format("{0:yyyyMMdd}", DateTime.Now) + "//";
                }


                this.Lineas = this.AgregarFechayHora(this.Lineas);
                //verifico si existe el directorio 
                if (Directory.Exists(path))
                {
                    //creo o escribo en el fichero txt 
                    this.CreateOrWriteFile(path);
                    this.Lineas = new List<string>();
                }
                else
                {
                    //creo el directorio y creo o escribo en el fichero txt
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    this.CreateOrWriteFile(path);
                    this.Lineas = new List<string>();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"error - {e.Message}");
            }

        }

        public List<string> AgregarFechayHora(List<string> lineas)
        {
            List<string> nuevasLineas = new List<string>();
            foreach (string item in lineas.ToList())
            {
                string value = string.Join(" ", string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now), item);
                nuevasLineas.Add(value);
            }

            return nuevasLineas;
        }

        public void CreateOrWriteFile(string path)
        {
            if (File.Exists(path + this.Proceso + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".txt"))
            {

                File.AppendAllLinesAsync(path + this.Proceso + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".txt", this.Lineas);
            }
            else
            {

                File.AppendAllLinesAsync(path + this.Proceso + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".txt", this.Lineas);
            }
        }

        public bool depurarLogs(int CantidadDías, string basedir)
        {
            bool result = false;
            string path = "";
            //Limpio los logs a partir de una cantidad de  días parametrizados hacias atras
            for (int i = CantidadDías; i <= 10; i++)
            {

                if (_environment.IsDevelopment())
                {
                    path = basedir + "\\Logs\\" + string.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(-i));
                }
                else
                {
                    path = basedir + "//Logs//" + string.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(-i));
                }



                if (Directory.Exists(path))
                {
                    result = true;

                    foreach (string item in Directory.GetFiles(path))
                    {
                        File.Delete(item);
                    }
                    Directory.Delete(path);

                }
            }

            return result;
        }
    }
}
