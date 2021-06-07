using UnityEngine;
using UnityEngine.UI;

public class BallCounter : MonoBehaviour
{
    [SerializeField] Text counterText;

    int numberOfBalls = 0;

    void Start()
    {
        counterText = GetComponent<Text>();
    }

    public void IncreaseCounter()
	{
        numberOfBalls++;
        UpdateCounter();
	}

    public void DecreaseCounter()
	{
        numberOfBalls--;
        UpdateCounter();
	}

    public void UpdateCounter()
	{
        counterText.text = numberOfBalls.ToString();
	}
}
