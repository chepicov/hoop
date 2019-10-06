using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonHandler : MonoBehaviour
{
    [SerializeField] Text text;

    Image img;
    Animator animator;
    bool isClicked;

    void Awake()
    {
        img = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        isClicked = false;
    }

    public void OnClick()
    {
        if (isClicked) {
            return;
        }
        GameManager.Instance.LoadLevel();
        isClicked = true;

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        animator.SetTrigger("fadeOut");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
