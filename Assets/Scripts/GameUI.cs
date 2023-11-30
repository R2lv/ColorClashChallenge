using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
