using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using WIA;

namespace VitScaner
{
    public class ClassScaner
    {
        private WIA.CommonDialog wiaDiag = new WIA.CommonDialog();
        private List<Image> images = new List<Image>();
        private WIA.ICommonDialog wiaCommonDialog = new WIA.CommonDialog();
        private WIA.Item item = null;

        public Image Start()
        {
            WIA.ImageFile wiaImage = null;

            wiaImage = wiaDiag.ShowAcquireImage(
                WiaDeviceType.ScannerDeviceType,
                WiaImageIntent.TextIntent,
                WiaImageBias.MaximizeQuality,
                ImageFormat.wiaFormatTIFF,
                true,
                true,
                true
            );
            return null;

            if (wiaImage != null)
            {
                WIA.Vector vector = wiaImage.FileData;
                Image image = Image.FromStream(new MemoryStream((byte[])vector.get_BinaryData()));
                wiaImage.SaveFile("d:\\www.jpg");
                return image;
            }
            return null;
        }

        public struct ImageFormat
        {
            public const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
            public const string wiaFormatPNG = "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}";
            public const string wiaFormatGIF = "{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}";
            public const string wiaFormatJPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
            public const string wiaFormatTIFF = "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}";
        }

        private const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
        private const string WIA_DEVICE_PROPERTY_PAGES_ID = "3096";
        private const string WIA_SCAN_BRIGHTNESS_PERCENTS = "6154";
        private const string WIA_SCAN_CONTRAST_PERCENTS = "6155";
        private const string WIA_SCAN_COLOR_MODE = "6146";

        private class WIA_DPS_DOCUMENT_HANDLING_SELECT
        {
            public const uint FEEDER = 0x00000001;
            public const uint FLATBED = 0x00000002;
        }

        private class WIA_DPS_DOCUMENT_HANDLING_STATUS
        {
            public const uint FEED_READY = 0x00000001;
        }

        private class WIA_PROPERTIES
        {
            public const uint WIA_RESERVED_FOR_NEW_PROPS = 1024;
            public const uint WIA_DIP_FIRST = 2;
            public const uint WIA_DPA_FIRST = WIA_DIP_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            public const uint WIA_DPC_FIRST = WIA_DPA_FIRST + WIA_RESERVED_FOR_NEW_PROPS;

            //
            // Scanner only device properties (DPS)
            //
            public const uint WIA_DPS_FIRST = WIA_DPC_FIRST + WIA_RESERVED_FOR_NEW_PROPS;

            public const uint WIA_DPS_DOCUMENT_HANDLING_STATUS = WIA_DPS_FIRST + 13;
            public const uint WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_FIRST + 14;
        }

        //public void SetProperty(Property property, int value)
        //{
        //    IProperty x = (IProperty)property;
        //    Object val = value;
        //    x.set_Value(ref val);
        //}

        /// <summary>
        /// Use scanner to scan an image (with user selecting the scanner from a dialog).
        /// </summary>
        /// <returns>Scanned images.</returns>
        public List<Image> Scan()
        {
            WIA.ICommonDialog dialog = new WIA.CommonDialog();
            WIA.Device device = dialog.ShowSelectDevice(WIA.WiaDeviceType.UnspecifiedDeviceType, true, false);

            if (device != null)
            {
                return Scan(device.DeviceID, 9);
            }
            else
            {
                throw new Exception("You must select a device for scanning.");
            }
        }

        /// <summary>
        /// Use scanner to scan an image (scanner is selected by its unique id).
        /// </summary>
        /// <param name="scannerName"></param>
        /// <returns>Scanned images.</returns>
        public List<Image> Scan(string scannerId, int pages)
        {
            //List<Image> images = new List<Image>();
            bool hasMorePages = true;
            int numbrPages = pages;
            // select the correct scanner using the provided scannerId parameter
            WIA.DeviceManager manager = new WIA.DeviceManager();
            WIA.Device device = null;

            foreach (WIA.DeviceInfo info in manager.DeviceInfos)
            {
                if (info.DeviceID == scannerId)
                {
                    // connect to scanner
                    device = info.Connect();
                    break;
                }
            }
            // device was not found
            if (device == null)
            {
                // enumerate available devices
                string availableDevices = "";
                foreach (WIA.DeviceInfo info in manager.DeviceInfos)
                {
                    availableDevices += info.DeviceID + "\n";
                }

                // show error with available devices
                throw new Exception("The device with provided ID could not be found. Available Devices:\n" + availableDevices);
            }

            SetWIAProperty(device.Properties, WIA_DEVICE_PROPERTY_PAGES_ID, 1);

            item = device.Items[1] as WIA.Item;
            int dpi = 300;
            int inch = 254;
            double dpiMM = dpi / inch;
            double widthFormat = 210;
            double heightFormat = 297;
            int widthPixel = (int)(widthFormat * dpiMM);
            int heightPixel = (int)(heightFormat * dpiMM);
            AdjustScannerSettings(item, dpi, 0, 0, widthPixel, heightPixel, 0, 0, 2);

            while (hasMorePages)
            {
                Thread thread = new Thread(goScan);
                thread.Start();
                thread.Join();

                /*
                finally
                {
                    //item = null;
                    //determine if there are any more pages waiting
                    WIA.Property documentHandlingSelect = null;
                    WIA.Property documentHandlingStatus = null;
                    foreach (WIA.Property prop in device.Properties)
                    {
                        if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT)
                            documentHandlingSelect = prop;
                        if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS)
                            documentHandlingStatus = prop;
                    }
                    // assume there are no more pages
                    hasMorePages = false;
                    // may not exist on flatbed scanner but required for feeder
                    if (documentHandlingSelect != null)
                    {
                        // check for document feeder
                        if ((Convert.ToUInt32(documentHandlingSelect.get_Value()) & WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER) != 0)
                        {
                            hasMorePages = ((Convert.ToUInt32(documentHandlingStatus.get_Value()) & WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) != 0);
                        }
                    }
                }
                */
                numbrPages -= 1;
                if (numbrPages > 0)
                {
                    hasMorePages = true;
                }
                else
                {
                    hasMorePages = false;
                }
            }
            return images;
        }

