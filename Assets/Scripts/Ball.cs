using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] Ball ballPrefab;

    public const float G = 6.67259f;

    BallManager ballManager;
    Rigidbody myRigidbody;

    void Start()
    {
        ballManager = GameObject.Find("Ball Manager").GetComponent<BallManager>();
        myRigidbody = GetComponent<Rigidbody>();
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

	void OnCollisionEnter(Collision collision)
	{
        Merge(collision.gameObject.GetComponent<Ball>());
	}

    void Merge(Ball otherBall)
	{
        // select first object from colliding pair
        if (!enabled) return;
        otherBall.enabled = false;
        if (!enabled) return;

        // merged ball gets position of bigger ball, or middle if balls have equal size
        Vector3 newPosition;
        if (transform.localScale.x == otherBall.transform.localScale.x)
            newPosition = (myRigidbody.position + otherBall.GetRigidbody().position) / 2f;
        else
            newPosition = transform.localScale.x > otherBall.transform.localScale.x ? myRigidbody.position : otherBall.GetRigidbody().position;

        float newMass = myRigidbody.mass + otherBall.GetRigidbody().mass;
        float newRadius = Mathf.Sqrt(Mathf.Pow(transform.localScale.x / 2f, 2f) + Mathf.Pow(otherBall.transform.localScale.x / 2f, 2f));
        Vector3 newScale = Vector3.one * newRadius * 2f;

        ballManager.DeleteBall(otherBall);

        transform.position = newPosition;
        myRigidbody.mass = newMass;
        transform.localScale = newScale;
	}
}
