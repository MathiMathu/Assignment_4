using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Defining the variables
            string FolderName;
            DateTime FolderCreatedDate;
            string FolderOwner;
            string[] FileTypes;
            int FileNumber;
            string FileName;
            DateTime FileCreatedDate;
            string Content;

            var root = $@"C:\test";

            //null check in the given path
            if (Directory.Exists(root))
            {
                Console.WriteLine("Directory exists");
            }
            else
            {
                Console.WriteLine("Directory does not exist");
            }

            DirectoryInfo info = new DirectoryInfo(root);

            //Getting the folders by sorting it with the created date
            string [] folders = info.EnumerateDirectories().OrderBy(d => (d.LastWriteTime)).Select(d => (d.FullName)).ToArray();
            Stack myStack = new Stack();
            List<string> files = new List<string>();
            List<string> counts = new List<string>();


            foreach (var folder in folders)
            {
                var path = folder;
                FolderName = Path.GetFileNameWithoutExtension(path);
                FolderCreatedDate = Directory.GetCreationTime(path);
                myStack.Push(FolderName);

                DirectoryInfo info2 = new DirectoryInfo(path);
                //Getting the files by sorting it with the created date
                var filess = info2.EnumerateFiles().OrderBy(d => (d.CreationTime)).Select(d => (d.FullName)).ToArray();

                foreach(var f in filess)
                {
                    files.Add(f);
                }
                int count = filess.Count();
                counts.Add(count.ToString());
            }

            int length = files.Count;   
            Console.WriteLine("Tray is as follows:");
            int j = counts.Count;

            //Implemented the tray using stack and list
            foreach (var folder in myStack)
            {
                Console.WriteLine(folder); 
                
                for (int i = length-Convert.ToInt32(counts[j-1]); i < length; i++)
                {
                    var path1 = files[i];
                    FileName = Path.GetFileNameWithoutExtension(path1);
                    files[i] = FileName;
                    Console.Write(FileName + ",");
                }
                length = length - Convert.ToInt32(counts[j-1]);
                j = j - 1;
                Console.WriteLine();
            }

            Console.WriteLine();
            
            //Search the folder
            Console.WriteLine("Enter the folder name to be searched:");
            string folderNamee = Console.ReadLine();
            Console.WriteLine("The status of the searched folder:"+myStack.Contains(folderNamee));

            //Search the file
            Console.WriteLine("Enter the file name to be searched:");
            string fileName = Console.ReadLine();
            Console.WriteLine("The status of the searched folder:" + files.Contains(fileName));
        }
    
    }
}
