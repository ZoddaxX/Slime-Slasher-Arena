using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool paused = false;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            pause();
        }
    }

    // Update is called once per frame
    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
        
}

