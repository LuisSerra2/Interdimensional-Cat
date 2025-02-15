using UnityEngine;

public class BG_FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float parallaxEffect = 0.5f; 

    private float startPosX;

    void Start()
    {
        startPosX = transform.position.x;
    }

    void Update()
    {
        float distance = player.position.x * parallaxEffect;
        transform.position = new Vector3(startPosX + distance, transform.position.y, transform.position.z);
    }
}
