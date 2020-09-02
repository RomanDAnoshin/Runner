using UnityEngine;
using UnityEngine.Events;

public class CoinTrigger : MonoBehaviour
{
    public GameObject Owner;
    public UnityEvent TriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character")) {
            Destroy(Owner);
            TriggerEvent.Invoke();
        }
    }
}
