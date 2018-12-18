using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitCardPropsValue;
using VitTypeCard;

namespace VitFileCard
{
    public class ClassFileCard
    {
        public struct CardCollection
        {
            public string name;
            public CardPeopsValueCollection[] cardPeopsValueCollections;
        }

        public struct CardPeopsValueCollection
        {
            public int idCardProps;
            public int idTypeValue;
            public string value;
            public string filePath;
        }

        public void create(CardCollection cardCollection)
        {
            ClassTypeCard classTypeCard = new ClassTypeCard();
            classTypeCard.add(cardCollection.name);
            ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
            foreach (CardPeopsValueCollection cardPeopsValueCollection in cardCollection.cardPeopsValueCollections) {
                classCardPropsValue.createValue(cardPeopsValueCollection.idCardProps, cardPeopsValueCollection.value, cardPeopsValueCollection.filePath);
            }
        }
    }
}
