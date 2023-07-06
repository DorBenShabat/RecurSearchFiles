using System;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter Path To Directory:");
        string input = Console.ReadLine();
        string path = input.Trim('"');

        Console.WriteLine("Select an option:");
        Console.WriteLine("1. Count files and directories");
        Console.WriteLine("2. Display files by extension");
        Console.WriteLine("3. Search for a specific file");
        Console.WriteLine("4. Display all files");

        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                CountFilesAndDirectories(path);
                break;
            case "2":
                Console.WriteLine("Enter file extension:");
                string extension = Console.ReadLine();
                DisplayFilesByExtension(path, extension);
                break;
            case "3":
                Console.WriteLine("Enter file name to search:");
                string fileName = Console.ReadLine();
                SearchFile(path, fileName);
                break;
            case "4":
                TraverseDirectory(path);
                break;
            default:
                Console.WriteLine("Invalid option. Exiting program.");
                break;
        }
    }

    public static void CountFilesAndDirectories(string path)
    {
        int fileCount = 0;
        int directoryCount = 0;

        CountFilesAndDirectoriesRecursive(path, ref fileCount, ref directoryCount);

        Console.WriteLine($"Total Files: {fileCount}");

        if (directoryCount - 1 != 0)
            Console.WriteLine($"Total Directories: {directoryCount - 1}");
    }

    private static void CountFilesAndDirectoriesRecursive(string path, ref int fileCount, ref int directoryCount)
    {
        if (path == null || !Directory.Exists(path))
            return;

        directoryCount++;

        string[] files = Directory.GetFiles(path);
        fileCount += files.Length;

        string[] subdirectories = Directory.GetDirectories(path);
        foreach (string subdirectory in subdirectories)
        {
            CountFilesAndDirectoriesRecursive(subdirectory, ref fileCount, ref directoryCount);
        }
    }

    public static void DisplayFilesByExtension(string path, string extension)
    {
        Console.WriteLine($"Files with extension '{extension}' in directory: {path}:");

        bool filesFound = DisplayFilesByExtensionRecursive(path, extension);

        if (!filesFound)
        {
            Console.WriteLine("No files found with the specified extension.");
        }
    }


    private static bool DisplayFilesByExtensionRecursive(string path, string extension)
    {
        if (path == null || !Directory.Exists(path))
            return false;

        bool filesFound = false;

        string searchPattern = "*." + extension.TrimStart('.'); 

        string[] files = Directory.GetFiles(path, searchPattern);
        foreach (string file in files)
        {
            Console.WriteLine($"Path To File: {file}");
            filesFound = true;
        }

        string[] subdirectories = Directory.GetDirectories(path);
        foreach (string subdirectory in subdirectories)
        {
            if (DisplayFilesByExtensionRecursive(subdirectory, extension))
                filesFound = true;
        }

        return filesFound;
    }




    public static void SearchFile(string path, string partialFileName)
    {
        Console.WriteLine($"Searching for file '{partialFileName}' in directory: {path}...");

        bool fileFound = SearchFileRecursive(path, partialFileName);

        if (!fileFound)
        {
            Console.WriteLine("File not found.");
        }
    }

    private static bool SearchFileRecursive(string path, string partialFileName)
    {
        if (path == null || !Directory.Exists(path))
            return false;

        string[] files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
        bool fileFound = false;

        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            if (fileName.IndexOf(partialFileName, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Console.WriteLine($"Path To File: {file}");
                fileFound = true;
            }
        }

        string[] subdirectories = Directory.GetDirectories(path);
        foreach (string subdirectory in subdirectories)
        {
            if (SearchFileRecursive(subdirectory, partialFileName))
                fileFound = true;
        }

        return fileFound;
    }




    public static void TraverseDirectory(string path)
    {
        if (path == null || !Directory.Exists(path))
            return;

        Console.WriteLine($"Directory: {path}");

        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            Console.WriteLine($"File: {file}");
        }

        string[] subdirectories = Directory.GetDirectories(path);
        foreach (string subdirectory in subdirectories)
        {
            TraverseDirectory(subdirectory);
        }
    }
}
