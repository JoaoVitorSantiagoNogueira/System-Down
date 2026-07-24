using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Path path;

    public float speed = 2f;

    public float initialOffset = 0f;

    void Update()
    {
        if (path == null || path.TotalLength <= 0)
            return;

        float distance = initialOffset + speed * Time.time;

        transform.position = path.GetPosition(distance);
    }
    
}