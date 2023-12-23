using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("refs")]
    public Transform playerTransform;

    [Header("control")]
    public Vector3 posFollow;
    public float followDamp = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        if (playerTransform == null) {
            playerTransform = FindObjectOfType<PlayerController>().transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        posFollow = Vector2.Lerp(posFollow, playerTransform.position, followDamp);

        transform.position = new Vector3(posFollow.x, posFollow.y, transform.position.z);
    }
}
