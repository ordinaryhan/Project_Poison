using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B_AttackLimit : MonoBehaviour {
    
    public Camera mainCame;
    public Transform target;
    public RectTransform attackImage;
    public Text attackLimitText1;
    public Text attackLimitText2;
    public int attackLimit = 15;
	
	// Update is called once per frame
	void Update () {
        attackImage.position = mainCame.WorldToScreenPoint(new Vector3(target.position.x, target.position.y, transform.position.z));
	}

    public void Attack()
    {
        attackLimit--;
        attackLimitText1.text = "" + attackLimit;
        attackLimitText2.text = "" + attackLimit;
    }

}
