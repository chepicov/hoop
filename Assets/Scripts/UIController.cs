using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text ScoreText;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject PlayPanel;
    [SerializeField] GameObject StartPanel;
    [SerializeField] ParticleSystem particles;
    [SerializeField] Image progressBar;

    private static UIController instance;

    public static UIController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIController>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "UIController";
                    instance = obj.AddComponent<UIController>();
                }
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EnablePanel(StartPanel);
    }

    void EnablePanel(GameObject panel) {
        GameObject[] menuPanels = { WinPanel, PlayPanel, StartPanel };
        foreach (var menuPanel in menuPanels)
        {
            menuPanel.SetActive(false);
        }
        panel.SetActive(true);
    }

    public void SetScore(string score, float fillAmount) {
        ScoreText.text = score;
        progressBar.fillAmount = fillAmount;
    }

    public void ShowPlay()
    {
        EnablePanel(PlayPanel);
    }

    public void ShowWin()
    {
        ScoreText.text = "0";
        progressBar.fillAmount = 0f;
        EnablePanel(WinPanel);
        particles.Play();
    }
}
