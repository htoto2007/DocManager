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
            imageList.ImageSize = new Size(24, 24);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.Images.Add("default_file", Properties.ResourceColorImage.icons8_document_48);
            imageList.Images.Add("default_folder", Properties.ResourceColorImage.icons8_folder_48);
            imageList.Images.Add("root", Properties.ResourceColorImage.icons8_tree_structure_40);

            foreach (string path in Directory.GetFiles(VitSettings.Properties.GeneralsSettings.Default.fileTypeIcons))
            {
                Image image = Image.FromFile(path);
                imageList.Images.Add(Path.GetFileNameWithoutExtension(path).TrimStart('.'), image);
                Console.WriteLine("Загружен значек: " + Path.GetFileNameWithoutExtension(path).TrimStart('.'));
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

            imageList.Images.Add(Path.GetExtension(pathToFile).Trim('.'), icon);

            return Path.GetExtension(pathToFile).Trim('.');
        }
    }
}