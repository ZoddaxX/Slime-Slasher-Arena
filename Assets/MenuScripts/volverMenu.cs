using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class volverMenu : MonoBehaviour
{
    [SerializeField] private int mainMenuScene;

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}