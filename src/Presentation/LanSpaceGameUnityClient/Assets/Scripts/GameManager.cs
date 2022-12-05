using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;

    public static GameManager singleton;

    void Awake()
    {
        singleton = this;
    }

    public float FPS { get; private set; }

    void Update()
    {
        FPS = (1f / Time.unscaledDeltaTime);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(350, 10, 200, 20), FPS.ToString());

        if (score == 3)
        {
            if (GUI.Button(new Rect(10, 10, 200, 20), "YOU WIN!"))
            {
                // TODO: Корабли переходят на сцену либо сцена не загружается
                //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                //Application.LoadLevel(0);
                UnityEngine.Networking.NetworkManager.singleton.ServerChangeScene("Main");
            }
        }
        else
        {
            GUI.Label(new Rect(10, 10, 200, 20), "Score: " + score);
        }

    }

}
