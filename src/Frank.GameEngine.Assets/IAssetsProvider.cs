namespace Frank.GameEngine.Assets;

public interface IAssetsProvider<T>
{
    IEnumerable<string> SearchForAsset(string assetName);

    bool TryGetAsset(string assetName, out T asset);

    T GetAsset(string resourceName);
}