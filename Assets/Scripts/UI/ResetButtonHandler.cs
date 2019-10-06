using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonHandler : MonoBehaviour
{
    public void OnClick()
    {
        PlayerPrefs.SetInt(GameManager.LEVEL, -1);
        GameManager.ActiveLevel = -1;
    }
}
