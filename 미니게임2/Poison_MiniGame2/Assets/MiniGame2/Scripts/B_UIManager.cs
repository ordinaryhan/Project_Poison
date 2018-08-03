using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B_UIManager : MonoBehaviour {
    
    public Camera mainCame;
    public Transform playerTarget, enemy1Target, enemy2Target, clear1Target, clear2Target;
    public GameObject door1, door2;
    public RectTransform attackImage, enemy1_bar, enemy2_bar;
    public Text attackLimitText1, attackLimitText2;
    public Text shieldLimitText1, shieldLimitText2;
    public Scrollbar playerHPbar;
    public Slider hourglassA, hourglassB, hourglassC;
    public Animator BreakHourglass;
    public int attackLimit = 15, shieldLimit = 5;
    [SerializeField]
    public int playerMaxHP = 600;
    int playerHP;
    public bool moveSand = false, enemy2_page2 = false;
    // 적 공격 패턴
    public enum enemyMode { normal = -1, UpTogether = 0 };
    public enemyMode mode = enemyMode.normal;
    // 적이 클리어되면 false가 된다.
    public bool flag1 = true, flag2 = true, doflag = false, isItem = false, breakHourglass = false;
    [SerializeField]
    public int enemyMaxHP = 5;
    int enemy1HP, enemy2HP;
    float diffHP;
    // 애니메이션을 위한
    private Animator myAnimator1;
    private Animator myAnimator2;
    public Button attackButton;
    public Button shieldButton;

    private void Awake()
    {
        myAnimator1 = attackButton.GetComponent<Animator>();
        myAnimator2 = shieldButton.GetComponent<Animator>();
        playerHP = playerMaxHP;
        enemy1HP = enemyMaxHP;
        enemy2HP = enemyMaxHP;
        door1.SetActive(false);
        door2.SetActive(false);
        hourglassA.value = playerHP;
        hourglassB.value = playerMaxHP - playerHP;
        hourglassC.value = 0;
    }

    // Update is called once per frame
    void Update () {
        attackImage.position = mainCame.WorldToScreenPoint(new Vector3(playerTarget.position.x, playerTarget.position.y, transform.position.z));
        if (flag1)
            enemy1_bar.position = mainCame.WorldToScreenPoint(new Vector3(enemy1Target.position.x, enemy1Target.position.y, transform.position.z));
        else
            enemy1_bar.position = mainCame.WorldToScreenPoint(new Vector3(clear1Target.position.x-0.35f, clear1Target.position.y+0.85f, transform.position.z));
        if (flag2)
            enemy2_bar.position = mainCame.WorldToScreenPoint(new Vector3(enemy2Target.position.x, enemy2Target.position.y, transform.position.z));
        else
            enemy2_bar.position = mainCame.WorldToScreenPoint(new Vector3(clear2Target.position.x-0.25f, clear2Target.position.y+0.7f, transform.position.z));

        // 모래시계 UI 관련
        if (playerHP > 0)
        {
            if (moveSand)
            {
                moveSand = false;
                StartCoroutine("MoveSand");
            }
            // 플레이어 체력 양과 모래시계 양이 차이가 나면 diffHP씩 맞춰간다.
            if (hourglassA.value > playerHP && !isItem)
            {
                diffHP = (hourglassA.value - playerHP) * 0.12f;
                hourglassA.value -= diffHP;
                hourglassB.value += diffHP;
            }
            else if (hourglassA.value < playerHP && isItem)
            {
                diffHP = (playerHP - hourglassA.value) * 0.12f;
                hourglassA.value += diffHP;
                hourglassB.value -= diffHP;
            }
        }
        // 플레이어 체력이 0이 되면 모래시계 깨지는 효과가 나오게 한다. (1회 실행)
        else if(!breakHourglass)
        {
            breakHourglass = true;
            BreakHourglass.SetTrigger("break");
        }

        // 적 하나의 체력이 절반이하로 떨어지면 mode를 UpTogether로 바꾼다.
        if (enemy2HP <= enemyMaxHP * 0.5f || enemy1HP <= enemyMaxHP * 0.5f)
            mode = enemyMode.UpTogether;

        // 적이 모두 clear되면 문을 활성화 시킨다. (1회 실행)
        if (!flag1 && !flag2 && !doflag)
        {
            doflag = true;
            door1.SetActive(true);
            door2.SetActive(true);
        }
    }

    //모래 쏟아져 내리는 효과 관련 (hourglassC가 모래 막대 부분임)
    IEnumerator MoveSand()
    {
        for (int i = 1; i <= 10; i++)
        {
            hourglassC.value += 0.1f;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        Vector3 tempV = hourglassC.GetComponent<Transform>().localScale;
        tempV.y *= -1;
        if (hourglassC.direction == Slider.Direction.TopToBottom)
        {
            hourglassC.SetDirection(Slider.Direction.BottomToTop, true);
            hourglassC.GetComponent<Transform>().localScale = tempV;
        }
        else
        {
            hourglassC.SetDirection(Slider.Direction.TopToBottom, true);
            hourglassC.GetComponent<Transform>().localScale = tempV;
        }
        for (int i = 1; i <= 10; i++)
        {
            hourglassC.value -= 0.1f;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        tempV.y *= -1;
        if (hourglassC.direction == Slider.Direction.TopToBottom)
        {
            hourglassC.SetDirection(Slider.Direction.BottomToTop, true);
            hourglassC.GetComponent<Transform>().localScale = tempV;
        }
        else
        {
            hourglassC.SetDirection(Slider.Direction.TopToBottom, true);
            hourglassC.GetComponent<Transform>().localScale = tempV;
        }
    }

    // 플레이어 체력 관련
    public void HitPlayer(int power)
    {
        moveSand = true;
        playerHP -= power;
        playerHPbar.size = (float) playerHP / playerMaxHP;
    }

    public void HealPlayer(int power)
    {
        moveSand = true;
        playerHP += power;
        playerHPbar.size = (float)playerHP / playerMaxHP;
    }

    // 적 체력 관련
    public void HitEnemy1()
    {
        enemy1HP--;
        enemy1_bar.GetComponent<Scrollbar>().size = (float) enemy1HP / enemyMaxHP;
    }

    public void HitEnemy2()
    {
        enemy2HP--;
        enemy2_bar.GetComponent<Scrollbar>().size = (float) enemy2HP / enemyMaxHP;
    }

    // 공격/방어 개수 제한 관련
    public void Attack()
    {
        attackLimit--;
        attackLimitText1.text = "" + attackLimit;
        attackLimitText2.text = "" + attackLimit;
        myAnimator1.SetTrigger("Attack");
    }

    public void Shield()
    {
        shieldLimit--;
        shieldLimitText1.text = "" + shieldLimit;
        shieldLimitText2.text = "" + shieldLimit;
        myAnimator2.SetTrigger("Shield");
    }

}
