using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButtonHandler : MonoBehaviour
{
    Image img;
    bool isClicked = false;

    void Awake()
    {
        img = GetComponent<Image>();
    }

    private void Start() {
        AudioManager.Instance.ToggleMute(false);
    }


    public void OnClick()
    {
        isClicked = !isClicked;

        float animationTime = 0.5f;
 
        if (isClicked)
        {
            AudioManager.Instance.ToggleMute(false);
            StartCoroutine(FadeIn(animationTime));
        }
        else
        {
            AudioManager.Instance.ToggleMute(true);
            StartCoroutine(FadeOut(animationTime));
        }
    }

    IEnumerator FadeOut(float delayTime)
    {
        for (float i = delayTime; i >= 0.3f * delayTime; i -= Time.deltaTime)
        {
            Color tempColor = img.color;
            tempColor.a = i / delayTime;
            img.color = tempColor;

            yield return null;
        }
    }

    IEnumerator FadeIn(float delayTime)
    {
        for (float i = 0.3f * delayTime; i <= delayTime; i += Time.deltaTime)
        {
            Color tempColor = img.color;
            tempColor.a = i / delayTime;
            img.color = tempColor;

            yield return null;
        }
    }
}
