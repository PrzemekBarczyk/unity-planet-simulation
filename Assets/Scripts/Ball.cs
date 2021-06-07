using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public const float G = 6.67259f;

    BallManager ballManager;
    Rigidbody myRigidbody;

    void Start()
    {
        ballManager = GameObject.Find("Ball Manager").GetComponent<BallManager>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void FixedUpdate()
	{
		foreach (var ball in ballManager.GetBalls())
		{
            if (ball != this)
                CalculateAttraction(ball);
        }
	}

	public void CalculateAttraction(Ball ball)
	{
        Vector3 direction = ball.GetRigidbody().position - myRigidbody.position;
        float distance = direction.magnitude;

        Vector3 force = (myRigidbody.mass * ball.GetRigidbody().mass) / Mathf.Pow(distance, 2f) * G * direction.normalized;

        myRigidbody.AddForce(force);
	}

    public Rigidbody GetRigidbody()
	{
        return myRigidbody;
	}
}
