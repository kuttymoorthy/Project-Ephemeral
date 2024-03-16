using System.Collections;
using UnityEngine;

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

        // Inform manager card is selected with a slight delay
        private IEnumerator DelayedSelectionEvent()
        {
            yield return new WaitForSeconds(GameConstants.GameWaitForSeconds);
            MainGameController.Instance.CardClicked(_spriteID, _id);
        }
    }
}