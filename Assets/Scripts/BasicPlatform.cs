using UnityEngine;

public class BasicPlatform : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    [Tooltip("Seconds for movement between end points")]
    [Min(0.01f)] public float lerpTime = 5.0f;
    [Min(0.0f)] public float pauseAtStart = 1.0f;
    [Min(0.0f)] public float pauseAtEnd = 1.0f;

    bool goingForward = true;
    float elapsed;
    float pauseRemaining;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (startPos == null || endPos == null) return;

        transform.position = startPos.position;
        pauseRemaining = Mathf.Max(0f, pauseAtStart);
        Physics.SyncTransforms();

    }

    // Update is called once per frame
    void Update()
    {
        if (startPos == null || endPos == null) return;

        float remainingTime = Time.deltaTime;

        if (pauseRemaining > 0)
        {
            float pausedTime = Mathf.Min(pauseRemaining, remainingTime);
            pauseRemaining -= pausedTime;
            remainingTime -= pausedTime;
        }

        if (remainingTime < 0f) return;

        elapsed += remainingTime;

        float t = Mathf.Clamp01(elapsed/Mathf.Max(0.01f, lerpTime));
        Vector3 from = goingForward? startPos.position:endPos.position;
        Vector3 to = goingForward? endPos.position:startPos.position;

        //Finally here is the lerp
        transform.position = Vector3.Lerp(from,to,t);

        Physics.SyncTransforms();

        if(t >= 1)
        {
            pauseRemaining = Mathf.Max(0f, goingForward ? pauseAtEnd : pauseAtStart);
            goingForward = !goingForward;
            elapsed = 0f;
        }
    }
}
