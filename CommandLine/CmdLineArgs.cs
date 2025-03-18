using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vsocasm.CommandLine
{
    public class CommandLineArgsEntry
    {
        public string[] Key { get; set; }
        public Argument Object { get; set; }

        public bool IsKey(string key) { return Key.Contains(key);  }
    }
    public class CommandLineArgs
    {
        private readonly List<CommandLineArgsEntry> _arguments = new();
        private readonly List<Argument> _requiredArguments = new();

        public string ApplicationInfo { get; set; }
        public string ExecutableName { get; set; }

        public CommandLineArgs()
        {
            ExecutableName = System.IO.Path.GetFileNameWithoutExtension(Assembly.GetCallingAssembly().Location);

            // Registrierung des Help-Arguments
            RegisterArgument( ["-h"], new HelpArgument());
        }

        // Methode zum Registrieren von Argumenten
        public void RegisterArgument(string[] key, Argument argument)
        {
            var _entry = new CommandLineArgsEntry()
            {
                Key = key,
                Object = argument
            };
            _arguments.Add(_entry);

            if (argument.IsRequired)
            {
                _requiredArguments.Add(argument);
            }
        }
        /// <summary>Processes a set of command line arguments.</summary>
		/// <param name="args">
		/// Command line arguments to process. This is usally coming from your Main method.
		/// </param>
		/// <returns>
		/// <c>true</c> if the arguments in <paramref name="args"/> are valid; otherwise
		/// <c>false</c> .
		/// </returns>
		public bool Parse(string[] args)
        {
            for(int i=0; i<args.Length; i++)
            {
               var _arg = args[i];
                if (_arg.StartsWith('-'))
                {
                    var argument = GetArgument(_arg);
                    if (argument == null)
                    {
                        Console.WriteLine($"Error: Argument {_arg} not used");
                        ShowHelp();
                        return false;
                    }
                    ;

                    if (argument.NeedsValue == true)
                    {
                        if (argument.Validate(args[i + 1] as object)) {
                            i += 1;
                            argument.SetValue<string>(args[i]);
                        } else {
                            argument.SetValue<object>(argument.DefaultValue );
                        }
                    }
                    else
                    {
                        argument.SetValue<bool>(true);
                    }
                    argument.Process();
                } 
            }

            foreach(var arg in _arguments)
            {
                if(arg.Object.IsRequired && arg.Object.GetValue<object>() == null)
                {
                    Console.WriteLine($"Error: Missing required argument {arg.Key[0]}");
                    ShowHelp();
                    return false;
                }
            }
            return true;
        }

        private Argument GetArgument(string arg)
        {
            foreach (var entry in _arguments)
            {
                if (entry.IsKey(arg))
                {
                    return entry.Object;
                }
            }
            return null;
        }

        private string GetArgName(string arg)
        {
            return string.Join("", arg.Split('-'));
        }

        private void ShowHelp()
        {
            Console.WriteLine("Usage: " + ExecutableName + " [Arguments]");
            Console.WriteLine($"\nDescription:\n{ApplicationInfo}");
            Console.WriteLine("\nArguments:");

            foreach (var arg in _arguments)
            {
                Console.WriteLine($"  {string.Join(", ", arg.Key)}: {arg.Object.HelpText}");
            }
            
        }
    }
}
