using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B_FlipHourglass : MonoBehaviour {

    public bool doFlip = false;
    public Slider hourglassC;
	
	// Update is called once per frame
	void Update () {
        // 아이템 먹을 시, 모래시계 뒤집기
        if (doFlip)
        {
            doFlip = false;
            StartCoroutine("FlipHourglass");
        }
    }

    IEnumerator FlipHourglass()
    {
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
        float initY = transform.localScale.y;
        if (initY > 0)
        {
            for (int i = 1; i <= 40; i++)
            {
                transform.localScale = new Vector3(1, 1f - i*0.05f, 1);
                yield return new WaitForSecondsRealtime(0.015f);
            }
        }
        else
        {
            for (int i = 1; i <= 40; i++)
            {
                transform.localScale = new Vector3(1, -1f + i * 0.05f, 1);
                yield return new WaitForSecondsRealtime(0.015f);
            }
        }
    }

}
