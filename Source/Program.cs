using System;
using System.Diagnostics;
using System.IO;

namespace MiPPPI
{
    class Program
    {
        private const String _appName = "MiPPPI";

        public static void Main(String[] args)
        {
            if (args.Length != 3)
            {
                InvalidArguments();
            }

            String printerName = args[0], identifier = args[1], outputFileName = args[2];

            String appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            String portDirectory = Path.Combine(appDataPath, _appName, identifier);
            String port = Path.Combine(portDirectory, outputFileName);

            if (!Directory.Exists(portDirectory))
                Directory.CreateDirectory(portDirectory);

            if (AddLocalPortAndPrinter(printerName, port))
                Console.WriteLine($"Printer \"{printerName}\" with port \"{port}\" added successfully.");
            else
                Console.WriteLine("Failed to add printer.");

            Environment.Exit(0);
        }

        private static void InvalidArguments()
        {
            Console.WriteLine("\nError: Invalid Arguments!\n\nExpected:\n\tMiPPPI [printer] [identifier] [filename]\n");
            Console.WriteLine("\nExample:\n\tMiPPPi.exe \"MiPPPi PDF\" \"ExampleCompany\" \"document.pdf\"\n");
            Console.WriteLine("\nExiting...\n");
            Environment.Exit(1);
        }

        private static Boolean AddLocalPortAndPrinter(String printerName, String portName)
        {
            try
            {
                String psCommand = $@"
                    Add-PrinterPort -Name '{portName}';
                    Add-Printer -Name '{printerName}' -DriverName 'Microsoft Print To PDF' -PortName '{portName}'";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-Command \"{psCommand}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    String result = process.StandardOutput.ReadToEnd();
                    String error = process.StandardError.ReadToEnd();

                    if (process.ExitCode == 0)
                    {
                        Console.WriteLine(result);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {error}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
