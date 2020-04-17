using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
  
    public void LoadGameOver()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        QuitGame();
    }
}
