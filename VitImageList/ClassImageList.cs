using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace VitIcons
{
    public class ClassImageList
    {
        public ImageList imageList = new ImageList();




        public ClassImageList()
        {
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(24, 24);
            imageList.Images.Add("default_file", Properties.ResourceColorImage24.file_warning_40447_24);
            imageList.Images.Add("default_folder", Properties.ResourceColorImage24.folder_yellow_10921_24);
            imageList.Images.Add("default_folder_no_empty", Properties.ResourceColorImage.folder_black_documents_11000);
            imageList.Images.Add("root", Properties.ResourceColorImage.unlnown_file);

            Console.WriteLine("Обновление базы значков... ");
            foreach (string path in Directory.GetFiles(VitSettings.Properties.GeneralsSettings.Default.fileTypeIcons))
            {
                Image image = Image.FromFile(path);
                imageList.Images.Add(Path.GetFileNameWithoutExtension(path).TrimStart('.'), image);
                
            }

            
        }

        public string addIconFile(string pathToFile)
        {
            if (imageList.Images.ContainsKey(Path.GetExtension(pathToFile).Trim('.')))
            {
                return Path.GetExtension(pathToFile).Trim('.');
            }

            Icon icon = null;
            try
            {
                icon = Icon.ExtractAssociatedIcon(pathToFile);
            }
            catch (System.IO.FileNotFoundException)
            {
                return "icons8_document_48.png";
            }
            imageList.ImageSize = new Size(24, 24);
            imageList.Images.Add(Path.GetExtension(pathToFile).Trim('.'), icon);

            return Path.GetExtension(pathToFile).Trim('.');
        }
    }
}