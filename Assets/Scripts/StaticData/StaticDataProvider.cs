#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using ModestTree;
using Solar2048.Buildings;
using Solar2048.Cards;
using Solar2048.Packs;
using Solar2048.Score;
using Solar2048.UI;
using UnityEngine;

namespace Solar2048.StaticData
{
    [UsedImplicitly]
    public sealed class StaticDataProvider : IBuildingSettingsProvider
    {
        private const string SCORE_SETTINGS_PATH = "Score/score_settings";
        private const string PACK_GENERATOR_SETTINGS_PATH = "Pack/pack_generator_settings";
        private const string PACK_BUYING_SETTINGS_PATH = "Pack/pack_buying_settings";
        private const string UI_MANAGER_SETTINGS_PATH = "UI/ui_manager_settings";
        private const string STARTING_HAND_CONFIG_PATH = "Hand/starting_hand_config";
        private const string BUILDINGS_SETTINGS_PATH = "Buildings";

        private readonly StartingHandConfig _startingHandConfig;
        
        private Dictionary<BuildingType, BuildingSettings> _buildingSettings = new();

        public ScoreSettings ScoreSettings { get; }
        public PackGeneratorSettings PackGeneratorSettings { get; }
        public PackBuyingSettings PackBuyingSettings { get; }
        public UIManagerSettings UIManagerSettings { get; }

        // TODO (Stas): Not sure this is fine.
        public StaticDataProvider()
        {
            ScoreSettings = LoadScriptable<ScoreSettings>(SCORE_SETTINGS_PATH);
            PackGeneratorSettings = LoadScriptable<PackGeneratorSettings>(PACK_GENERATOR_SETTINGS_PATH);
            PackBuyingSettings = LoadScriptable<PackBuyingSettings>(PACK_BUYING_SETTINGS_PATH);
            UIManagerSettings = LoadScriptable<UIManagerSettings>(UI_MANAGER_SETTINGS_PATH);
            _startingHandConfig = LoadScriptable<StartingHandConfig>(STARTING_HAND_CONFIG_PATH);

            LoadBuildingsSettings();
        }

        private void LoadBuildingsSettings()
        {
            BuildingSettings[]? buildingsSettings = Resources.LoadAll<BuildingSettings>(BUILDINGS_SETTINGS_PATH);
            Assert.IsNotNull(buildingsSettings);
            Assert.IsNotEmpty(buildingsSettings);
            _buildingSettings = buildingsSettings.ToDictionary(setting => setting.BuildingType, setting => setting);
        }

        public BuildingSettings GetBuildingSettingsFor(BuildingType buildingType)
        {
            return _buildingSettings.TryGetValue(buildingType, out BuildingSettings? settings)
                ? settings
                : throw new Exception($"No building config found for {buildingType}");
        }

        public IEnumerable<BuildingType> GetStartingHand() => _startingHandConfig.StartingHand;

        private T LoadScriptable<T>(string path) where T : ScriptableObject
        {
            var result = Resources.Load<T>(path);
            Assert.IsNotNull(result);
            return result;
        }
    }
}