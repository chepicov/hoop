using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TapticPlugin;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TimeManager timeManager;
    Rigidbody playerRigidbody;
    private static GameManager instance;
    public static int ActiveLevel = -1;

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
    }

    public void LoadLevel()
    {
        if (ActiveLevel > -1)
        {
            SceneManager.UnloadSceneAsync(SceneConstants.Levels[ActiveLevel].Id);
        }
        ActiveLevel = Mathf.Min(ActiveLevel + 1, SceneConstants.Levels.Length - 1);
        SceneManager.LoadScene(SceneConstants.Levels[ActiveLevel].Id, LoadSceneMode.Additive);

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
        TapticManager.Impact(ImpactFeedback.Medium);
        BallFactory.Instance.Stop();
        UIController.Instance.ShowWin();
        player.isActive = false;
        timeManager.DoSlowMotion();
    }
}
