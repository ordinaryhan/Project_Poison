﻿using System.Collections;
using UnityEngine;

public class B_CameraMove : MonoBehaviour {
    
    public B_UIManager UIM;
    public B_UIManager.enemyMode mode = B_UIManager.enemyMode.normal;
    public Transform target;
    public float smoothSpeed = 0.125f;
    private Transform ThisTransform;
    private Rigidbody2D ThisBody;
    private Camera ThisCamera;
    float x, y;
    bool switchA = false, switchB = false, switchC = false, switchD = false, switchE = false, switchF = false;
    Vector3 UP1 = new Vector3(0, 17.5f, -25), UP2 = new Vector3(0, 16.17f, -25), targetPosition;

    private void Awake()
    {
        ThisTransform = GetComponent<Transform>();
        ThisBody = GetComponent<Rigidbody2D>();
        ThisCamera = GetComponent<Camera>();
        SetupCamera();
    }

    // 화면 해상도
    private void SetupCamera()
    {
        Screen.SetResolution(1280, 800, false);
    }

// 모드를 확인한다. (모드는 UIManager가 관리한다.)
private void CheckMode()
    {
        mode = UIM.mode;
    }

    // 카메라 움직임 관련
    private void LateUpdate()
    {
        // 모드 체크
        CheckMode();

        // 플레이어 위치 체크
        targetPosition = target.position;
        x = targetPosition.x;
        y = targetPosition.y;

        // UpTogether 모드로 변하는 순간 1회에 한해 작동
        if (mode == B_UIManager.enemyMode.UpTogether && !switchB)
        {
            if (!switchA)
            {
                if (ThisTransform.position.y > UP1.y - 0.01f)
                {
                    ThisBody.velocity = Vector2.zero;
                    Invoke("SetSwitchA", 1f);
                }
                else
                    ThisBody.velocity = (UP1 - ThisTransform.position) * 3f;
            }
            else
            {
                SetXY();
                Vector3 targetXY = new Vector3(x, y, -25);
                if (ThisTransform.position == targetXY)
                {
                    ThisBody.velocity = Vector2.zero;
                    switchB = true;
                }
                else
                    ThisBody.velocity = (targetXY - ThisTransform.position) * 3f;
            }
        }
        // LastPang 모드로 변하는 순간 1회에 한해 작동
        else if (mode == B_UIManager.enemyMode.LastPang)
        {
            if (!switchC || !switchD)
            {
                // 카메라 위치 이동
                if (ThisTransform.position.y > UP2.y - 0.01f && ThisTransform.position.x > UP2.x - 0.01f && ThisTransform.position.x < UP2.x + 0.01f)
                {
                    ThisBody.velocity = Vector2.zero;
                    switchC = true;
                }
                else
                    ThisBody.velocity = (UP2 - ThisTransform.position) * 3f;
                // 카메라 크기 조정
                if (ThisCamera.orthographicSize >= 7.8f)
                {
                    switchD = true;
                    ThisCamera.orthographicSize = 7.8f;
                }
                else
                    ThisCamera.orthographicSize += 0.01f;
            }
        }
        // End 모드로 변하는 순간 작동
        else if (mode == B_UIManager.enemyMode.End)
        {
            if (!switchE || !switchF)
            {
                SetXY();
                targetPosition = new Vector3(x, y, -25);
                // 카메라 위치 이동
                if (ThisTransform.position == targetPosition)
                {
                    ThisBody.velocity = Vector2.zero;
                    switchE = true;
                }
                else
                {
                    ThisBody.velocity = (targetPosition - ThisTransform.position) * 3f;
                }
                // 카메라 크기 조정
                if (ThisCamera.orthographicSize <= 5.733f)
                {
                    switchF = true;
                    ThisCamera.orthographicSize = 5.733f;
                }
                else
                    ThisCamera.orthographicSize -= 0.01f;
            }
            else if(switchE && switchF)
            {
                SetXY();
                ThisTransform.position = new Vector3(x, y, -25);
            }
        }
        // normal모드 시 작동
        else
        {
            SetXY();
            ThisTransform.position = new Vector3(x, y, -25);
            ThisCamera.orthographicSize = 5.733f;
        }
    }

    private void SetXY()
    {
        if (y <= 0)
        {
            y = 0;
        }
        else if (y >= 19.55f)
        {
            y = 19.55f;
        }
        if (x <= -3.33f)
        {
            x = -3.33f;
        }
        else if (x >= 3.33f)
        {
            x = 3.33f;
        }
    }

    private void SetSwitchA()
    {
        switchA = true;
    }

}
