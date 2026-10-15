using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactDistance = 3.0f;
    public LayerMask interactLayerMask = ~0; //Default is interact will all layers
    public Vector3 playerInteractHeight = new Vector3(0, 1f, 0);

    public void OnInteract(InputValue value)
    {
        
        if (value.isPressed)
        {
            Debug.Log("Interact");
            Ray ray = new Ray(transform.position + playerInteractHeight, transform.forward);

            if(Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayerMask))
            {
                Debug.Log($"Interacted with: {hit.collider.name}");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + playerInteractHeight, transform.position + playerInteractHeight + transform.forward * interactDistance);
    }
}
