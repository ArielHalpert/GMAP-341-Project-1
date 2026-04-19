using UnityEngine;
using UnityEngine.SceneManagement;
public class LoseMenu : MonoBehaviour
{
    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }
}
