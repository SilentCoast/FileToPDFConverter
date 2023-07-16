using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;

namespace GetConvertedPDFs
{
    public static class TiffToPDFConverter
    {
        /// <summary>
        /// Converts Image type files(such as .tif .png .jpg) to .pdf; It does not delete old file after conversion
        /// </summary>
        /// <param name="filePath">File to convert</param>
        /// <returns>
        /// The path of newly created pdf file or same string if given file is already .pdf; And null if conversion is failed
        /// </returns>
        public static string GetPDFPath(string filePath)
        {
            FileInfo fileInfo= new FileInfo(filePath);
            if (fileInfo.Extension == ".pdf")
            {
                return filePath;
            }

            var pdf = ConvertFileToPDF(fileInfo);
            if (pdf != null)
            {
                return pdf;
            }
            return null;
        }
        /// <summary>
        /// Converts Image type files(such as .tif .png .jpg) to .pdf; It does not delete old file after conversion
        /// </summary>
        /// <param name="fileInfo"> File to convert</param>
        /// <returns>Full Path of the new created pdf file or null if conversion is failed</returns>
        private static string ConvertFileToPDF(FileInfo fileInfo)
        {
            try
            {
                Image MyImage = Image.FromFile(fileInfo.FullName);
                PdfDocument doc = new PdfDocument();

                for (int PageIndex = 0; PageIndex < MyImage.GetFrameCount(FrameDimension.Page); PageIndex++)
                {
                    MyImage.SelectActiveFrame(FrameDimension.Page, PageIndex);

                    MemoryStream strm = new MemoryStream();
                    MyImage.Save(strm, ImageFormat.Tiff);

                    XImage img = XImage.FromStream(strm);

                    var page = new PdfPage();

                    if (img.PointWidth > img.PointHeight)
                    {
                        page.Orientation = PageOrientation.Landscape;
                    }
                    else
                    {
                        page.Orientation = PageOrientation.Portrait;
                    }
                    page.Width = img.PointWidth;
                    page.Height = img.PointHeight;
                    doc.Pages.Add(page);

                    XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[PageIndex]);

                    xgr.DrawImage(img, 0, 0);
                }

                string finalFileName = fileInfo.DirectoryName + "\\" + Path.GetFileNameWithoutExtension(fileInfo.Name)+".pdf";
                doc.Save(finalFileName);
                doc.Close();
                MyImage.Dispose();

                return finalFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static List<FileInfo> GetConvertedPDFs(string folderPathFull)
        {
            List<FileInfo> files = new List<FileInfo>();

            DirectoryInfo directoryInfo = new DirectoryInfo(folderPathFull);
            FileInfo[] fileInfos = directoryInfo.GetFiles("*tif", SearchOption.AllDirectories);
            if (fileInfos.Length == 0)
            {
                MessageBox.Show("Файлы с таким расширением не найдены");
            }
            foreach (FileInfo fileInfo in fileInfos)
            {
                ConvertFileToPDF(fileInfo);
                files.Add(new FileInfo(folderPathFull + "\\" + Path.GetFileNameWithoutExtension(fileInfo.FullName) + ".pdf"));
            }

            return files;
        }
        public static List<FileInfo> GetPDFs(string folderPathFull)
        {
            List<FileInfo> files = new List<FileInfo>();

            DirectoryInfo directoryInfo = new DirectoryInfo(folderPathFull);
            FileInfo[] fileInfos = directoryInfo.GetFiles("*pdf", SearchOption.AllDirectories);
            if (fileInfos.Length == 0)
            {
                MessageBox.Show("Файлы с таким расширением не найдены");
            }
            foreach (FileInfo fileInfo in fileInfos)
            {
                files.Add(new FileInfo(folderPathFull + "\\" + Path.GetFileNameWithoutExtension(fileInfo.FullName) + ".pdf"));
            }

            return files;
        }
        public static void OpenFileWithSystemApp(string filePath)
        {
            System.Diagnostics.Process.Start(filePath);
        }
    }
}
