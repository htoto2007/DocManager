using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VitCardPropsValue;
using VitTypeCard;
using static VitCardPropsValue.ClassCardPropsValue;

namespace VitFileCard
{
    public class ClassFileCard
    {
        public struct CardCollection
        {
            public string typeCardName;
            public CardPropsValueCollection[] cardPeopsValueCollections;
        }

        

        public void create(CardCollection cardCollection)
        {
            FormFileCard formFileCard = new FormFileCard();
            if (formFileCard.ShowDialog() != DialogResult.OK) return;

            ClassTypeCard classTypeCard = new ClassTypeCard();
            classTypeCard.add(cardCollection.typeCardName);
            ClassCardPropsValue classCardPropsValue = new ClassCardPropsValue();
            foreach (CardPropsValueCollection cardPeopsValueCollection in cardCollection.cardPeopsValueCollections) {
                classCardPropsValue.createValue(cardPeopsValueCollection.idCardProps, cardPeopsValueCollection.value, cardPeopsValueCollection.filePath);
            }
        }

        private CardCollection DataFormToCardCollection(FormFileCard formFileCard)
        {
            CardCollection cardCollection = new CardCollection();
            cardCollection.typeCardName = formFileCard.comboBoxTypeCard.Text;
            cardCollection.cardPeopsValueCollections[0].



            return cardCollection;
        }
    }
}
