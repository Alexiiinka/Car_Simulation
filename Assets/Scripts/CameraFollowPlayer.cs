using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform playerTr;
    int offsetNum = 0;
    [SerializeField] Vector3[] offset = {new Vector3 (0.0f, 0.5f, -1.5f), 
                                            new Vector3 (0.0f, 0.35f, -0.9f),
                                            new Vector3 (0.0f, 0.3f, 2.0f)};
    [SerializeField] float followSpeed = 10.0f, lookSpeed = 10.0f;
    void LookAtTarget()
    {
        Vector3 lookDirection = playerTr.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.fixedDeltaTime);
    }

    void Move()
    {
        Vector3 targetPos = playerTr.position + playerTr.forward * offset[offsetNum].z +
                            playerTr.right * offset[offsetNum].x +
                            playerTr.up * offset[offsetNum].y;
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.fixedDeltaTime);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeView();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtTarget();
        Move();
    }

    void ChangeView()
    {
        offsetNum++;
        if (offsetNum % 3 == 0)
        {
            offsetNum = 0;
        }
        
    }
}
