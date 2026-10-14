using UnityEngine;

public class GoalScript : MonoBehaviour
{
    bool _reached = false;


    private void OnTriggerEnter(Collider other)
    {
        if (_reached) return;
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance?.collectedPickups != GameManager.Instance?.totalPickups) return;
            _reached = true;
            GameManager.Instance?.OnGoalReached();
        }
    }
}
