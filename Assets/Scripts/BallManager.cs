using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallManager : MonoBehaviour
{
    [SerializeField] UnityEvent OnBallCreated;
    [SerializeField] UnityEvent OnBallDestroyed;

    List<Ball> balls = new List<Ball>();

    BallSpawner ballSpawner;

	void Start()
	{
        ballSpawner = GameObject.Find("Ball Spawner").GetComponent<BallSpawner>();
	}

	public void AddBall(Ball newBall)
	{
        balls.Add(newBall);
        OnBallCreated.Invoke();

        if (balls.Count >= 250)
        {
            ReverseBallsGravity();
            ballSpawner.StopSpawn();
        }
	}

    public void DeleteBall(Ball ball)
	{
        balls.Remove(ball);
        Destroy(ball.gameObject);
        OnBallDestroyed.Invoke();
	}

    void ReverseBallsGravity()
	{
        foreach (Ball ball in balls)
            ball.ReverseGravity(true);
	}

    public List<Ball> GetBalls()
	{
        return balls;
	}
}
