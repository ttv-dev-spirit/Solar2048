using NUnit.Framework;
using Solar2048.Cards;

namespace Tests
{
    [TestFixture]
    public class HandTests
    {
        [Test]
        public void WhenAddCard_AndHandIsEmpty_ThenOneCardInHand()
        {
            // Arrange.
            ICardContainer handUnderTest = Create.Hand();
            Card card = Create.Card();
            // Act.
            handUnderTest.AddCard(card);
            // Assert.
            Assert.IsTrue(handUnderTest.Cards.Count == 1);
        }

        [Test]
        public void WhenRemoveCard_AndOneCardInHand_ThenHandIsEmpty()
        {
            // Arrange.
            ICardContainer handUnderTest = Create.Hand();
            Card card = Create.Card();
            handUnderTest.AddCard(card);
            // Act.
            handUnderTest.RemoveCard(card);
            // Assert.
            Assert.IsTrue(handUnderTest.Cards.Count == 0);
        }
    }
}