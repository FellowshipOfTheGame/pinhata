using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowComponent : MonoBehaviour
{
    [SerializeField] private float cameraMoveSpeed = 10f;
    [SerializeField] private Vector3 cameraOffset = Vector3.zero;

    private Func<Vector3> GetCameraFollowPositionFunc;

    public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc() + cameraOffset;
        cameraFollowPosition.y = transform.position.y;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);

        if(distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            //Overshot target
            if(distanceAfterMoving > distance)
                newCameraPosition = cameraFollowPosition;

            transform.position = newCameraPosition;
        }
    }
}
