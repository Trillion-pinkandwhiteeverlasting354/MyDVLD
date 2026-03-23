using Microsoft.Win32;
using MyDVLD_Business;
using System;
using System.IO;
using System.Windows.Forms;

namespace MyDVLD.Classes
{
    internal static class clsGlobal
    {
        public static clsUser CurrentUser;

        /*  THIS USES A FILE TO STORE USERNAME AND PASSWORD */
        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            try
            {
                // This will get the current project directory folder.
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Define the path to the text file where you want to save the data
                string FilePath = CurrentDirectory + "\\data.txt";

                // In case the username is empty, delete the file
                if (string.IsNullOrEmpty(Username) && File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    return false;
                }

                // Concatonate username and passwrod withe seperator.
                string dataToSave = Username + "#//#" + Password;

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    // Write the data to the file
                    writer.WriteLine(dataToSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }


        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            // This will get the stored username and password and will return true if found and false if not found.
            try
            {
                // Gets the current project's directory
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string FilePath = CurrentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(FilePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        // Read data line by line until the end of the file
                        string Line;
                        while ((Line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(Line); // Output each line of data to the console
                            string[] result = Line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }


        /* USING WINDOWS REGISTRY */

        public static bool RememberUsernameToRegistry(string Username)
        {
            try
            {
                if (string.IsNullOrEmpty(Username))
                {
                    Registry.CurrentUser.DeleteSubKey(@"Software\DVLD", false);
                    return false;
                }

                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\DVLD"))
                {
                    key.SetValue("Username", Username, RegistryValueKind.String);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static bool GetStoredCredentialFromRegistry(ref string Username)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\DVLD"))
                {
                    if (key == null)
                        return false;

                    Username = key.GetValue("Username")?.ToString();

                    return !string.IsNullOrEmpty(Username);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static void ClearStoredUsername()
        {
            try
            {
                Registry.CurrentUser.DeleteSubKey(@"Software\DVLD", false);
            }
            catch { }
        }


    }
}
