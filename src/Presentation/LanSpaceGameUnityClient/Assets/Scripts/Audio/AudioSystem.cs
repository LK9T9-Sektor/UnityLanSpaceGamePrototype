using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class AudioSystem
    {
        #region Disgusting Singleton

        private AudioSystem() { }

        private static readonly AudioSystem _instance = new AudioSystem();

        public static AudioSystem Singleton { get { return _instance; } }

        #endregion

        // TODO: Looks like an extension method
        public void PlaySoundAtPosition(AudioClip audioSource, Vector3 position)
        {
            AudioSource.PlayClipAtPoint(audioSource, position);
        }

    }
}
