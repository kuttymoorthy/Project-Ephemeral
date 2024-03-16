using System.Collections;
using UnityEngine;

// This class handles the player interaction on the card

namespace Ephemeral.ScriptsOnly.Scripts
{
    public partial class Card : MonoBehaviour
    {
        // Card onclick event
        public void CardBtn()
        {
            if (_flipped || _turning || !MainGameController.Instance.CanClick()) return;
            Flip();
            StartCoroutine(DelayedSelectionEvent());
        }
        // Inform main game manager card is selected with a slight delay
        private IEnumerator DelayedSelectionEvent()
        {
            yield return new WaitForSeconds(GameConstants.GameWaitForSeconds);
            MainGameController.Instance.CardClicked(_spriteID, _id);
        }
    }
}