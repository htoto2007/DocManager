using System.Drawing;

namespace VitColors
{
    public class ClassColors
    {
        public ColorsCollection getCollection()
        {
            ColorsCollection colorsCollection = new ColorsCollection
            {
                primary0 = Color.FromArgb(0, 204, 255),
                primary1 = Color.FromArgb(153, 255, 255),
                secondaryA0 = Color.FromArgb(255, 204, 0),
                secondaryA1 = Color.FromArgb(255, 204, 153),
                secondaryB0 = Color.FromArgb(255, 0, 0),
                secondaryB1 = Color.FromArgb(255, 153, 153)
            };

            return colorsCollection;
        }

        public struct ColorsCollection
        {
            public Color primary0;
            public Color primary1;
            public Color secondaryA0;
            public Color secondaryA1;
            public Color secondaryB0;
            public Color secondaryB1;
        }
    }
}