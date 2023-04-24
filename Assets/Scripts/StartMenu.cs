using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame(int level)
    {
        int index = SceneManager.GetActiveScene().buildIndex + level;
        SceneManager.LoadScene(index);
    }

    public void GameOver(int index)
    {
        SceneManager.LoadScene(index);
    }
}
