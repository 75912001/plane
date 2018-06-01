using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗 用户 移动
public class BattleUserMove : MonoBehaviour {

    private Vector2 movement = new Vector2(1, 1);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        #region 移动
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        // this.movment = new Vector2(inputX*Global.Instance.battleMgr.GetUserPlane().battleMoveMgr.battleMoveUser.speed.x,
        //     inputY* Global.Instance.battleMgr.GetUserPlane().battleMoveMgr.battleMoveUser.speed.x);
        this.movement = new Vector2(inputX*50, inputY*50);
        #endregion
        Debug.LogFormat("x:{0}, y:{1}", this.movement.x, this.movement.y);
    }

    void FixedUpdate()
    {
        //移动
        GetComponent<Rigidbody2D>().velocity = this.movement;
        Debug.LogFormat("{0}", GetComponent<Rigidbody2D>().name);
        Debug.LogFormat("!!!!!!!!!!!!!!!!x:{0}, y:{1}", this.movement.x, this.movement.y);
    }
}
