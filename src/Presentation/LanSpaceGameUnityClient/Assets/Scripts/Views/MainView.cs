using UnityEngine;

namespace Assets.Scripts.Views
{
    public class MainView : MonoBehaviour
    {
        public void OnExitGameClick()
        {
            Debug.Log("Quit game");

            // Call to save any game data here
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

    }
}
