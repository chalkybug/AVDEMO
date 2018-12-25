using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Collections;
using System.Management;
using System.Threading;
using ANtivirus;
using static ANtivirus.DbContext;

namespace Antivirus_CS
{
    class Program
    {
        private static Dictionary<string, MalwareTypes> signatures = DbContext.GetAll();

        private static void ScanProcess()
        {
            Process[] processlist = Process.GetProcesses();

            foreach (Process theprocess in processlist)
            {
                try
                {
                    string processlocation = GetMainModuleFilepath(theprocess.Id);
                    byte[] fileBytes = File.ReadAllBytes(processlocation);
                    string read = Encoding.UTF8.GetString(fileBytes);

                    if (ScanHelper.Scan(read, ScanHelper.ScanTypes.Mutil, signatures))
                    {
                        Console.WriteLine($"Malware is {theprocess.ProcessName}");
                        //  theprocess.Kill();
                    }

                }
                catch
                {

                }

            }

        }

        private static void ScanFile()
        {
            Console.WriteLine("Enter path folder:\n");
            
            string path = @"C:\Users\luong\Desktop\demoScan";
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] pathsFile = directory.GetFiles("*.*", SearchOption.AllDirectories);
            List<string> lstPath = new List<string>();
            lstPath = pathsFile.Select(x => x.FullName).ToList();

            foreach (string item in lstPath)
            {

                if (item.Contains(".txt") || item.Contains(".pdf") || item.Contains(".doc"))
                {
                    continue;
                }
                else
                {
                    try
                    {
                        using (StreamReader stream = new StreamReader(item))
                        {
                            string read = stream.ReadToEnd();
                            if (ScanHelper.Scan(read, ScanHelper.ScanTypes.Mutil, signatures))
                            {
                                Console.WriteLine($"Malware is: {item}");
                            }
                        }

                    }
                    catch
                    {

                    }
                }
            }



        }

        private static string GetMainModuleFilepath(int processId)
        {
            string wmiQueryString = "SELECT ProcessId, ExecutablePath FROM Win32_Process WHERE ProcessId = " + processId;
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            {
                using (var results = searcher.Get())
                {
                    ManagementObject mo = results.Cast<ManagementObject>().FirstOrDefault();
                    if (mo != null)
                    {
                        return (string)mo["ExecutablePath"];
                    }
                }
            }
            return null;
        }


        static void Main(string[] args)
        {
            //ScanFile();
            ScanProcess();
            Console.ReadKey();
        }
    }
}
