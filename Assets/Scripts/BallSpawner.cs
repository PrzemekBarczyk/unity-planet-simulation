using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] Ball ballPrefab;

    [Header("Spawn distance")]
    [SerializeField] float minDistance = 5f;
    [SerializeField] float maxDistance = 50f;

    BallManager ballManager;
    Transform ballsParent;

    void Start()
    {
        ballManager = GameObject.Find("Ball Manager").GetComponent<BallManager>();
        ballsParent = GameObject.Find("Balls").transform;
        StartCoroutine(SpawnBall());
    }

    public IEnumerator SpawnBall()
	{
        while (true)
		{
            yield return new WaitForSeconds(0.25f);
            var newBall = Instantiate(ballPrefab, RandomPosition(), RandomRotation(), ballsParent);
            ballManager.AddBall(newBall);
        }
	}

    Vector3 RandomPosition()
	{
        return Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Random.Range(minDistance, maxDistance)));
    }

    Quaternion RandomRotation()
	{
        return Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)));
	}
}
