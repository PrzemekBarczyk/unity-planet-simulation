using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallManager : MonoBehaviour
{
    [SerializeField] UnityEvent OnBallCreated;
    [SerializeField] UnityEvent OnBallDestroyed;

    List<Ball> balls = new List<Ball>();

    public void AddBall(Ball newBall)
	{
        balls.Add(newBall);
        OnBallCreated.Invoke();
	}

    public void DeleteBall(Ball ball)
	{
        balls.Remove(ball);
        Destroy(ball);
        OnBallDestroyed.Invoke();
	}

    public List<Ball> GetBalls()
	{
        return balls;
	}
}
