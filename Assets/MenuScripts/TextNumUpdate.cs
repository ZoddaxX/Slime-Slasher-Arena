using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextNumUpdate : MonoBehaviour
{
    public TextMeshProUGUI numberText;

    public void SetNumberText(float value)
    {
        numberText.text = value.ToString();
    }

}
