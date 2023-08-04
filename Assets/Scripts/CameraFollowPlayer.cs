using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform playerTr;
    [SerializeField] Vector3 offset;
    [SerializeField] float followSpeed = 10.0f, lookSpeed = 10.0f;
    // Start is called before the first frame update
    void LookAtTarget()
    {
        Vector3 lookDirection = playerTr.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.fixedDeltaTime);

    }

    void Move()
    {
        Vector3 targetPos = playerTr.position + playerTr.forward * offset.z +
                            playerTr.right * offset.x +
                            playerTr.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtTarget();
        Move();
    }
}
