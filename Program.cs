using System;
using Microsoft.Win32;
using System.Threading;

internal class CheckRegValue
{
    private static void Main()
    {
        const string registryPath = @"SOFTWARE\...<registry path here>"; //change the path to the key you need to check
        const string valueName = "<The Registry Key>"; //put the key here

        while (true)
        {
            // Open the registry key
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath))
            {
                if (key != null)
                {
                    // Retrieve the value from the registry
                    object value = key.GetValue(valueName);
                    if (value != null && value is int)
                    {
                        int intValue = (int)value;

                        // Check the value
                        if (intValue == 0)
                        {
                            Console.WriteLine("Value is 0");
                            break; // Exit the loop to move to the next step
                        }
                        else if (intValue == 1)
                        {
                            Console.WriteLine("Value is 1");
                            // Wait for some time before rechecking
                            Thread.Sleep(1000); // Wait for 1 second
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Registry key not found.");
                    break;
                }
            }
        }

        // Continue with the next steps...
        Console.WriteLine("Dying...");
    }
}
