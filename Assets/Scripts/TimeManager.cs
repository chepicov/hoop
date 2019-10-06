using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float slowdownFactor = 0.05f;

    private static TimeManager instance;

    public static TimeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TimeManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "TimeManager";
                    instance = obj.AddComponent<TimeManager>();
                }
            }
            return instance;
        }
    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void ReturnDefault()
    {
        Time.timeScale = 1.0f;
    }
}
