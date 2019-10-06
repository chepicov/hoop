using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TapticPlugin;

public class GameManager : MonoBehaviour
{
    public const string LEVEL = "currentLevel";
    bool firstLoad = false;
    [SerializeField] Player player;
    [SerializeField] TimeManager timeManager;
    Rigidbody playerRigidbody;
    public static int ActiveLevel = -1;
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GameManager";
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player.isActive = false;
        playerRigidbody.isKinematic = true;
        ActiveLevel = PlayerPrefs.GetInt(LEVEL) - 1;
        firstLoad = true;
    }

    public void LoadLevel()
    {
        if (!firstLoad)
        {
            SceneManager.UnloadSceneAsync(SceneConstants.Levels[ActiveLevel].Id);
        }
        ActiveLevel = Mathf.Min(ActiveLevel + 1, SceneConstants.Levels.Length - 1);
        Debug.Log(ActiveLevel);
        SceneManager.LoadScene(SceneConstants.Levels[ActiveLevel].Id, LoadSceneMode.Additive);
        PlayerPrefs.SetInt(LEVEL, ActiveLevel);
        firstLoad = false;

        float delayTime = 0.2f;
        playerRigidbody.velocity = Vector3.zero;
        player.transform.position = new Vector3(0, 0, 0);
        player.transform.rotation = Quaternion.identity;
        StartCoroutine(StartPhysics(playerRigidbody, delayTime));
    }

    IEnumerator StartPhysics(Rigidbody rb, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneConstants.Levels[ActiveLevel].Id));
        playerRigidbody.isKinematic = false;
        player.isActive = true;
        TapticManager.Impact(ImpactFeedback.Medium);
        if (ActiveLevel == 0) {
            UIController.Instance.ShowTutorial();
            yield return new WaitForSeconds(2f);
        }
        UIController.Instance.ShowPlay();
    }

    public void ChangeScore(int points)
    {
        int winPoints = SceneConstants.Levels[ActiveLevel].WinPoints;
        float fillAmount = (float) points / winPoints;
        UIController.Instance.SetScore(points.ToString(), fillAmount);
        if (points >= winPoints) {
            Win();
        }
    }

    void Win()
    {
        for (int i = 0; i < 3; i++)
        {
            TapticManager.Impact(ImpactFeedback.Medium);
        }
        BallFactory.Instance.Stop();
        UIController.Instance.ShowWin();
        player.isActive = false;
        timeManager.DoSlowMotion();
    }
}
