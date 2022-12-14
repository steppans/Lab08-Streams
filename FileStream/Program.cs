using System.IO.Compression;

Console.Write("Enter name of file you want to find: ");
string filename = Console.ReadLine() ?? string.Empty;

if (string.IsNullOrEmpty(filename))
{
    Console.WriteLine("Can't find file with empty name");
    return;
}

Console.Write("Enter directory, where you want to find this file: ");
string directory = Console.ReadLine() ?? string.Empty;

if (string.IsNullOrEmpty(filename))
{
    Console.WriteLine("Can't find file in directory with empty name");
    return;
}

IEnumerable<string> allfiles = Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories);

var neadedFiles = from file in allfiles
                  where Path.GetFileNameWithoutExtension(file) == filename
                  select file;

if(neadedFiles.Count() == 0)
{
    Console.WriteLine($"Can't find files with file name [{filename}] in this directory: {directory}");
    return;
}

foreach(var file in neadedFiles)
{
    Console.WriteLine("/-------------------------------------/");
    displayFileInfo(file);
    Console.WriteLine();
}


void displayFileInfo(string file)
{
    Console.WriteLine(file);
    IEnumerable<string> lines = File.ReadLines(file);
    int i = 1;
    foreach(string line in lines)
    {
        Console.WriteLine($"{i++}. {line}");
    }
    Console.WriteLine();
    CompressFileOrNot(file);
}

void CompressFileOrNot(string file)
{
    string ans = string.Empty;
    while (ans != "y" && ans != "n")
    {
        if (!string.IsNullOrEmpty(ans))
        {
            Console.WriteLine("Incorrect output. Try Again.");
        }
        Console.Write("Do you want to compress this file?(y/n)");
        ans = Console.ReadLine() ?? " ";
    }

    if (ans == "n")
    {
        return;
    }

    if (ans == "y")
    {
        // Поток для чтения исходного файла
        using FileStream sourceStream = new FileStream(file, FileMode.Open);
        // Поток для записи сжатого файла
        string targetFile = Path.Combine(Path.GetDirectoryName(file) ?? "", Path.GetFileNameWithoutExtension(file) + ".gz");
        using FileStream targetStream = new FileStream(targetFile, FileMode.Create);

        using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
        {
            sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
        }

        Console.WriteLine($"Сompression of file [{file}] is complete.");
        Console.WriteLine($"Original size: {sourceStream.Length}  compressed size: {new FileInfo(targetFile).Length}");

    }
}


