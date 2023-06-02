using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarScript : MonoBehaviour
{
    public Slider slider;

    public void setHealth(float currentHealth, float maxHealth)
    {
        slider.value =  (int) Mathf.Round(20 * currentHealth / maxHealth);
        Debug.Log(slider.value);
    }
}
