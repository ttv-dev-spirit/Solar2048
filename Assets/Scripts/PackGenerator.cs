#nullable enable
namespace Solar2048
{
    public sealed class PackGenerator : IPackGenerator
    {
        private readonly PackGeneratorSettings _settings;
        private readonly IBuildingsPackProvider _buildingsPackProvider;
        private readonly Pack.Factory _packFactory;

        public PackGenerator(PackGeneratorSettings settings, IBuildingsPackProvider buildingsPackProvider,
            Pack.Factory packFactory)
        {
            _settings = settings;
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