using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗 用户 移动
public class BattleUserMove : MonoBehaviour {

    private Vector2 movment;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        #region 移动
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        //this.movment = new Vector2()
        #endregion
    }
}
