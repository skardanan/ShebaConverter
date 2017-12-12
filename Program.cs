using System;

namespace ShebaConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string sheba = ShebaUtility.ConvertAccountToSheba("xxxxxxxxxxxxx", "19");
            Console.WriteLine(sheba);
            Console.ReadLine();
        }
    }
}
