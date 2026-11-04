
using UnityEngine;

public class ReactivePlatform : MonoBehaviour
{
    public GameObject obj;
    public Transform startPos;
    public Transform endPos;

    [Min(0.01f)] public float lerpTime = 5.0f;
    [Min(0.0f)] public float pauseTime = 1.0f;
    [Min(0.0f)] public float returnDelay = 3.0f;
    public bool isActive;

    public Transform MovingTransform => obj != null ? obj.transform : null;

    bool goingForward = true;
    float currentLerpTime;
    float pauseRemaining;
    bool hasRider;
    float emptyTime;
    bool waitingForRider;
    bool returningHome;
    Vector3 returnFrom;
    float returnElapsed;
    float returnDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(startPos != null)
        {
            MovingTransform.position = startPos.position;
            Physics.SyncTransforms();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startPos == null || endPos == null) return;

        if(waitingForRider && !hasRider)
        {
            emptyTime += Time.deltaTime;
            if (emptyTime >= returnDelay) 
            {
                BeginReturnHome();
            }
        }

        if (returningHome)
        {
            returnElapsed += Time.deltaTime;
            float returnT = Mathf.Clamp01(returnElapsed / returnDuration);

            MovingTransform.position = Vector3.Lerp(returnFrom, startPos.position, returnT);
            Physics.SyncTransforms();

            if (returnT >= 1f)
            {
                returningHome = false;
                isActive = false;
                goingForward = true;
                currentLerpTime = 0f;
                pauseRemaining = 0f;
            }
            return;
        }

        float remainingTime = Time.deltaTime;
        if(pauseRemaining > 0f)
        {
            float pausedTime = Mathf.Min(pauseRemaining, remainingTime);
            pauseRemaining -= pausedTime;
            remainingTime -= pausedTime;
        }

        if (remainingTime <= 0f) return;

        //Move the platform
        currentLerpTime += remainingTime;
        float t = Mathf.Clamp01(currentLerpTime / Mathf.Max(0.01f, lerpTime));
        Vector3 from = goingForward ? startPos.position : endPos.position;
        Vector3 to = goingForward ? endPos.position : startPos.position;
        MovingTransform.position = Vector3.Lerp (from, to, t);
        Physics.SyncTransforms();

        if(t >= 1f)
        {
            goingForward = !goingForward;
            currentLerpTime = 0f;
            pauseRemaining = Mathf.Max(0f, pauseTime);
        }
    }

    public void ActivatePlatform()
    {
        isActive = true;
    }

    public void DeActivatePlatform()
    {
        isActive = false;
    }

    public void PlayerBoarded()
    {
        hasRider = true;
        waitingForRider = false;
        emptyTime = 0f;

        if (returningHome)
        {
            float fullDistance = Vector3.Distance(startPos.position, endPos.position);
            float progress = fullDistance > 0.0f ? Vector3.Distance(startPos.position, MovingTransform.position) / fullDistance : 0f;
            goingForward = true;
            currentLerpTime = Mathf.Clamp01(progress) * Mathf.Max(0.01f, lerpTime);
            pauseRemaining = 0f;
            returningHome = false;
        }
        ActivatePlatform();
    }

    public void PlayerLeft()
    {
        hasRider = false;
        waitingForRider = true;
        emptyTime = 0f;
    }

    //End for today;

    private void BeginReturnHome()
    {
        waitingForRider = false;
        returningHome = true;
        returnFrom = MovingTransform.position;
        returnElapsed = 0.0f;

        float fullDistance = Vector3.Distance(startPos.position,endPos.position);
        float fraction = fullDistance > 0.0f ? Vector3.Distance(returnFrom, startPos.position)/fullDistance : 0.0f;
        returnDuration = Mathf.Max(0.01f, lerpTime * fraction);
    }
}
