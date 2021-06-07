using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallManager : MonoBehaviour
{
    [SerializeField] UnityEvent OnBallCreated;
    [SerializeField] UnityEvent OnBallDestroyed;

    List<GameObject> balls = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBall(GameObject newBall)
	{
        balls.Add(newBall);
        OnBallCreated.Invoke();
	}

    public void DeleteBall(GameObject ball)
	{
        balls.Remove(ball);
        Destroy(ball);
        OnBallDestroyed.Invoke();
	}
}
