using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B_UIManager : MonoBehaviour {
    
    public Camera mainCame;
    public Transform playerTarget, enemy1Target, enemy2Target, clear1Target, clear2Target;
    public GameObject door1, door2, item1, item2;
    public RectTransform attackImage, enemy1_bar, enemy2_bar;
    public Text attackLimitText1, attackLimitText2;
    public Text shieldLimitText1, shieldLimitText2;
    public Scrollbar playerHPbar;
    public Slider hourglassA, hourglassB, hourglassC;
    public int attackLimit = 15, shieldLimit = 5;
    [SerializeField]
    public int playerMaxHP = 600;
    int playerHP;
    public bool moveSand = false;
    // 적이 클리어되면 false가 된다.
    public bool flag1 = true, flag2 = true, doflag = false, isItem = false;
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
    }

    // Update is called once per frame
    void Update () {
        attackImage.position = mainCame.WorldToScreenPoint(new Vector3(playerTarget.position.x, playerTarget.position.y, transform.position.z));
        if (flag1)
            enemy1_bar.position = mainCame.WorldToScreenPoint(new Vector3(enemy1Target.position.x, enemy1Target.position.y, transform.position.z));
        else
            enemy1_bar.position = mainCame.WorldToScreenPoint(new Vector3(clear1Target.position.x, clear1Target.position.y+0.4f, transform.position.z));
        if (flag2)
            enemy2_bar.position = mainCame.WorldToScreenPoint(new Vector3(enemy2Target.position.x, enemy2Target.position.y, transform.position.z));
        else
            enemy2_bar.position = mainCame.WorldToScreenPoint(new Vector3(clear2Target.position.x, clear2Target.position.y+0.4f, transform.position.z));

        // 모래시계 UI 관련
        if (moveSand)
        {
            moveSand = false;
            StartCoroutine("MoveSand");
        }
        if (hourglassA.value > playerHP && !isItem)
        {
            diffHP = (hourglassA.value - playerHP)*0.12f;
            hourglassA.value -= diffHP;
            hourglassB.value += diffHP;
        }
        else if (hourglassA.value < playerHP && isItem)
        {
            diffHP = (playerHP - hourglassA.value) * 0.12f;
            hourglassA.value += diffHP;
            hourglassB.value -= diffHP;
        }

        if (!flag1 && !flag2 && !doflag)
        {
            doflag = true;
            door1.SetActive(true);
            door2.SetActive(true);
            item1.SetActive(false);
            item2.SetActive(false);
        }
    }

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

    public void HitPlayer(int power)
    {
        moveSand = true;
        playerHP -= power;
        playerHPbar.size = (float) playerHP / playerMaxHP;
        print(playerHP + " HitPlayer " + power);
    }

    public void HealPlayer(int power)
    {
        moveSand = true;
        playerHP += power;
        playerHPbar.size = (float)playerHP / playerMaxHP;
        print(playerHP + " HealPlayer " + power);
    }

    public void HitEnemy1()
    {
        enemy1HP--;
        enemy1_bar.GetComponent<Scrollbar>().size = (float) enemy1HP / enemyMaxHP;
        print(enemy1HP + " enemy1 ");
    }

    public void HitEnemy2()
    {
        enemy2HP--;
        enemy2_bar.GetComponent<Scrollbar>().size = (float) enemy2HP / enemyMaxHP;
        print(enemy2HP + " enemy2 ");
    }

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
