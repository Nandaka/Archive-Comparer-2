using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using ArchiveComparer2.Library;

namespace ArchiveComparer2.Console
{
    class Program
    {
        public static ILog Logger = LogManager.GetLogger("ArchiveComparer2.Console");
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            Logger.Debug("Hello World");
            

            ArchiveDuplicateDetector worker = new ArchiveDuplicateDetector();
            worker.Notify +=new ArchiveDuplicateDetector.NotifyEventHandler(worker_Notify);

            List<string> paths = new List<string>();
            paths.Add(@"D:\New Folder");
            var option = new DuplicateSearchOption() { Paths = paths };
            List<DuplicateArchiveInfoList> list = worker.Search(option);
            
            foreach (var item in list)
            {
                System.Console.WriteLine(item.Original.ToString());
                foreach (var dup in item.Duplicates)
                {
                    System.Console.WriteLine(" - " + dup.ToString());
                }
            }

            System.Console.ReadLine();
        }

        private static void worker_Notify(object sender, NotifyEventArgs e)
        {
            System.Console.WriteLine(e.Status.ToString() + " :" + e.Message);
        }

    }
}
