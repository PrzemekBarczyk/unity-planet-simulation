using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] float explosionMinForce = 10f;
    [SerializeField] float explosionMaxForce = 15f;

    public const float G = 6.67259f;

    bool attract = true;
    bool reverseGravity;

    BallManager ballManager;
    BallSpawner ballSpawner;

    Rigidbody myRigidbody;
    SphereCollider myCollider;

	void Awake()
	{
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<SphereCollider>();
    }

	void Start()
    {
        ballManager = GameObject.Find("Ball Manager").GetComponent<BallManager>();
        ballSpawner = GameObject.Find("Ball Spawner").GetComponent<BallSpawner>();
    }

	void FixedUpdate()
	{
		foreach (var ball in ballManager.GetBalls())
		{
            if (ball != this && attract)
                CalculateAttraction(ball);
        }
	}

	public void CalculateAttraction(Ball ball)
	{
        Vector3 direction = ball.GetRigidbody().position - myRigidbody.position;
        if (reverseGravity)
            direction *= -1;

        float distance = direction.magnitude;
        if (distance == 0f)
            return;

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
        // select bigger or first (if equal) object from colliding pair
        if (transform.localScale.x >= otherBall.transform.localScale.x && enabled)
            otherBall.enabled = false;
        else 
            return;

        float newMass = myRigidbody.mass + otherBall.GetRigidbody().mass;
        if (newMass >= 50f)
            Explode();

        // merged ball gets position of bigger ball, or middle if balls have equal size
        Vector3 newPosition;
        if (transform.localScale.x == otherBall.transform.localScale.x)
            newPosition = (myRigidbody.position + otherBall.GetRigidbody().position) / 2f;
        else
            newPosition = myRigidbody.position;

        float newRadius = Mathf.Sqrt(Mathf.Pow(transform.localScale.x / 2f, 2f) + Mathf.Pow(otherBall.transform.localScale.x / 2f, 2f));
        Vector3 newScale = Vector3.one * newRadius * 2f;

        ballManager.DeleteBall(otherBall);

        transform.position = newPosition;
        myRigidbody.mass = newMass;
        transform.localScale = newScale;
    }

    void Explode()
	{
        myCollider.enabled = false;
        for (int i = 0; i < myRigidbody.mass; i++)
		{
            Ball newBall = Instantiate(ballSpawner.GetBallPrefab(), myRigidbody.position, Quaternion.Euler(Vector3.zero));

            newBall.DisableCollision(0.5f);

            ballManager.AddBall(newBall);

			float randomForce = Random.Range(explosionMinForce, explosionMaxForce);
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

			newBall.GetRigidbody().AddForce(randomForce * randomDirection, ForceMode.Impulse);
		}
        ballManager.DeleteBall(this);
	}

    public void DisableCollision(float time)
	{
        StartCoroutine(DisableCollisionCoroutine(time));
	}

    IEnumerator DisableCollisionCoroutine(float time)
	{
        myCollider.enabled = false;
        attract = false;

        yield return new WaitForSeconds(time);

        myCollider.enabled = true;
        attract = true;
	}

    public void ReverseGravity(bool reverse)
	{
        reverseGravity = reverse;
	}
}
