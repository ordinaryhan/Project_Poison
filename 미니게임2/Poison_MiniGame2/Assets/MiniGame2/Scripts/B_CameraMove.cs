using UnityEngine;

public class B_CameraMove : MonoBehaviour {

    private Transform ThisTransform;
    public B_UIManager UIM;
    public B_UIManager.enemyMode mode = B_UIManager.enemyMode.normal;
    public Transform target;
    public float smoothSpeed = 0.125f;
    private Rigidbody2D ThisBody;
    float x, y;
    bool switchA = false, switchB = false;
    Vector3 UP = new Vector3(0, 11.5f, -25);

    private void Awake()
    {
        ThisTransform = GetComponent<Transform>();
        ThisBody = GetComponent<Rigidbody2D>();
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
        x = target.position.x;
        y = target.position.y;

        // UpTogether 모드로 변하는 순간 1회에 한해 작동
        if (mode == B_UIManager.enemyMode.UpTogether && !switchB)
        {
            if (!switchA)
            {
                if (ThisTransform.position == UP)
                {
                    ThisBody.velocity = new Vector2(0, 0);
                    Invoke("SetSwitchA", 0.5f);
                }
                else
                    ThisBody.velocity = (UP - ThisTransform.position) * 3f;
            }
            else
            {
                if (y <= 0)
                {
                    y = 0;
                }
                else if (y >= 13.58f)
                {
                    y = 13.58f;
                }
                if (x <= -1)
                {
                    x = -1;
                }
                else if (x >= 1)
                {
                    x = 1;
                }
                Vector3 targetXY = new Vector3(x, y, -25);
                if (ThisTransform.position == targetXY)
                {
                    ThisBody.velocity = new Vector2(0, 0);
                    switchB = true;
                }
                else
                    ThisBody.velocity = (targetXY - ThisTransform.position) * 3f;
            }
        }
        else
        {
            if (y <= 0)
            {
                y = 0;
            }
            else if (y >= 13.58f)
            {
                y = 13.58f;
            }
            if (x <= -1)
            {
                x = -1;
            }
            else if (x >= 1)
            {
                x = 1;
            }
            ThisTransform.position = new Vector3(x, y, -25);
        }
    }

    private void SetSwitchA()
    {
        switchA = true;
    }

}