        private void goScan()
        {
            try
            {
                WIA.ImageFile image = (WIA.ImageFile)wiaCommonDialog.ShowTransfer(item, wiaFormatBMP, false);
                List<WIA.ImageFile> imageList = new List<ImageFile>();

                WIA.Vector vector = image.FileData;
                Image img = Image.FromStream(new MemoryStream((byte[])vector.get_BinaryData()));
                if (img.Width < img.Height)
                {
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }

                images.Add(img);
                return;
            }
            catch (Exception)
            {
                //throw exc;
                return;
            }
        }

        /// <summary>
        /// Gets the list of available WIA devices.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDevices()
        {
            List<string> devices = new List<string>();
            WIA.DeviceManager manager = new WIA.DeviceManager();
            foreach (WIA.DeviceInfo info in manager.DeviceInfos)
            {
                devices.Add(info.DeviceID);
            }
            return devices;
        }

        private void SetWIAProperty(WIA.IProperties properties,
               object propName, object propValue)
        {
            WIA.Property prop = properties.get_Item(ref propName);
            try
            {
                prop.set_Value(ref propValue);
            }
            catch (Exception)
            {
            }
        }

        private void AdjustScannerSettings(IItem scannnerItem, int scanResolutionDPI, int scanStartLeftPixel, int scanStartTopPixel,
         int scanWidthPixels, int scanHeightPixels, int brightnessPercents, int contrastPercents, int colorMode)
        {
            const string WIA_SCAN_COLOR_MODE = "6146";
            const string WIA_HORIZONTAL_SCAN_RESOLUTION_DPI = "6147";
            const string WIA_VERTICAL_SCAN_RESOLUTION_DPI = "6148";
            const string WIA_HORIZONTAL_SCAN_START_PIXEL = "6149";
            const string WIA_VERTICAL_SCAN_START_PIXEL = "6150";
            const string WIA_HORIZONTAL_SCAN_SIZE_PIXELS = "6151";
            const string WIA_VERTICAL_SCAN_SIZE_PIXELS = "6152";
            const string WIA_SCAN_BRIGHTNESS_PERCENTS = "6154";
            const string WIA_SCAN_CONTRAST_PERCENTS = "6155";

            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_START_PIXEL, scanStartLeftPixel);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_START_PIXEL, scanStartTopPixel);
            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_SIZE_PIXELS, scanWidthPixels);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_SIZE_PIXELS, scanHeightPixels);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_BRIGHTNESS_PERCENTS, brightnessPercents);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_CONTRAST_PERCENTS, contrastPercents);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_COLOR_MODE, colorMode);
        }
    }

    public class scan2
    {
        public bool stop = false;

        public struct ImageFormat
        {
            public const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
            public const string wiaFormatPNG = "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}";
            public const string wiaFormatGIF = "{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}";
            public const string wiaFormatJPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
            public const string wiaFormatTIFF = "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}";
        }

        public List<Image> start()
        {
            List<Image> imageLost = new List<Image>();
            WIA.CommonDialog dialog = new WIA.CommonDialog();
            WIA.Device device = dialog.ShowSelectDevice(WIA.WiaDeviceType.ScannerDeviceType);
            WIA.Items items = dialog.ShowSelectItems(device);
            WIA.ImageFile image = null;
            dialog = new WIA.CommonDialog();
            foreach (WIA.Item item in items)
            {
                while (true)
                {

                    Console.WriteLine(device.Commands);
                    
                    try
                    {
                        
                        if(stop == true)
                        {
                            image = null;
                            stop = false;
                        }
                        else
                        {
                            image = (WIA.ImageFile)dialog.ShowTransfer(item, "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}", false);
                        }
                        if ((image != null) && (image.FileData != null))
                        {
                            WIA.Vector vector = image.FileData;
                            imageLost.Add(Image.FromStream(new MemoryStream((byte[])vector.get_BinaryData())));
                        }
                        
                    }
                    catch (System.Runtime.InteropServices.COMException)
                    {
                        return imageLost;
                    }
                    finally
                    {
                        if (image != null)
                        {
                            Marshal.FinalReleaseComObject(image);
                        }
                    }
                }
            }
            return imageLost;
        }
    }
}