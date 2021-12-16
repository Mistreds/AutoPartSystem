using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoPartSystem.Model
{
    public static class CheckUpdates
    {
        public static void CheckUpdate()
        {
            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Version.ToString());
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://spiriddp.beget.tech/version.txt");
            // устанавливаем метод на загрузку файлов
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            // если требуется логин и пароль, устанавливаем их
            request.Credentials = new NetworkCredential("spiriddp_2", "AutoPart1_");
            //request.EnableSsl = true; // если используется ssl

            // получаем ответ от сервера в виде объекта FtpWebResponse
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            // получаем поток ответа
            Stream responseStream = response.GetResponseStream();
            // сохраняем файл в дисковой системе
            // создаем поток для сохранения файла
            FileStream fs = new FileStream("newVersion.txt", FileMode.Create);

            //Буфер для считываемых данных
            byte[] buffer = new byte[64];
            int size = 0;

            while ((size = responseStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fs.Write(buffer, 0, size);

            }
            fs.Close();
            var version = "";
            using (FileStream fstream = File.OpenRead($"newVersion.txt"))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                version = textFromFile;
            }
            File.Delete("newVersion.txt");
            response.Close();
            if (version == Assembly.GetExecutingAssembly().GetName().Version.ToString())
            {
                MessageBox.Show("Программа не требует обновлений", "Внимание");
            }
            else
            {
                request = (FtpWebRequest)WebRequest.Create("ftp://spiriddp.beget.tech/AutoPart.exe");
                // устанавливаем метод на загрузку файлов
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                // если требуется логин и пароль, устанавливаем их
                request.Credentials = new NetworkCredential("spiriddp_2", "AutoPart1_");
                //request.EnableSsl = true; // если используется ssl

                // получаем ответ от сервера в виде объекта FtpWebResponse
                response = (FtpWebResponse)request.GetResponse();

                // получаем поток ответа
                responseStream = response.GetResponseStream();
                // сохраняем файл в дисковой системе
                // создаем поток для сохранения файла
                fs = new FileStream("AutoPart.exe", FileMode.Create);

                //Буфер для считываемых данных
                buffer = new byte[64];
                size = 0;

                while ((size = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fs.Write(buffer, 0, size);

                }
                fs.Close();
                Process.Start("AutoPart.exe");
                Application.Current.Shutdown();
            }


            Console.WriteLine("Загрузка и сохранение файла завершены");
        }
    }
}
