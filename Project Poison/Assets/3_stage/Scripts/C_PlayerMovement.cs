
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class C_PlayerMovement : MonoBehaviour {

    // 플레이어가 바라보는 방향
    public enum FaceDirection { FaceRight = -1, FaceLeft = 1 };
    public FaceDirection Facing = FaceDirection.FaceRight;

    //이동
    float dirX, dirY;
    int index = 0;
    public bool inputLeft = false;
    public bool inputRight = false;
    public bool inputUp = false;
    public bool inputDown = false;
    public float movePower = 6f;
    int k = 0;
    Vector3 moveVelocity = Vector3.zero;

    //속도
    public float MaxSpeed = 10f, Speed = 5f;

    bool isWalk = false;
    float health;
    bool isDie = false;

    //아이템
    bool isItem = false;
    //public GameObject[] phone;
    public Transform[] PathCenter;
    public GameObject item;
    public Transform[] itemarr;

    private Transform ThisTransform = null;
    int i = 0;
    public float PathCenterposX = 5f;
    public GameObject[] Enemy;
    private C_EnemyMove[] EnemyScript = null;
    int EnemyIndex = 0;
    bool isEnemyIndex = false;

    int PathIndex = 0;

    //충돌
    Rigidbody2D rigid;
    public Transform[] random_Pos;

    // 애니메이션을 위한
    public Animator faceAnimator;
    public Animator handsAnimator;
    public Animator bodyAnimator;

    public SpriteRenderer[] spriteRenderer;

    //충돌시 깜빡거림
    bool isUnBeatTime = false;

    bool flag = true;
    int[] index1 = { 0, 1, 2, 3, 7, 6, 10, 9, 8, 4, 0, 1, 2, 6, 5, 4, 8, 9, 10, 6, 2, 1, 0, 4, 5, 6, 10, 11, 7, 3, 2, 1, 0, 4, 8, 9, 10, 6, 7, 3, 2, 1, 0, 4, 5, 1, 0, 4, 5, 1, 2, 3, 7, 11, 10, 9, 8, 4, 0, 1, 2, 3, 7, 11, 10, 6, 2, 3, 7, 11, 10, 9, 8, 4, 0, 1, 2, 3, 7, 11, 10, 6, 2, 3, 7, 11, 10, 6, 2, 3, 7, 11, 10 };
    int[] index2 = { 11, 10, 9, 8, 4, 5, 1, 2, 3, 7, 6, 5, 9, 10, 11, 7, 3, 2, 1, 0, 4, 8, 9, 10, 11, 7, 3, 2, 1, 0, 4, 5, 9, 10, 11, 7, 3, 2, 1, 5, 4, 8, 9, 10, 11, 7, 6, 2, 3, 7, 6, 10, 9, 8, 4, 0, 1, 2, 6, 5, 9, 10, 6, 5, 9, 8, 4, 0, 1, 5, 6, 2, 1, 5, 6, 7, 11, 10, 9, 5, 4, 0, 1, 5, 9, 8, 4, 0, 1, 5, 9, 8, 4, 0 };
    public bool indexpattern = false;

    Vector3 movement;
    public float speed = 20; //속도

    public AudioSource player, gettingItem, Audio;
    public AudioClip hitEnemy, bigger;

    

    // Use this for initialization
    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        ThisTransform = GetComponent<Transform>();
        health = s_variable.score[1];
        EnemyScript = new C_EnemyMove[Enemy.Length];
        for(int i = 0; i < EnemyScript.Length; i++)
        {
            EnemyScript[i] = Enemy[i].GetComponent<C_EnemyMove>();
        }
        for (i = 0; i < PathCenter.Length; i++)
        {
            item.transform.position = PathCenter[i].transform.position;
            item.SetActive(false);
        }
        ThisTransform.position = PathCenter[5].position;
        i = 5;

        StartCoroutine("ItemTime");
    }
    void SetItemOn()
    {
        isItem = false;
    }
 
    public void Shuffle()
    {
        for(int i = 0; i< itemarr.Length; i++)
        {
            int random = Random.Range(0, itemarr.Length);
            Transform temp = itemarr[random];
            itemarr[random] = itemarr[i];
            itemarr[i] = temp;
        }
    }
    IEnumerator ItemTime()
    {
        while (true)
        {
            Shuffle();
            //Instantiate(생성시킬 오브젝트, 생성될 위치, 생성됐을때 회전값);
            //
            for (int i = 0; i < itemarr.Length; i++)
            {
                item.transform.position = itemarr[i].position;
                item.SetActive(true);
                yield return new WaitForSeconds(5f);
                item.SetActive(false);

                if (C_ScoreUpdate.instance.isItem)//Mathf.Abs(ThisTransform.localScale.x) == 7)
                {
                    item.SetActive(false); //아이템 생성 X
                    yield return new WaitForSeconds(8f);
                    item.SetActive(true);
                }
            }
           
        }

    }
    void Move()
    {
        bodyAnimator.SetBool("walk", false);
        handsAnimator.SetBool("walk",false);
    }
    public void Move_Left()
    {
        if (i == 0 || i == 4 || i == 8)
            return;
        bodyAnimator.SetBool("walk", true);
        handsAnimator.SetBool("walk", true);
        Invoke("Move", 1f);
        player.Play();
        transform.position = new Vector3(PathCenter[i - 1].transform.position.x, PathCenter[i - 1].transform.position.y, 0);
        i--;
        if(Facing == FaceDirection.FaceRight)
        {
            FlipDirection();
        }

    }
    public void Move_Right()
    {
        if (i == 3 || i == 7 || i == 11)
            return;
        bodyAnimator.SetBool("walk", true);
        handsAnimator.SetBool("walk", true);
        Invoke("Move", 1f);
        player.Play();
        transform.position = new Vector3(PathCenter[i + 1].transform.position.x, PathCenter[i + 1].transform.position.y, 0);
        i++;
        if (Facing == FaceDirection.FaceLeft)
        {
            FlipDirection();
        }

    }
    public void Move_Up()
    {
        if (i == 0 || i == 1 || i == 2 || i==3)
            return;
        bodyAnimator.SetBool("walk", true);
        handsAnimator.SetBool("walk", true);
        Invoke("Move", 1f);
        player.Play();
        transform.position = new Vector3(PathCenter[i - 4].transform.position.x, PathCenter[i - 4].transform.position.y, 0);
        i-=4;

    }
    public void Move_Down()
    {
        if (i == 8 || i == 9 || i == 10 || i==11)
            return;
        bodyAnimator.SetBool("walk", true);
        handsAnimator.SetBool("walk", true);
        Invoke("Move", 1f);
        player.Play();
        transform.position = new Vector3(PathCenter[i +4].transform.position.x, PathCenter[i +4].transform.position.y, 0);
        i+=4;

    }
    
    // 캐릭터 방향을 바꾼다.
    public void FlipDirection()
    {
        Facing = (FaceDirection)((int)Facing * -1f);
        Vector3 LocalScale = ThisTransform.localScale;
        LocalScale.x *= -1f;
        ThisTransform.localScale = LocalScale;
    }
    
    void RestartStage()
    {
        GameManager.RestartStage();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //rigidBody가 무언가와 충돌할때 호출되는 함수
        //Collider2D other로 부딪힌 객체를 받아온다.
        //적과 충돌

        if (other.CompareTag("enemy") && !isUnBeatTime)
        {
            Audio.clip = hitEnemy;
            Audio.Play();
            if (C_ScoreUpdate.instance.isItem)
            {
                other.GetComponent<C_EnemyMove>().StopMove();
                float enemyX = other.transform.position.x;
                Rigidbody2D enemyBody = other.GetComponent<Rigidbody2D>();
                if (enemyX - transform.position.x > 0)
                {
                    enemyBody.velocity = Vector3.right * 20;
                }
                else
                {
                    enemyBody.velocity = Vector3.left * 20;
                }
            }
            else
            {
                health -= 25;
                C_ScoreUpdate.instance.HitPlayer(25);
                if (health > 0)
                {
                    isUnBeatTime = true;
                    Random_Position();
                }
            }
         }

         //아이템충돌
        else if (other.CompareTag("Item"))
        {
            gettingItem.Play();
            item.SetActive(false);
            isItem = true;
            C_ScoreUpdate.instance.ScoreSet();
        }

    }
    //적과 충돌하면 랜덤위치로 이동
    void Random_Position()
    {
        bool[] tempP = new bool[PathCenter.Length];
        for (int k = 0; k < PathCenter.Length; k++)
        {
            tempP[k] = true;
            for (int a = 0; a < Enemy.Length; a++)
            {
                if (PathCenter[k].position == Enemy[a].transform.position)
                {
                    tempP[k] = false;
                    break;

                }
            }
        }
        int random = Random.Range(0, tempP.Length);
        while (!tempP[random])
        {
            random = Random.Range(0, tempP.Length);
        }
        Transform temp = PathCenter[random];
        ThisTransform.position = temp.position;
        
        StartCoroutine("UnBeatTime");// 이동후 깜박이기
        
    }

    public void CheckItem()
    {
        //누적 게이지가 0이하거나 거대화 상태면 item효과 실행 X
        if (C_ScoreUpdate.instance.score <= 0 || C_ScoreUpdate.instance.isItem)
            return;

        C_ScoreUpdate.instance.ScoreReset1();
        StartCoroutine("Itemact");
    }


    // 크기 조정
    IEnumerator Itemact()
    {
        C_ScoreUpdate.instance.isItem = true;
        Vector3 LocalScale = ThisTransform.localScale; //ThisSize를 LocalScale로 
        while (true)
        {
            if (Mathf.Abs(LocalScale.x) == 7)
            {
                if (LocalScale.x < 0)//왼쪽
                {
                    LocalScale.x = -7;
                    LocalScale.y = 7;
                    if (Facing == FaceDirection.FaceLeft)
                    {
                        FlipDirection();
                    }

                }
                else//오른쪽
                {
                    LocalScale.x = LocalScale.y = 7;
                    if (Facing == FaceDirection.FaceRight)
                    {
                        FlipDirection();
                    }
                }
                ThisTransform.localScale = LocalScale;
               
                break;
            }
            //서서히 작아지기
            else
            {
                Audio.clip = bigger;
                Audio.Play();
                if (LocalScale.x < 0)//왼쪽
                {
                    LocalScale.x -= 1;
                    if (Facing == FaceDirection.FaceLeft)
                    {
                        FlipDirection();
                    }
                }
                else//오른쪽
                {
                    LocalScale.x += 1;
                    if (Facing == FaceDirection.FaceRight)
                    {
                        FlipDirection();
                    }
                }
                LocalScale.y = Mathf.Abs(LocalScale.x);
                ThisTransform.localScale = LocalScale;
                yield return new WaitForSeconds(0.1f);
            }

        }
        float keeptime = 5f;// C_ScoreUpdate.instance.accumulate * 1.5f;
        yield return new WaitForSeconds(keeptime);

        //크기원래대로
        while (true)
        {
            if (Mathf.Abs(LocalScale.x) == 3)
            {
                if (LocalScale.x < 0)//왼쪽
                {
                    LocalScale.x = -3;
                    LocalScale.y = 3;
                    if (Facing == FaceDirection.FaceLeft)
                    {
                        FlipDirection();
                    }
                }
                else//오른쪽
                {
                    LocalScale.x = LocalScale.y = 3;
                    if (Facing == FaceDirection.FaceRight)
                    {
                        FlipDirection();
                    }
                }
                ThisTransform.localScale = LocalScale;
                
                break;
            }
            else
            {
                if (LocalScale.x < 0)//왼쪽
                {
                    LocalScale.x += 1;
                    if (Facing == FaceDirection.FaceLeft)
                    {
                        FlipDirection();
                    }
                }
                else//오른쪽
                {
                    LocalScale.x -= 1;
                    if (Facing == FaceDirection.FaceRight)
                    {
                        FlipDirection();
                    }
                }
                LocalScale.y = Mathf.Abs(LocalScale.x);
                ThisTransform.localScale = LocalScale;
                
                yield return new WaitForSeconds(0.1f);
            }
            
        }

            //아이템 사용시 enemy 패턴
            C_ScoreUpdate.instance.isItem = false;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < Enemy.Length; i++)
        {
            Enemy[i].SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        
        for (int i = 0; i < Enemy.Length; i++)
        {
            if (i < 3)
            {
                EnemyScript[i].InitPosition(i);
                //Enemy[i].transform.position = PathCenter[index1[i]].position;
                Enemy[i].SetActive(true);
                EnemyScript[i].isDrag = false;
            }
            else
            {
                EnemyScript[i].InitPosition(i-3);
                //Enemy[i].transform.position = PathCenter[index2[i-3]].position;
                Enemy[i].SetActive(true);
                EnemyScript[i].isDrag = false;
            }
        }
        //while (true)
        //{
        //    for (int i = 0; i < Enemy.Length; i++)
        //    {
        //        if (indexpattern)
        //            Enemy[i].transform.position = PathCenter[index1[i]].position;
        //        else
        //            Enemy[i].transform.position = PathCenter[index2[i]].position;
        //        yield return new WaitForSeconds(1f);
        //        if (i == index1.Length)
        //            i = 0;
        //    }
            
        //}
    }

    //깜박거리기
    IEnumerator UnBeatTime()
    {

            int countTime = 0;
            while (countTime < 10)
            {
                for (int i = 0; i < spriteRenderer.Length; i++)
                {
                    if (countTime % 2 == 0)
                        spriteRenderer[i].color = new Color32(255, 255, 255, 90);
                    else
                        spriteRenderer[i].color = new Color32(255, 255, 255, 180);
                }
                yield return new WaitForSeconds(0.2f);
                    countTime++;
            }
        for (int i = 0; i < spriteRenderer.Length; i++)
            spriteRenderer[i].color = new Color32(255, 255, 255, 255);
    
        isUnBeatTime = false;
    }
    
}


