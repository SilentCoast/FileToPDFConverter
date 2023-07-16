using GetConvertedPDFs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSearch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TiffToPDFConverter.GetPDFPath("C:\\Users\\Andre\\OneDrive\\Рабочий стол\\LoodsmanServer\\pdf00.pdf"));
            Console.WriteLine(TiffToPDFConverter.GetPDFPath("C:\\Users\\Andre\\OneDrive\\Рабочий стол\\LoodsmanServer\\tif11.tif"));
            Console.WriteLine(TiffToPDFConverter.GetPDFPath("C:\\Users\\Andre\\OneDrive\\Рабочий стол\\LoodsmanServer\\tif22.Tiff"));
            Console.WriteLine(TiffToPDFConverter.GetPDFPath("C:\\Users\\Andre\\OneDrive\\Рабочий стол\\LoodsmanServer\\tif33.png"));
            Console.WriteLine(TiffToPDFConverter.GetPDFPath("C:\\Users\\Andre\\OneDrive\\Рабочий стол\\LoodsmanServer\\tif44.jpg"));
            
            Console.ReadKey();
        }
    }
}
