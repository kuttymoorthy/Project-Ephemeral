using System.Collections;
using UnityEngine;

// This class handles the flip animation of the card

namespace Ephemeral.ScriptsOnly.Scripts
{
    public partial class Card : MonoBehaviour
    {
        // Flip card animation coroutine
        private IEnumerator FlipCard(float time, bool changeSprite)
        {
            var rotation = transform.rotation;
            var startRotation = rotation;
            var endRotation = rotation * Quaternion.Euler(new Vector3(0, 90, 0));
            var rate = GameConstants.CardFixedDuration / time;
            var targetTime = 0.0f;

            while (targetTime < GameConstants.CardFixedDuration)
            {
                targetTime += Time.deltaTime * rate;
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, targetTime);
                yield return null;
            }

            // Change sprite and flip another 90 degrees
            if (changeSprite)
            {
                _flipped = !_flipped;
                ChangeSprite();
                StartCoroutine(FlipCard(time, false));
            }
            else
            {
                _turning = false;
            }
        }
        // Perform a 180-degree flip
        public void Flip()
        {
            _turning = true;
            AudioPlayer.Instance.PlayAudio(0);
            StartCoroutine(FlipCard(GameConstants.CardFlipDuration, true));
        }
        // Toggle front/back sprite
        private void ChangeSprite()
        {
            if (_spriteID == -1 || img == null) return;
            img.sprite = _flipped
                ? MainGameController.Instance.GetSprite(_spriteID)
                : MainGameController.Instance.CardBack();
        }
    }
}