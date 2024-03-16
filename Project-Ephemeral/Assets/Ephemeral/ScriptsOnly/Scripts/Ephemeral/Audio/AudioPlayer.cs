using UnityEngine;

// This class handles audio playback functionality.

namespace Ephemeral.ScriptsOnly.Scripts
{
    public class AudioPlayer : MonoBehaviour
    {
        public static AudioPlayer Instance;
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip[] audioClips;

        private void Awake()
        {
            Instance = this;
        }
        public void PlayAudio(int id)
        {
            audioSource.PlayOneShot(audioClips[id]);
        }
        public void PlayAudio(int id, float vol)
        {
            audioSource.PlayOneShot(audioClips[id], vol);
        }
    }
}
