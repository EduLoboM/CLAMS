using System.Text;

Console.OutputEncoding = Encoding.UTF8;

HashSet<string> includes = new HashSet<string>();

includes.Add("echo");
includes.Add("exit");
includes.Add("type");
includes.Add("cd");
includes.Add("ls");
includes.Add("exec");
includes.Add("mkdir");
includes.Add("rm");
includes.Add("touch");
includes.Add("cat");
includes.Add("truth");
includes.Add("tem");

while (true)
{
    string tdr = Directory.GetCurrentDirectory();
    Console.Write($"{tdr} → ");
    string? command = Console.ReadLine();
    if (string.IsNullOrEmpty(command))
    {
        continue;
    }
    string[] split = command.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
    string parameter = split.Length > 1 ? split[1].Trim() : string.Empty;
    command = split[0].ToLowerInvariant();

    if (command == "echo")
    {
        Console.WriteLine(parameter);
        continue;
    }

    if (command == "exit")
    {
        break;
    }

    if (command == "type")
    {
        if (includes.Contains(parameter))
        {
            Console.WriteLine($"{parameter}: is a command");
        }
        else
        {
            Console.WriteLine($"{parameter}: isn't a command");
        }
        continue;
    }

    if (command == "cd")
    {
        try
        {
            Directory.SetCurrentDirectory(parameter);
        }
        catch (Exception error)
        {
            Console.WriteLine($"{error.Message}");
        }
        continue;
    }

    if (command == "ls" && string.IsNullOrEmpty(parameter))
    {
        try
        {
            var list = Directory.EnumerateFileSystemEntries(tdr);

            foreach (string item in list)
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

    if (command == "ls" && !string.IsNullOrEmpty(parameter))
    {
        try
        {
            var list = Directory.EnumerateFileSystemEntries(parameter);
            foreach (string item in list)
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

    if (command == "exec")
    {
        split = parameter.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        command = split[0];
        parameter = split[1];

        string echo = Path.Combine(tdr, command);
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
        continue;
    }

    if (command == "mkdir")
    {
        string echo = Path.Combine(tdr, parameter);
        if (!Directory.Exists(echo))
        {
            Directory.CreateDirectory(echo);
            Console.WriteLine($"{parameter}: created");
        }
        else
        {
            Console.WriteLine($"{parameter}: already exists");
        }
        continue;
    }

    if (command == "rm")
    {
        if (string.IsNullOrEmpty(parameter))
        {
            Console.WriteLine("rm: missing parameter");
        }
        else
        {
            string echo = Path.Combine(tdr, parameter);
            if (!File.Exists(echo) && !Directory.Exists(echo))
            {
                Console.WriteLine($"{parameter}: doesn't exist");
            }
            else
            {
                if (File.Exists(echo))
                {
                    File.Delete(echo);
                    Console.WriteLine($"{parameter}: deleted");
                }

                if (Directory.Exists(echo))
                {
                    Directory.Delete(echo);
                    Console.WriteLine($"{parameter}: deleted");
                }
            }
        }
        continue;
    }

    if (command == "touch")
    {
        string echo = Path.Combine(tdr, parameter);
        if (!File.Exists(echo))
        {
            File.Create(echo).Close();
            Console.WriteLine($"{parameter}: created");
        }
        else
        {
            Console.WriteLine($"{parameter}: already exists");
        }
        continue;
    }

    if (command == "cat")
    {
        string echo = Path.Combine(tdr, parameter);
        if (File.Exists(echo))
        {
            string content = File.ReadAllText(echo);
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine($"{parameter}: doesn't exist");
        }
        continue;
    }

    if (command == "truth" && string.IsNullOrEmpty(parameter))
    {

        List<string> truths = new List<string>
        {
            "A sorte favorece os audazes... mas evita os imprudentes!",
            "Um bug no código é como um grão de areia na praia: sempre haverá mais.",
            "while(true) { /* Viva intensamente */ }",
            "A resposta para a vida, o universo e tudo mais: 42.",
            "Ctrl+C, Ctrl+V: a base da sabedoria moderna."
        };

        Random random = new Random();
        string truth = truths[random.Next(truths.Count)];

        string animal = @"       (\---/)   /
       |     |  /
       \ ^ ^ /  .-.
        \_o_/  / /
       /`   `\/  |
      /       \  |
      \ (   ) /  |
     / \_) (_/ \ /
    |   (\-/)   |
    \  --^o^--  /
     \ '.___.' /
    .'  \-=-/  '.
   /   /`   `\   \
  (//./       \.\\)
   `""`         `""`";
        var lines = truth.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        int maxLength = lines.Max(l => l.Length);
        string top = " " + new string('_', maxLength + 2);
        string bottom = " " + new string('-', maxLength + 2);
        var balloon = new StringBuilder();
        balloon.AppendLine(top);

        foreach (var line in lines)
        {
            balloon.AppendLine($"< {line.PadRight(maxLength)} >");
        }
        balloon.AppendLine(bottom);
        Console.Write(balloon.ToString());
        Console.WriteLine(animal);
        continue;
    }

    if (command  == "tem")
    {
        if (!string.IsNullOrEmpty(parameter))
        {
            if (parameter.EndsWith("?"))
            {
                string echo = parameter.Remove(parameter.Length - 1);
                echo = echo.Substring(1);
                echo = "n" + echo;
                Console.WriteLine($"{echo}");
            }
            else
            {
                var echo = parameter.Substring(1);
                echo = "N" + echo;
                Console.WriteLine($"{echo}");
            }
        }
        else
        {
            Console.WriteLine("Nem");
        }
        continue;
    }

    Console.WriteLine($"{command}: isn't a command");
}



