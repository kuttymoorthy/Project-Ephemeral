//This class handles the player score system in the game

using DG.Tweening;
using TMPro;
using UnityEngine;

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
        public void CheckCombo(TextMeshProUGUI  animObj)
        {
            _comboStreak++;
            if (_comboStreak >= ComboThreshold)
            {
                // We can add visual effects for combos
                animObj.transform.DOScale(1.0f * 1.5f, 0.5f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        animObj.transform.DOScale(1, 0.5f)
                            .SetEase(Ease.OutQuad);
                    });
            }
        }
    }
}