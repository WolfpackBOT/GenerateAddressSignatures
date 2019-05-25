using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace GenerateAddressSignatures
{
    class Program
    {
        static string cliPath = "";
        static string signatureFilename = "WalletSignatures.txt";
        static int buffer = 0;
        static bool canContinue = true;
        static StringBuilder sb = new StringBuilder();

        static void Write(string s, bool error = false)
        {
            if (error)
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(s);
        }

        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Write("Please provide the wolfcoin-cli path", true);
                Console.ReadLine();
            }
            else
            {
                cliPath = args[0];

                if (args.Length > 1)
                    signatureFilename = args[1];

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("");

                Write("Welcome to the Wolfcoin Multi-Address Registration Code Generator!");
                Write("Instructions:");
                Write("1. Run this application on the computer that has your wallet installed, and from the wallet's directory.");
                Write("2. Run and unlock the wallet.");
                Write("This is neccessary to generate the signatures needed to perform the verifications.");
                Write($"3. After this program completes, it will generate a text file called {signatureFilename} with the generated codes.");
                Write("Copy all of that text and paste into the verification window on the website.");
                Write("");
                Write("Ready to continue? (y = Yes, n = No)");

                var result = Console.ReadKey();
                if (result.KeyChar == 'y')
                {
                    Write("");
                    Write("Please wait, this can take a few minutes depending on how many addresses you have.");

                    if (File.Exists(signatureFilename))
                        File.Delete(signatureFilename);

                    // Create a new output file
                    File.AppendAllText(signatureFilename, "");

                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = cliPath,
                            Arguments = "listaddressbalances",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        }
                    };

                    process.Start();
                    var outputReader = process.StandardOutput;
                    var errorReader = process.StandardError;
                    string output = outputReader.ReadToEnd();
                    process.WaitForExit();

                    var arrayAddresses = JObject.Parse(output);
                    if (arrayAddresses != null && arrayAddresses.Count > 0)
                        Write($"{arrayAddresses.Count} addresses found.");

                    int i = 0;
                    foreach (var oAddress in arrayAddresses)
                    {
                        i++;

                        if (canContinue)
                        {
                            buffer++;

                            SignMessage(oAddress.Key);

                            if (canContinue)
                            {
                                Console.Write("\r   Progress: {0} of {1}   ", i, arrayAddresses.Count);
                            }
                        }

                        // Write to the output file
                        if (buffer > 4)
                        {
                            File.AppendAllText(signatureFilename, sb.ToString());
                            sb.Clear();
                            buffer = 0;
                        }
                    }


                    if (canContinue)
                    {
                        // Write remaining buffer
                        if (buffer > 0)
                        {
                            File.AppendAllText(signatureFilename, sb.ToString());
                        }
                    }
                    else
                    {
                        Console.ReadLine();
                    }
                }
            }
        }

        // Using the signmessage RPC function, with the address as the message
        static void SignMessage(string address)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = cliPath,
                    Arguments = $"signmessage {address} \"" + address + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };
            process.Start();
            var outputReader = process.StandardOutput;
            var errorReader = process.StandardError;
            string output = outputReader.ReadToEnd();
            process.WaitForExit();

            if (String.IsNullOrWhiteSpace(output))
            {
                Write("The wallet is not unlocked, cannot continue...", true);
                canContinue = false;
            }

            // Write to buffer
            if (canContinue)
            {
                sb.Append(address + '|' + output);
            }
        }
    }
}
