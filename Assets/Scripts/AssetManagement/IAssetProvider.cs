using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Solar2048.AssetManagement
{
    public interface IAssetProvider
    {
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        void CleanUp();
    }
}