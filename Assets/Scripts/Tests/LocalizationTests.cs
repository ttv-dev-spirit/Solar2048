using System.Linq;
using NUnit.Framework;
using Solar2048.Localization;

namespace Tests
{
    [TestFixture]
    public class LocalizationTests
    {
        [Test]
        public void WhenSelectLocale_AndOtherLocaleWasSelectedInitially_ThenSelectedLocaleIsActive()
        {
            // Arrange.
            var localizationControllerUnderTest = new LocalizationController();
            Language languageToSelect = localizationControllerUnderTest.AvailableLanguages.First(language =>
                localizationControllerUnderTest.SelectedLanguage != language);

            // Act.
            localizationControllerUnderTest.SelectLanguage(languageToSelect);

            // Assert.
            Assert.IsTrue(localizationControllerUnderTest.SelectedLanguage == languageToSelect);
        }
    }
}