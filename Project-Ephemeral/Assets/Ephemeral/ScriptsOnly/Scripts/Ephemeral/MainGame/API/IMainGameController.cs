using UnityEngine;

//This interface defines methods for managing game play

namespace Ephemeral.ScriptsOnly.Scripts
{
    public interface IMainGameController
    {
        void StartCardGame();
        void SetGameSize();
        Sprite GetSprite(int spriteId);
        Sprite CardBack();
        bool CanClick();
        void CardClicked(int spriteId, int cardId);
        void GiveUp();
        void DisplayWinInfo(bool boolValue);
    }
}