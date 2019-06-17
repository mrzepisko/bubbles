namespace Bubbles.Core.Abstract {
    public interface IDataManager {
        void Save(PlayerData data);
        PlayerData Load();
    }
}