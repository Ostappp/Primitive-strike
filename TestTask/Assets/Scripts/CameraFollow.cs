using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTransform;
    [SerializeField] private Vector3 cameraShift;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerTransform == null)
            PlayerTransform = FindAnyObjectByType<Player>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(PlayerTransform);
        transform.position = PlayerTransform.position + cameraShift;
    }
}
