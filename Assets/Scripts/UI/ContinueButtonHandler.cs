using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButtonHandler : MonoBehaviour
{
    public void OnClick()
    {
        TimeManager.Instance.ReturnDefault();
        GameManager.Instance.LoadLevel();
        UIController.Instance.ShowPlay();
    }
}
