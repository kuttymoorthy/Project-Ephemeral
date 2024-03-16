using UnityEngine;

//This class handles the player game state

namespace Ephemeral.ScriptsOnly.Scripts
{
    public class GameStateManager : IGameStateManager
    {
        public void SaveGameData(int winningCount, int missingCount, int gameSize, int gameScore)
        {
            PlayerPrefs.SetInt(GameConstants.WinningCountKey, winningCount);
            PlayerPrefs.SetInt(GameConstants.MissingCountKey, missingCount);
            PlayerPrefs.SetInt(GameConstants.GameSizeKey, gameSize);
            PlayerPrefs.SetInt(GameConstants.GameScoreKey, gameScore);
            PlayerPrefs.Save();
        }
        
        public void LoadGameData(out int winningCount, out int missingCount, out int gameSize, out int gameScore)
        {
            // Currently we are loading only Game Layout Size for the player
            winningCount = PlayerPrefs.GetInt(GameConstants.WinningCountKey, 0);  
            missingCount = PlayerPrefs.GetInt(GameConstants.MissingCountKey, 0);  
            gameSize = PlayerPrefs.GetInt(GameConstants.GameSizeKey, GameConstants.GameLayoutMinSize);
            gameScore = PlayerPrefs.GetInt(GameConstants.GameScoreKey, 0);
        }
        
        public void ResetGameData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}