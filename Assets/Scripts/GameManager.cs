using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlaneController planeController;

    public MissleSpawner missleSpawner;

    public void ExitGame()
    {        
        Application.Quit();
    }

    public void RestartGame()
    {
        planeController.startTime = 0f;

        planeController.coinScore = 0f;

        planeController.DisableControls();

        missleSpawner.initialMissileCount = 1;
        
        SceneManager.LoadScene("Assignment");


    }
}
