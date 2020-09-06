using UnityEngine;

public class CoinUIAnimation : MonoBehaviour
{
    public AnimationCurve Curve;

    private float currentTime;
    private float totalTime;
    private bool IsRunAnimation;

    private float scaleX;
    private float scaleY;
    private float scaleZ;

    void Start()
    {
        totalTime = Curve.keys[Curve.keys.Length - 1].time;
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    void Update()
    {
        RunAnimation();
    }

    public void StartAnimation()
    {
        IsRunAnimation = true;
        print("Start");
    }

    private void RunAnimation()
    {
        if (IsRunAnimation) {
            print("Run");
            var value = Curve.Evaluate(currentTime);
            transform.localScale = new Vector3(scaleX * value, scaleY, scaleZ * value);

            currentTime += Time.deltaTime;

            if (currentTime >= totalTime) {
                currentTime = 0f;
                IsRunAnimation = false;
                print("Stop");
            }
        }
    }
}
