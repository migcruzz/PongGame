using UnityEngine;
using UnityEngine.Events;

public class GoalController : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (onTriggerEnter != null && onTriggerEnter.GetPersistentEventCount() > 0)
            {
                onTriggerEnter.Invoke();
            }
            else
            {
                Debug.LogWarning("No listeners attached to onTriggerEnter in GoalController.");
            }
        }
    }
}
