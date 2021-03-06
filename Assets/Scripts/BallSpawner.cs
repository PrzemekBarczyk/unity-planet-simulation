using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] Ball ballPrefab;

    [Header("Spawn distance")]
    [SerializeField] float minDistance = 10f;
    [SerializeField] float maxDistance = 150f;

    IEnumerator co;

    BallManager ballManager;
    Transform ballsParent;

    void Start()
    {
        ballManager = GameObject.Find("Ball Manager").GetComponent<BallManager>();
        ballsParent = GameObject.Find("Balls").transform;

        co = SpawnBall();
        StartCoroutine(co);
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

    public void StopSpawn()
	{
        StopCoroutine(co);
	}

    Vector3 RandomPosition()
	{
        return Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Random.Range(minDistance, maxDistance)));
    }

    Quaternion RandomRotation()
	{
        return Quaternion.Euler(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)));
	}

    public Ball GetBallPrefab()
	{
        return ballPrefab;
	}
}
