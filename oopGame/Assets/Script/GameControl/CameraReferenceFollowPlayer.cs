using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReferenceFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 distanceFromPlayer = new Vector3(0,0,0);
    [SerializeField]
    GameObject playerRef;
    void Start()
    {
        distanceFromPlayer.x = playerRef.transform.position.x - this.transform.position.x;
        distanceFromPlayer.y = playerRef.transform.position.y - this.transform.position.y;
        distanceFromPlayer.z = playerRef.transform.position.z - this.transform.position.z;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = playerRef.transform.position;
        newPosition.x = newPosition.x - distanceFromPlayer.x;
        newPosition.y = newPosition.y - distanceFromPlayer.y;
        newPosition.z = newPosition.z - distanceFromPlayer.z;
        this.gameObject.transform.position = newPosition;
    }
}
