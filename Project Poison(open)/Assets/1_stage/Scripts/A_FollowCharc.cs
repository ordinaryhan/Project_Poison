﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_FollowCharc : MonoBehaviour {

    public Transform target;
    public float smoothTime = .15f;

    Vector3 velocity = Vector3.zero;

    //to restrict the movement of camera when camera gets out of the background
    public bool YMaxEnabled = false;
    public float YMaxValue = 0;
    public bool YMinEnabled = false;
    public float YMinValue = 0;

    public bool XMaxEnabled = false;
    public float XMaxValue = 0;
    public bool XMinEnabled = false;
    public float XMinValue = 0;

    private void Awake()
    {
        setupCamera();
    }

    // 화면 해상도
    private void setupCamera()
    {
        Screen.SetResolution(1280, 800, false);
    }

    // Update is called once per frame
    void FixedUpdate () {
        //know where character is placed
        Vector3 targetPos = target.position;

        if (YMinEnabled && YMaxEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, YMinValue, YMaxValue);
        else if (YMinEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, YMinValue, target.position.y);
        else if (YMaxEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, target.position.y, YMaxValue);

        if (XMinEnabled && XMaxEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, XMinValue, XMaxValue);
        else if (XMinEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, XMinValue, target.position.x);
        else if (XMaxEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, target.position.x, XMaxValue);

        //align the camera and target
        targetPos.z = transform.position.z;
        targetPos.y += 1.5f;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
	}
}
