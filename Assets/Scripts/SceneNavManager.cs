using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneNavManager : MonoBehaviour
{
    public void GotoGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("Home");
    }
}
