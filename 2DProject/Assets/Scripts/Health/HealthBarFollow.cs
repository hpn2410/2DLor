using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform target; // Reference to the enemy's transform
    public Vector3 offset; // Offset to adjust the position above the enemy

    private void LateUpdate()
    {
        if (target != null)
        {
            // Set the position of the health bar to be above the enemy
            transform.position = target.position + offset;
        }
    }
}
