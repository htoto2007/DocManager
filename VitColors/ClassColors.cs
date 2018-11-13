using System.Drawing;

namespace VitColors
{
    public class ClassColors
    {
        public ColorsCollection getCollection()
        {
            ColorsCollection colorsCollection = new ColorsCollection
            {
                blue = Color.FromArgb(175, 207, 251),
                gray = Color.FromArgb(214, 228, 255),
                secondaryA0 = Color.FromArgb(255, 204, 0),
                secondaryA1 = Color.FromArgb(255, 204, 153),
                secondaryB0 = Color.FromArgb(255, 0, 0),
                secondaryB1 = Color.FromArgb(255, 153, 153)
            };

            return colorsCollection;
        }

        public struct ColorsCollection
        {
            public Color blue;
            public Color gray;
            public Color secondaryA0;
            public Color secondaryA1;
            public Color secondaryB0;
            public Color secondaryB1;
        }
    }
}