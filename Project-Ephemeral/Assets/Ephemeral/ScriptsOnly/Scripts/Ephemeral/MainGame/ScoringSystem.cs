namespace Ephemeral.ScriptsOnly.Scripts
{
    public class ScoringSystem : IScoringSystem
    {
        private int _comboStreak; 
        private const int ComboThreshold = 2; 

        public int Score { get; set; }

        public ScoringSystem()
        {
            ResetScore();
            ResetComboStreak();
        }

        public void IncrementScore()
        {
            Score += (_comboStreak + 1) * 100;
        }

        public void ResetScore()
        {
            Score = 0;
        }

        public void ResetComboStreak()
        {
            _comboStreak = 0;
        }

        public void CheckCombo()
        { 
            _comboStreak++;
            if (_comboStreak >= ComboThreshold)
            {
                // We can add visual effects for combos
            }
        }
    }
}