using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    bool isMuted = false;
    
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "AudioManager";
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    public void ToggleMute(bool mute = false)
    {
        if (mute || !isMuted)
        {
            AudioListener.volume = 0f;
            isMuted = true;
        }
        else
        {
            AudioListener.volume = 1f;
            isMuted = false;
        }
    }
}
