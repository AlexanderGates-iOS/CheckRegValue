using System;
using Microsoft.Win32;
using System.Threading;

internal class CheckRegValue
{
    private static void Main()
    {
        const string registryPath = @"SOFTWARE\WOW6432Node\WRData\Status";
        const string valueName = "ActiveScans";

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
                            Console.WriteLine("Value is 0, not scanning.");
                            break; // Exit the loop to move to the next step
                        }
                        else if (intValue == 1)
                        {
                            Console.WriteLine("Value is 1, scanning...");
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
