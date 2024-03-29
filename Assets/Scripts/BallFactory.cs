﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    [SerializeField] Transform ballPrefab;

    private List<Goal> balls = new List<Goal>();
    private static BallFactory instance;
    public static BallFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BallFactory>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "BallFactory";
                    instance = obj.AddComponent<BallFactory>();
                }
            }
            return instance;
        }
    }
    static int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        Goal.RunTrigger();
        StartCoroutine(SpawnBall(GameManager.ActiveLevel - 1, 10.0f));
    }

    IEnumerator SpawnBall(int count, float delay) {
        int i = 0;
        while (i++ < count) {
            yield return new WaitForSeconds(delay);
            CreateObject();
        }
    }

    void CreateObject()
    {
        Transform newObj = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        if (newObj.transform.GetChild(0) != null) {
            GameObject child = newObj.transform.GetChild(0).gameObject;
            Goal script = child.GetComponent<Goal>();
            balls.Add(script);
            script.ChangePosition();
        }
    }

    public void IncPoints()
    {
        points++;
        GameManager.Instance.ChangeScore(points);
    }

    public void Stop()
    {
        StopAllCoroutines();
        Goal.StopTrigger();
    }
}
