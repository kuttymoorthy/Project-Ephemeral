namespace Ephemeral.ScriptsOnly.Scripts
{
    public interface IGameStateManager
    {
        void SaveGameData(int winningCount, int missingCount, int gameSize, int gameScore);
        void LoadGameData(out int winningCount, out int missingCount, out int gameSize, out int gameScore);
        void ResetGameData();
    }
}