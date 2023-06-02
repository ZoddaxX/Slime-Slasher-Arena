using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nuevoIntento : MonoBehaviour
{
    [SerializeField] private int startScene;

    public void TryAgain()
    {
        SceneManager.LoadScene(startScene);
    }
}
