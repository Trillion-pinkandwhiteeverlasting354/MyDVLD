using System;

namespace MyDVLD.Classes
{
    public class clsFormat
    {

        public static string DateToShort(DateTime date)
        {
            return date.ToString("dd/MMM/yyyy");
        }

    }
}
