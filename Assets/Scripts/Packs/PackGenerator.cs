#nullable enable
using Solar2048.StaticData;

namespace Solar2048.Packs
{
    public sealed class PackGenerator : IPackGenerator
    {
        private readonly PackGeneratorSettings _settings;
        private readonly IBuildingsPackProvider _buildingsPackProvider;
        private readonly Pack.Factory _packFactory;

        public PackGenerator(StaticDataProvider staticDataProvider, IBuildingsPackProvider buildingsPackProvider,
            Pack.Factory packFactory)
        {
            _settings = staticDataProvider.PackGeneratorSettings;
            _buildingsPackProvider = buildingsPackProvider;
            _packFactory = packFactory;
        }

        public Pack GetPack()
        {
            int cardsToGenerate = _settings.CardsInPack;
            var buildings = _buildingsPackProvider.GetBuildings(cardsToGenerate);
            return _packFactory.Create(buildings);
        }
    }
}