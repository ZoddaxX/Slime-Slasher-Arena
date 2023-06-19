using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void Load()
    {

    }

    public void Save()
    {
        
    }

    public void DeleteSave()
    {
        PlayerPrefs.SetInt("TutorialCompleted", 0);
    }
}
