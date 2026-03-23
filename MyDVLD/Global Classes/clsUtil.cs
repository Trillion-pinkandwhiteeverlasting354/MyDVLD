using System;
using System.IO;
using System.Windows.Forms;

namespace MyDVLD.Classes
{
    public class clsUtil
    {

        public static string GenerateGUID()
        {
            // Generate a new GUID
            Guid NewGuid = Guid.NewGuid();

            // convert the GUID to a string
            return NewGuid.ToString();
        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            // Check if the folder exists
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }

            return true;
        }

        public static string ReplaceFileNameWithGUID(string SourceFile)
        {
            FileInfo fileInfo = new FileInfo(SourceFile);
            string exten = fileInfo.Extension; ;
            return GenerateGUID() + exten;
        }

        public static bool CopyImageToProjectImagesFolder(ref string SourceFile)
        {
            /*
                This funciton will copy the image to the project images foldr after renaming it
                with GUID with the same extention, then it will update the sourceFileName with the new name.
            */

            string DestinationFolder = @"C:\MyDVLD-People-Images\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string DestinationFile = DestinationFolder + ReplaceFileNameWithGUID(SourceFile);

            try
            {
                File.Copy(SourceFile, DestinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SourceFile = DestinationFile;

            return true;
        }


    }
}
