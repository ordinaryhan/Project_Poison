using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_CharacterMovement : MonoBehaviour {


    public GameObject joystick;
    public GameObject ladderCheck;
    public GameObject Manager;

    public float Charc_Speed;
    public Transform groundCheck; //to check if player is touching ground
    public float jumpForce = 5f; //jump height
    public LayerMask whatIsGround;

    //movement related
    public float horizontal;
    public float vertical;
    public bool jump = false;

    bool facingRight = true; //shifting image left & right
    bool grounded = false; // for detecting ground  

    //ladder and ground trigger
    public GameObject grounds;
    public Vector3 ladderPos;
    bool ladderRide = false;
    bool onLadder = false; //for ladder

    private float climbSpeed = 1.6f; //speed when climbing up the ladder
    float groundRadius = 0.01f; //distance from player to ground (when grounded)

    Animator anim;
    private Rigidbody2D rgbd;


    public AudioSource BGM;
    public AudioClip jumping;

    // Use this for initialization
    void Start () {
        rgbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ladderCheck.SetActive(false); //시작하면서 ladderfix 없애기
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        anim.SetBool("jump", !grounded);
        anim.SetFloat("vSpeed", rgbd.velocity.y); //how fast we are moving up n down

        //캐릭터 좌우 움직임
        horizontal = joystick.GetComponent<A_JoyStick>().Horizontal();
        vertical = joystick.GetComponent <A_JoyStick>().Vertical();

        rgbd.velocity = new Vector2(horizontal * Charc_Speed, rgbd.velocity.y);

        anim.SetFloat("walk", Mathf.Abs(horizontal));

        //좌우 버튼에 따라 플레이어 보는 방향 바꾸기
        if (horizontal > 0 && !facingRight)
            Flip();
        else if (horizontal < 0 && facingRight)
            Flip();

        if (onLadder && Mathf.Abs(vertical) > 0.13f)
        {
            onLadder = false;
            jump = false;
            rgbd.gravityScale = 0f;
            anim.SetBool("ladder", true);

            if (vertical > 0.13f)
                ladderCheck.SetActive(true); //사다리 일정거리 위로 못 올라가게 막는 것

            //플레이어 사다리 가운데에 배치하기
            Vector3 playerPos = transform.position;
            playerPos.x = ladderPos.x;
            transform.position = playerPos;

            //사다리에 올라가면 바닥이 trigger 상태로 변함 (벽을 통과해서 올라가기 위해서)
            grounds.SetActive(false);

            ladderRide = true;
        }

        if (ladderRide)
            rgbd.velocity = new Vector2(0, vertical * climbSpeed);
    }

    public void Jump()
    {
        if (grounded)
        {
            this.rgbd.velocity += jumpForce * Vector2.up;
            BGM.clip = jumping;
            BGM.Play();
        }

        //사다리에 타고있는 상태로 점프할때 (좌우버튼을 꼭 누르고 있어야함)
        if (ladderRide && horizontal != 0)
        {
            jump = true;

            ladderRide = false;
            anim.SetBool("ladder", false);
            rgbd.gravityScale = 0.9f;

            ladderCheck.SetActive(false);
            this.rgbd.velocity += jumpForce * Vector2.up;

            BGM.clip = jumping;
            BGM.Play();
        }
    }

    //캐릭터 좌우반전
    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;
        
        transform.localScale = theScale;
    }

    //사다리 타는 모션
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ladder")
        {
            onLadder = true;
            ladderPos = other.GetComponent<BoxCollider2D>().offset;
        }

        if(other.gameObject.name == "Door")
        {          
            Manager.GetComponent<A_TotalManager>().GameOver_Door(); //게임 엔드 코드 짜야함
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //if (other.tag == "Ladder" && vertical < 0.13f)
        //{
        //    ladderCheck.SetActive(true);
        //}

        if (other.gameObject.name == "LadderUp" && vertical < -0.13f)
        {
            grounds.SetActive(false);
        }
    }

        void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Ladder")
        {
            ladderRide = false;
            onLadder = false;

            anim.SetBool("ladder", false);
            rgbd.gravityScale = 0.9f;

            grounds.SetActive(true);
        }
    }
}
