using System.Windows.Forms;
using VitCardPropsValue;
using VitFiles;
using VitTypeCard;
using vitCardProps;
using static VitCardPropsValue.ClassCardPropValue;

namespace VitFileCard
{
    /// <summary>
    /// Обеспечивает работу со всеми элементами карточки файла 
    /// </summary>
    public class ClassFileCard
    {
        public struct CardCollection
        {
            public string typeCardName;
            public CardPropsValueCollection[] cardPeopsValueCollections;
        }

        public CardCollection[] getInfoByFilePath(string filePath)
        {
            ClassFiles classFiles = new ClassFiles();
            ClassCardPropValue classCardPropsValue = new ClassCardPropValue();
            ClassTypeCard classTypeCard = new ClassTypeCard();
            ClassCardProps classCardProps = new ClassCardProps();

            // узнаем свойства файла
            ClassFiles.FileCollection fileCollection = classFiles.getInfoByFilePath(filePath);
            // получаем список свойств карточки файла
            CardPropsValueCollection[] cardPropsValueCollections = classCardPropsValue.getByIdFile(fileCollection.id);
            // объявляем коллекцию карточки
            CardCollection[] cardCollections = new CardCollection[cardPropsValueCollections.GetLength(0)];
            // записываем свойства карточки в коллкцию
            for(int i = 0; i < cardCollections.GetLength(0); i++)
            {
                
                //cardCollections[i].typeCardName = 
            }
            return cardCollections;
        }

        public void create(CardCollection cardCollection, string filePath)
        {
            FormFileCard formFileCard = new FormFileCard();
            if (formFileCard.ShowDialog() != DialogResult.OK) return;

            ClassTypeCard classTypeCard = new ClassTypeCard();
            classTypeCard.add(cardCollection.typeCardName);
            ClassCardPropValue classCardPropsValue = new ClassCardPropValue();
            foreach (CardPropsValueCollection cardPeopsValueCollection in cardCollection.cardPeopsValueCollections) {
                classCardPropsValue.createValue(cardPeopsValueCollection.idCardProp, cardPeopsValueCollection.value, cardPeopsValueCollection.idFile);
            }
        }

        private CardCollection DataFormToCardCollection(FormFileCard formFileCard)
        {
            CardCollection cardCollection = new CardCollection();
            cardCollection.typeCardName = formFileCard.comboBoxTypeCard.Text;
            //cardCollection.cardPeopsValueCollections[0].



            return cardCollection;
        }
    }
}
