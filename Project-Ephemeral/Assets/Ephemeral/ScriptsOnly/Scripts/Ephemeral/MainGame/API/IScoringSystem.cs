//This interface defines methods for managing player score system

namespace Ephemeral.ScriptsOnly.Scripts
{
    public interface IScoringSystem
    {
        int Score { get; set; }
        void IncrementScore();
        void ResetScore();
        void ResetComboStreak();
        void CheckCombo();
    }
}