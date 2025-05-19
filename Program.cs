using System.IO;

Console.OutputEncoding = System.Text.Encoding.UTF8;

HashSet<string> includes = new HashSet<string>();

Console.OutputEncoding = System.Text.Encoding.UTF8;

includes.Add("type");
includes.Add("echo");
includes.Add("exit");

while (true)
{
    var tdr = Directory.GetCurrentDirectory();
    Console.Write($"{tdr}>>> ");
    var command = Console.ReadLine();

    if (command.StartsWith("echo ") == true)
    {
        var echo = command.Substring(5).Trim();
        Console.WriteLine(echo);
        continue;
    }

    if (command == "exit")
    {
        break;
    }

    if (command.StartsWith("type ") == true)
    {
        var echo = command.Substring(5).Trim();
        if (includes.Contains(echo) == true)
        {
            Console.WriteLine($"{echo}: is a command");
        }
        else
        {
            Console.WriteLine($"{echo}: isn't a command");
        }
        continue;
    }

    if (command.StartsWith("cd ") == true)
    {
        var echo = command.Substring(3).Trim();
        Directory.SetCurrentDirectory(echo); 
        continue;
    }

    Console.WriteLine($"{command}: isn't a command");
}



