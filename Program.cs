using System;
using System.IO;
using System.IO.Compression;

namespace oop_13
{
    class Program
    {
        delegate void log(string str);
        static event log Notify = KENLogger.Log;

        static class KENLogger
        {
            public static void Log(string message)
            {
                using(StreamWriter sw = new StreamWriter("LogFile.txt", true))
                {
                    sw.Write(DateTime.Now);
                    sw.Write(' ');
                    sw.Write(message);
                    sw.WriteLine();
                }
            }
        }

        static class KENDiskInfo
        {
            private static DriveInfo[] drives = DriveInfo.GetDrives();
            public static void FreeSpace()
            {
                Console.WriteLine(drives[1].AvailableFreeSpace / 1073741824 + " Гбайт");
                Notify("Использован метод FreeSpace() класса KENDiskInfo");
            }
            public static void FileSystem()
            {
                Console.WriteLine(drives[1].DriveFormat);
                Notify("Использован метод FileSystem() класса KENDiskInfo");
            }
            public static void DriveInformation()
            {
                Console.WriteLine(drives[1].Name + ' ' + drives[1].TotalSize / 1073741824 + " Гбайт " + drives[1].AvailableFreeSpace / 1073741824 + " Гбайт " + drives[1].VolumeLabel);
                Notify("Использован метод DriveInformation() класса KENDiskInfo");
            }
        }

        static class KENFileInfo
        {
            public static void FullPath(string path)
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                    Console.WriteLine(file.FullName);
                else
                    Console.WriteLine("Файл не существует");
                Notify($"Использован метод FullPath() класса KENFileInfo на файл {path}");
            }
            public static void FileInformation(string path)
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                    Console.WriteLine(file.Name + ' ' + file.Extension + ' ' + file.Length + " Байт");
                else
                    Console.WriteLine("Файл не существует");
                Notify($"Использован метод FileInformation() класса KENFileInfo на файл {path}");
            }
            public static void FileDates(string path)
            {
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                    Console.WriteLine("Дата создания: " + file.CreationTime.ToString() + " Дата изменения: " + file.LastWriteTime);
                else
                    Console.WriteLine("Файл не существует");
                Notify($"Использован метод FileDates() класса KENFileInfo на файл {path}");
            }
        }

        static class KENDirInfo
        {
            public static void NumberOfFiles(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.Exists)
                    Console.WriteLine(directory.GetFiles().Length);
                else
                    Console.WriteLine("Директория не существует");
                Notify($"Использован метод NumberOfFiles() класса KENDirInfo на директорию {path}");
            }
            public static void TimeOfCreation(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.Exists)
                    Console.WriteLine(directory.CreationTime);
                else
                    Console.WriteLine("Директория не существует");
                Notify($"Использован метод TimeOfCreation() класса KENDirInfo на директорию {path}");
            }
            public static void NumberOfSubDirectories(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.Exists)
                    Console.WriteLine(directory.GetDirectories().Length);
                else
                    Console.WriteLine("Директория не существует");
                Notify($"Использован метод NumberOfSubDirectories() класса KENDirInfo на директорию {path}");
            }
            public static void ParentDirectory(string path)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                if (directory.Exists)
                    Console.WriteLine(directory.Parent);
                else
                    Console.WriteLine("Директория не существует");
                Notify($"Использован метод ParentDirectory() класса KENDirInfo на директорию {path}");
            }
        }

        static class KENFileManager
        {
            public static void task1()
            {
                Directory.CreateDirectory(@"D:\OOP\oop-13\bin\Debug\netcoreapp3.1\KENInspect");
                string path1 = @"D:\OOP\oop-13\bin\Debug\netcoreapp3.1\KENInspect\kendirinfo.txt";
                string path2 = @"D:\OOP\oop-13\bin\Debug\netcoreapp3.1\laadirinfo.txt";

                using (StreamWriter sw = new StreamWriter(path1, false, System.Text.Encoding.Default))
                {
                    foreach (DirectoryInfo d in new DirectoryInfo(DriveInfo.GetDrives()[1].Name).GetDirectories())
                        sw.WriteLine(d.Name);
                }
                FileInfo file1 = new FileInfo(path1);
                FileInfo file2 = file1.CopyTo(path2);
                File.Move(file2.FullName, file2.FullName.Substring(0, path2.Length - 7) + ".txt");
                file1.Delete();
            }
            public static void task2()
            {
                string path = @"D:\OOP\oop-13\bin\Debug\netcoreapp3.1\KENFiles";
                Directory.CreateDirectory(path);

                DirectoryInfo directory = new DirectoryInfo(@"D:\www");
                foreach (FileInfo f in directory.GetFiles())
                    if (f.Extension == ".png")
                        File.Copy(f.FullName, path + f.Name);
                Directory.Move(path, @"D:\OOP\oop-13\bin\Debug\netcoreapp3.1\KENInspect\KENFiles");
            }
            public static void task3()
            {
                string sourceFolder = @"D:\OOP\oop-13\bin\Debug\netcoreapp3.1\KENInspect\KENFiles";
                string zipFile = @"D:\OOP\oop-13\bin\Debug\netcoreapp3.1\KENInspect\test.zip";
                string targetFolder = @"D:\OOP\oop-13\bin\Debug\netcoreapp3.1\KENInspect\Decompression";
                ZipFile.CreateFromDirectory(sourceFolder, zipFile);
                Notify($"Папка {sourceFolder} архивирована в файл {zipFile}");

                ZipFile.ExtractToDirectory(zipFile, targetFolder);
                Notify($"Файл {zipFile} распакован в папку {targetFolder}");

                Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            KENDiskInfo.FreeSpace();
            KENDiskInfo.FileSystem();
            KENDiskInfo.DriveInformation();

            KENFileInfo.FullPath(@"D:\юзабилити\курсач\Артемида.pdf");
            KENFileInfo.FileInformation(@"D:\юзабилити\курсач\Артемида.pdf");
            KENFileInfo.FileDates(@"D:\юзабилити\курсач\Артемида.pdf");

            KENDirInfo.NumberOfFiles(@"D:\oop");
            KENDirInfo.TimeOfCreation(@"D:\oop");
            KENDirInfo.NumberOfSubDirectories(@"D:\oop");
            KENDirInfo.ParentDirectory(@"D:\oop");

            KENFileManager.task1();
            KENFileManager.task2();
            KENFileManager.task3();

            ///

            using (StreamReader sr = new StreamReader("LogFile.txt"))
            {
                Console.WriteLine(sr.ReadToEnd());
            }

            using (StreamReader sr = new StreamReader("LogFile.txt"))
            {
                int i = 0;
                while (sr.ReadLine() != null)
                {
                    i++;
                }
                Console.WriteLine($"Количество строк в логе:{ i }");
            }

            string[] log;
            using (StreamReader sr = new StreamReader("LogFile.txt"))
            {
                log = sr.ReadToEnd().Split("\n");
                for (int i = 0; i < log.Length-1; i++)
                {
                    if (log[i].Substring(12, 2) != DateTime.Now.ToString().Substring(12, 2))
                    {
                        log[i] = null;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter("LogFile.txt"))
            {
                foreach(string str in log)
                {
                    if( str != null)
                        sw.WriteLine(str);
                }
            }
        }
    }
}
