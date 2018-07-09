using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_EnemyMovement : MonoBehaviour {

    private string ThisName;
    // 바라보는 방향
    public enum FaceDirection { FaceLeft = -1, FaceRight = 1 };
    public FaceDirection Facing = FaceDirection.FaceRight;
    // rigidbody에 대한 참조
    private Rigidbody2D ThisBody = null;
    // transform에 대한 참조
    private Transform ThisTransform = null;
    // 속도 변수
    public float Speed = 10f;
    public float bulletSpeed = 500;
    [SerializeField]
    public float[] bulletTimeOut;
    // 공격/방어를 위한
    public Transform[] barrel;
    public Rigidbody2D[] bullet;
    public B_PlayerControl target;
    private bool enemy1_CanAttack = true;
    private bool enemy2_CanAttack = true;
    // 애니메이션을 위한
    private Animator myAnimator;
    // 이동 관련
    [SerializeField]
    public Transform[] rotationCenter;
    public float rotationRadius = 2f, angularSpeed = 2f;
    float posX, posY, angle = -1f, temp0 = 0, temp1 = 0;
    int i = 0;
    bool flag = true;
    float digree;
    Vector3 position0 = new Vector3(-8.48f, -1.224f, 0f);

    // 체력 체크
    public static float Health
    {
        get
        {
            return _Health;
        }

        set
        {
            _Health = value;
            // 캐릭터가 죽은 경우 게임을 끝낸다.
            if (_Health <= 0)
            {
                Clear();
            }
        }
    }

    [SerializeField]
    private static float _Health = 5f;  // 체력 5칸
    // Use this for initialization
    private void Awake()
    {
        // 이 객체의 정보들을 담는다.
        ThisBody = GetComponent<Rigidbody2D>();
        ThisTransform = GetComponent<Transform>();
        myAnimator = GetComponent<Animator>();
        ThisName = ThisTransform.tag;
        // enemy1의 초기위치 설정(Floor_middle 중앙에서 시작)
        if (ThisName.Equals("enemy1"))
            ThisTransform.position = position0;
        
    }

    // 캐릭터 방향을 바꾼다.
    private void FlipDirection()
    {
        Facing = (FaceDirection)((int)Facing * -1f);
        Vector3 LocalScale = ThisTransform.localScale;
        LocalScale.x *= -1f;
        ThisTransform.localScale = LocalScale;
    }

    // Update is called once per frame
    void Update () {

        // enemy1의 이동 기본 패턴
        if (ThisName.Equals("enemy1"))
        {
            if (flag)
            {
                posX = rotationCenter[i].position.x - Mathf.Cos(angle) * rotationRadius;
                posY = rotationCenter[i].position.y - Mathf.Sin(angle) * rotationRadius;
                transform.position = new Vector2(posX, posY);
                angle = angle + Time.deltaTime * angularSpeed;
            }
        }
        // enemy2의 이동 기본 패턴
        else if (ThisName.Equals("enemy2"))
        {
            if (flag)
            {
                posX = rotationCenter[0].position.x - Mathf.Cos(angle) * rotationRadius;
                posY = rotationCenter[0].position.y - Mathf.Sin(angle) * rotationRadius;
                transform.position = new Vector2(posX, posY);
                angle = angle + Time.deltaTime * angularSpeed;
            }

            if (enemy2_CanAttack)
            {
                enemy2_CanAttack = false;
                StartCoroutine(Enemy2_Attack());
            }
        }

        // target(플레이어)가 있는 방향으로 고개를 돌림
        if (target.GetComponent<Transform>().position.x - transform.position.x > 0)
        {
            if (Facing == FaceDirection.FaceLeft)
                FlipDirection();
        }
        else
        {
            if (Facing == FaceDirection.FaceRight)
                FlipDirection();
        }

    }

    // enemy2 공격 함수
    IEnumerator Enemy2_Attack()
    {
        // barrel 방향으로 말탄환을 날린다.
        Vector2 barrelDir;
        Vector2 Dir;
        flag = false;
        // 상하좌우+대각선4방향으로 말탄환을 날린다.
        for (int k = 0; k < 8; k++)
        {
            barrelDir = barrel[k].position;
            Dir = barrelDir - (Vector2)transform.position;
            var letterBullets = Instantiate(bullet[k], barrel[k].position, barrel[k].rotation);
            yield return new WaitForSecondsRealtime(0.1f);
            letterBullets.GetComponent<B_DestroyInTime>().MoveBullet(Dir, bulletSpeed, 1.5f - 0.025f*k);
        }
        myAnimator.SetTrigger("Attack");
        yield return new WaitForSecondsRealtime(1f);
        Invoke("setFlag", 0.8f);
        Invoke("ActivateBullets", bulletTimeOut[Random.Range(0, bulletTimeOut.Length)]);
    }

    // enemy2 공격 딜레이
    private void ActivateBullets()
    {
        enemy2_CanAttack = true;
    }

    // enemy1의 이동 경로 조작. 네 개의 원의 교차점에서는 잠시 멈춰 공격을 날리고 다른 원을 따라 움직이는 것을 반복한다.
    public void Change_i()
    {
        flag = false;
        i = (i + 1) % 4;
        angle -= 1.47f;
        Enemy1_Attack();
        Invoke("setFlag", 0.8f);
    }

    private void setFlag()
    {
        flag = true;
    }

    // enemy1 공격 함수
    public void Enemy1_Attack()
    {
        if (!enemy1_CanAttack)
            return;
        
        enemy1_CanAttack = false;
        myAnimator.SetTrigger("Attack");
        new WaitForSeconds(2f);
        // target(플레이어) 방향으로 말탄환을 날린다.
        Vector2 targetDir = target.GetComponent<Transform>().position;
        Vector2 Dir = targetDir - (Vector2) transform.position;
        if (Facing == FaceDirection.FaceLeft)
            digree = Mathf.Atan2(barrel[0].position.y - targetDir.y, barrel[0].position.x - targetDir.x) * 180f / Mathf.PI;
        else
            digree = - Mathf.Atan2(barrel[0].position.y - targetDir.y, barrel[0].position.x - targetDir.x) * 180f / Mathf.PI + 180;
        barrel[0].Rotate(0, 0, digree);
        var letterBullet = Instantiate(bullet[Random.Range(0, bullet.Length)], barrel[0].position, barrel[0].rotation);
        letterBullet.AddForce(Dir.normalized * bulletSpeed);
        // 공격 딜레이
        Invoke("ActivateBullet", bulletTimeOut[0]);
    }

    // enemy1 공격 딜레이. 제한 시간이 지나야 CanJump 변수를 활성화한다.
    private void ActivateBullet()
    {
        enemy1_CanAttack = true;
        barrel[0].Rotate(0, 0, -digree);
    }

    // 적이 클리어된 경우
    static void Clear()
    {
        
    }

}
