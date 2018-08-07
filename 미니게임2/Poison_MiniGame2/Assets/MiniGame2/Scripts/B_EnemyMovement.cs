using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_EnemyMovement : MonoBehaviour {

    private string ThisName;
    // 공격 패턴
    public B_UIManager.enemyMode mode = B_UIManager.enemyMode.normal;
    // 바라보는 방향
    public enum FaceDirection { FaceLeft = -1, FaceRight = 1 };
    public FaceDirection Facing = FaceDirection.FaceRight;
    // 이 객체에 대한 참조
    private Rigidbody2D ThisBody = null;
    private Transform ThisTransform = null;
    // 속도 변수
    public float Speed = 10f;
    public float bulletSpeed = 500;
    [SerializeField]
    public float[] bulletTimeOut;
    // 공격/방어를 위한
    public Transform[] barrel;
    public Transform[] bullet;
    private B_DestroyInTime[] Bullets;
    public float destroyTime = 1f;
    public B_PlayerControl target;
    private Transform targetTransform;
    private bool enemy1_CanAttack = true;
    private bool enemy2_CanAttack = true;
    // 애니메이션을 위한
    public Animator headAnimator, wingsAnimator, bodyAnimator;
    public GameObject waterball;
    // 이동 관련
    [SerializeField]
    public Transform[] rotationCenter;
    [SerializeField]
    public Transform[] Path_UpTogether;
    public float rotationRadius = 2f, angularSpeed = 2f;
    float posX, posY, angle = -1f, digree;
    int i = 0, page = 1, count = 0;
    bool flag = true, flip = true, clear = false, switchA = false;
    Vector2 targetDir, Dir;
    //  체력 5칸
    int Health;
    public B_UIManager UIM;
    // 클리어 관련
    public GameObject ClearEnemy, HitMessage, item1, item2;
    private SpriteRenderer HitMsg;
    public Transform barrelPoint;
    // 효과음
    public AudioClip enemyHit, modeChange;
    private AudioSource ThisAudio;

    // Use this for initialization
    private void Awake()
    {
        // 이 객체의 정보들을 담는다.
        ThisAudio = GetComponent<AudioSource>();
        HitMsg = HitMessage.GetComponent<SpriteRenderer>();
        Health = UIM.enemyMaxHP;
        ThisBody = GetComponent<Rigidbody2D>();
        ThisTransform = GetComponent<Transform>();
        ThisName = ThisTransform.tag;
        targetTransform = target.GetComponent<Transform>();
        Bullets = new B_DestroyInTime[bullet.Length];
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i].gameObject.SetActive(false);
            Bullets[i] = bullet[i].GetComponent<B_DestroyInTime>();
        }
        waterball.SetActive(false);
        ClearEnemy.SetActive(false);
        HitMessage.SetActive(false);
    }

    // 캐릭터 방향을 바꾼다.
    private void FlipDirection()
    {
        Facing = (FaceDirection)((int)Facing * -1f);
        Vector3 LocalScale1 = ThisTransform.localScale;
        LocalScale1.x *= -1f;
        ThisTransform.localScale = LocalScale1;
        Vector3 LocalScale2 = barrelPoint.localScale;
        LocalScale2.x *= -1f;
        barrelPoint.localScale = LocalScale2;
        HitMsg.flipX = flip;
        flip = !flip;
    }

    // 모드를 확인한다. (모드는 UIManager가 관리한다.)
    private void CheckMode()
    {
        mode = UIM.mode;
    }

    // Update is called once per frame
    private void FixedUpdate() {

        // 모드 확인
        CheckMode();
        // 모드에 따라 코드 진행
        switch (mode) {
            /* normal 모드 일 때 */
            case B_UIManager.enemyMode.normal:
                // enemy1의 이동 기본 패턴
                if (ThisName.Equals("enemy1"))
                {
                    if (flag)
                    {
                        posX = rotationCenter[i].position.x - Mathf.Cos(angle) * rotationRadius;
                        posY = rotationCenter[i].position.y - Mathf.Sin(angle) * rotationRadius;
                        ThisTransform.position = new Vector2(posX, posY);
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
                        ThisTransform.position = new Vector2(posX, posY);
                        angle = angle + Time.deltaTime * angularSpeed;
                    }

                    if (enemy2_CanAttack)
                    {
                        enemy2_CanAttack = false;
                        StartCoroutine(Enemy2_Attack1());
                    }
                }
                break;

            /* UpTogether 모드 일 때 */
            case B_UIManager.enemyMode.UpTogether:
                // enemy1의 UpTogether 모드 이동 패턴
                if (ThisName.Equals("enemy1"))
                {
                    if (page == 1)
                    {
                        if (!switchA)
                        {
                            switchA = true;
                            StartCoroutine(ModeMotion());
                        }
                        if (Path_UpTogether[0].position == ThisTransform.position)
                        {
                            ThisBody.velocity = new Vector2(0, 0);
                            page = 2;
                        }
                        else
                            ThisBody.velocity = (Path_UpTogether[0].position - ThisTransform.position) * 5f;
                    }
                    else if (UIM.enemy2_page2)
                    {
                        if (Path_UpTogether[1].position == ThisTransform.position)
                        {
                            page = 1;
                            ThisBody.velocity = new Vector2(0, 0);
                            new WaitForSecondsRealtime(3f);
                        }
                        else
                            ThisBody.velocity = (Path_UpTogether[1].position - ThisTransform.position) * 5f;
                    }

                    if (enemy1_CanAttack)
                        Enemy1_Attack();

                }
                // enemy2의 UpTogether 모드 이동 패턴
                else if (ThisName.Equals("enemy2"))
                {
                    if (page == 1)
                    {
                        if (!switchA)
                        {
                            switchA = true;
                            StartCoroutine(ModeMotion());
                        }
                        if (Path_UpTogether[0].position == ThisTransform.position)
                        {
                            page = 2;
                            ThisBody.velocity = new Vector2(0, 0);
                        }
                        else
                            ThisBody.velocity = (Path_UpTogether[0].position - ThisTransform.position) * 5f;
                    }
                    else if (page == 2)
                    {
                        if (count == 0)
                        {
                            count += 1;
                            StartCoroutine(Enemy2_Attack2());
                        }
                        UIM.enemy2_page2 = true;
                        if (Path_UpTogether[1].position == ThisTransform.position)
                        {
                            page = 1;
                            ThisBody.velocity = new Vector2(0, 0);
                        }
                        else
                            ThisBody.velocity = (Path_UpTogether[1].position - ThisTransform.position) * 5f;
                    }
                }
                break;
        }

        // target(플레이어)가 있는 방향으로 고개를 돌림
        if (targetTransform.position.x - ThisTransform.position.x > 0)
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

    // 모드 바뀔 때 모션
    IEnumerator ModeMotion()
    {
        yield return new WaitForSecondsRealtime(0.75f);
        headAnimator.ResetTrigger("hit");
        headAnimator.ResetTrigger("attack");
        bodyAnimator.ResetTrigger("hit");
        bodyAnimator.SetTrigger("mode");
        yield return new WaitForSecondsRealtime(0.1f);
        wingsAnimator.ResetTrigger("hit");
        wingsAnimator.ResetTrigger("attack");
        wingsAnimator.SetTrigger("mode");
        yield return new WaitForSecondsRealtime(0.05f);
        ThisAudio.clip = modeChange;
        ThisAudio.Play();
    }

    // enemy2 공격 함수1
    IEnumerator Enemy2_Attack1()
    {
        flag = false;
        headAnimator.SetTrigger("attack");
        wingsAnimator.SetTrigger("attack");
        // 상하좌우+대각선4방향으로 말탄환을 날린다.
        for (int k = 0; k < 8; k++)
        {
            targetDir = barrel[k].position;
            Dir = targetDir - (Vector2) ThisTransform.position;
            bullet[k].gameObject.SetActive(true);
            bullet[k].position = barrel[k].position;
            bullet[k].rotation = barrel[k].rotation;
            Bullets[k].MoveBullet(Dir, bulletSpeed, 0);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        Invoke("SetFlag", 0.8f);
        Invoke("ActivateBullets", bulletTimeOut[Random.Range(0, bulletTimeOut.Length)]);
    }

    // enemy2 공격 함수2
    IEnumerator Enemy2_Attack2()
    {
        int index = 0;
        Quaternion[] R = {barrel[2].rotation, barrel[3].rotation, barrel[4].rotation, barrel[5].rotation, barrel[6].rotation};
        // 좌우하+대각선2방향으로 말탄환을 날린다.
        for (int i = 0; i < 4; i++) {
            headAnimator.SetTrigger("attack");
            wingsAnimator.SetTrigger("attack");
            for (int k = 2; k <= 6; k++)
            {
                barrel[k].rotation = R[k-2];
                targetDir = barrel[k].position;
                Dir = targetDir - (Vector2) ThisTransform.position;
                bullet[index].gameObject.SetActive(true);
                bullet[index].position = barrel[k].position;
                bullet[index].rotation = barrel[k].rotation;
                Bullets[index].MoveBullet(Dir, bulletSpeed, 0);
                yield return new WaitForSecondsRealtime(0.2f);
                index += 1;
                index %= 16;
            }
            yield return new WaitForSecondsRealtime(1f);
            headAnimator.SetTrigger("attack");
            wingsAnimator.SetTrigger("attack");
            for (int k = 6; k >= 2; k--)
            {
                if (i % 2 == 0)
                    barrel[k].Rotate(new Vector3(0, 0, 22.5f));
                else
                    barrel[k].Rotate(new Vector3(0, 0, -22.5f));
                targetDir = barrel[k].position;
                Dir = targetDir - (Vector2) ThisTransform.position;
                bullet[index].gameObject.SetActive(true);
                bullet[index].position = barrel[k].position;
                bullet[index].rotation = barrel[k].rotation;
                Bullets[index].MoveBullet(Dir, bulletSpeed, 0);
                yield return new WaitForSecondsRealtime(0.2f);
                index += 1;
                index %= 16;
            }
            yield return new WaitForSecondsRealtime(1f);
        }
        yield return new WaitForSecondsRealtime(1f);
        Invoke("ActivateBullets", 0.5f);
        count = 0;
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
        Invoke("SetFlag", 0.8f);
    }

    private void SetFlag()
    {
        if(!waterball.activeSelf)
            flag = true;
    }

    // enemy1 공격 함수
    public void Enemy1_Attack()
    {
        if (!enemy1_CanAttack)
            return;
        
        enemy1_CanAttack = false;
        headAnimator.SetTrigger("attack");
        wingsAnimator.SetTrigger("attack");
        // target(플레이어) 방향으로 말탄환을 날린다.
        targetDir = targetTransform.position;
        Dir = targetDir - (Vector2) ThisTransform.position;
        if (Facing == FaceDirection.FaceLeft)
            digree = Mathf.Atan2(barrel[0].position.y - targetDir.y, barrel[0].position.x - targetDir.x) * 180f / Mathf.PI;
        else
            digree = - Mathf.Atan2(barrel[0].position.y - targetDir.y, barrel[0].position.x - targetDir.x) * 180f / Mathf.PI + 180;
        barrel[0].Rotate(0, 0, digree);
        int k = Random.Range(0, bullet.Length);
        bullet[k].gameObject.SetActive(true);
        bullet[k].position = barrel[0].position;
        bullet[k].rotation = barrel[0].rotation;
        Bullets[k].MoveBullet(Dir, bulletSpeed, 0);
        // 공격 딜레이
        if (mode == B_UIManager.enemyMode.normal)
        {
            Bullets[k].Invoke("Delete", destroyTime);
            Invoke("ActivateBullet", bulletTimeOut[0]);
        }
        else if (mode == B_UIManager.enemyMode.UpTogether)
        {
            Bullets[k].Invoke("Delete", 1.5f);
            Invoke("ActivateBullet", 3f);
        }
    }

    // enemy1 공격 딜레이. 제한 시간이 지나야 CanAttack 변수를 활성화한다.
    private void ActivateBullet()
    {
        enemy1_CanAttack = true;
        barrel[0].Rotate(0, 0, -digree);
    }

    // 공격 당했을 시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("waterbullet") && Health > 0)
        {
            Health--;
            headAnimator.SetTrigger("hit");
            wingsAnimator.SetTrigger("hit");
            bodyAnimator.SetTrigger("hit");
            flag = false;
            if (ThisName.Equals("enemy1"))
                UIM.HitEnemy1();
            else if(ThisName.Equals("enemy2"))
                UIM.HitEnemy2();
            waterball.SetActive(true);
            HitMessage.SetActive(true);
            if(Health > 0)
                Invoke("SetFlagAndBall", 2f);
            else
                Invoke("SetFlagAndBall", 1.5f);
        }
    }

    private void SetFlagAndBall()
    {
        waterball.SetActive(false);
        HitMessage.SetActive(false);
        if (Health > 0)
        {
            ThisAudio.clip = enemyHit;
            ThisAudio.Play();
            flag = true;
        }
        else
        {
            Clear();
        }
    }

    // 적이 클리어된 경우
    public void Clear()
    {
        if (!clear)
        {
            Vector2 ThisPosition = ThisTransform.position;
            ClearEnemy.GetComponent<Transform>().position = new Vector2(ThisPosition.x, ThisPosition.y - 1.2f);
            gameObject.SetActive(false);
            ClearEnemy.SetActive(true);
            if (ThisName.Equals("enemy1"))
            {
                UIM.flag1 = false;
                item1.SetActive(true);
                item1.GetComponent<B_DestroyInTime>().Invoke("Delete", 5f);
            }
            else if (ThisName.Equals("enemy2"))
            {
                UIM.flag2 = false;
                item2.SetActive(true);
                item2.GetComponent<B_DestroyInTime>().Invoke("Delete", 5f);
            }

            // 적 둘 다 clear 됐을 시에는 아이템이 나오지 않는다.
            if (!UIM.flag1 && !UIM.flag2)
            {
                item1.SetActive(false);
                item2.SetActive(false);
            }
        }
        clear = true;
    }

}
