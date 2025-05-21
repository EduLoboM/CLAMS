using System.IO;

Console.OutputEncoding = System.Text.Encoding.UTF8;

HashSet<string> includes = new HashSet<string>();

Console.OutputEncoding = System.Text.Encoding.UTF8;

includes.Add("type");
includes.Add("echo");
includes.Add("exit");
includes.Add("cd");
includes.Add("ls");
includes.Add("exec");

while (true)
{
    var tdr = Directory.GetCurrentDirectory();
    Console.Write($"{tdr} → ");
    var command = Console.ReadLine();

    if (command.StartsWith("echo "))
    {
        var echo = command.Substring(5).Trim();
        Console.WriteLine(echo);
        continue;
    }

    if (command == "exit")
    {
        break;
    }

    if (command.StartsWith("type "))
    {
        var echo = command.Substring(5).Trim();
        if (includes.Contains(echo))
        {
            Console.WriteLine($"{echo}: is a command");
        }
        else
        {
            Console.WriteLine($"{echo}: isn't a command");
        }
        continue;
    }

    if (command.StartsWith("cd "))
    {
        var echo = command.Substring(3).Trim();
        try
        {
            Directory.SetCurrentDirectory(echo);
        }
        catch (Exception error)
        {
            Console.WriteLine($"{error.Message}");
        }
        continue;
    }

    if (command.StartsWith("ls"))
    {
        try
        {
            var list = Directory.EnumerateFileSystemEntries(tdr);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
        catch (Exception error)
        {
            Console.WriteLine($"{error.Message}");
        }
        continue;
    }

    if (command.StartsWith("ls "))
    {
        var echo = command.Substring(3).Trim();
        try
        {
            var list = Directory.EnumerateFileSystemEntries(echo);
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
        catch (Exception error)
        {
            Console.WriteLine($"{error.Message}");
        }
        continue;
    }

    if (command.StartsWith("exec "))
    {
        var echo = command.Substring(5).Trim();
        var split = echo.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        var program = split[0];
        var parameter = split[1];

        echo = Path.Combine(tdr, program);
        if (File.Exists(echo))
        {
            var process = new System.Diagnostics.Process();
            process.StartInfo.FileName = echo;
            process.StartInfo.Arguments = parameter;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = tdr;
            process.StartInfo.RedirectStandardOutput = true;    
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();
            Console.WriteLine(output);
            Console.WriteLine(error);
        }
        else
        {
            Console.WriteLine($"{echo}: isn't a executable");
        }

    }

    Console.WriteLine($"{command}: isn't a command");
}



