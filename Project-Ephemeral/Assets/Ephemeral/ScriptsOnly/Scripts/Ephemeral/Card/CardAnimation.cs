using System.Collections;
using UnityEngine;

namespace Ephemeral.ScriptsOnly.Scripts
{
    public partial class Card : MonoBehaviour
    {
        // Call fade animation
        public void Inactive()
        {
            StartCoroutine(Fade());
        }

        // Play fade animation by changing alpha of img's color
        private IEnumerator Fade()
        {
            var rate = GameConstants.CardFixedDuration / GameConstants.CardFadeDuration;
            var time = 0.0f;
            var targetColor = Color.clear;

            while (time < GameConstants.CardFixedDuration )
            {
                time += Time.deltaTime * rate;
                img.color = Color.Lerp(img.color, targetColor, time);
                yield return null;
            }
        }

        // Set card to be active color
        public void Active()
        {
            if (img)
            {
                img.color = Color.white;
            }
        }
    }
}