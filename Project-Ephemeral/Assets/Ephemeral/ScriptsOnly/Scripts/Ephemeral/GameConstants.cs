
// This abstract class handles the game constants 
namespace Ephemeral.ScriptsOnly.Scripts
{
    public abstract class GameConstants
    {
        public const int GameLayoutMinSize = 2;
        
        public const int GameLayoutMaxSize = 16;
        
        public const float CardFadeDuration = 2.0f;
        
        public const float CardFixedDuration = 1.0f;
        
        public const float CardFlipDuration = 0.25f;
        
        public const float GameWaitForSeconds = 0.5f;
        
        public const string WinningCountKey = "WinningCount";
        
        public const string MissingCountKey = "MissingCount";
        
        public const string GameSizeKey = "GameSize";
        
        public const string GameScoreKey = "GameScore";
        
        public const string WinMsgTxt = "Well Done!";
    }
}