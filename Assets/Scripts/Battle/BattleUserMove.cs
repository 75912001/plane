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
        this.movement = new Vector2(inputX*Global.Instance.battleMgr.GetUserPlane().battleMoveMgr.battleMove.speed.x,
             inputY* Global.Instance.battleMgr.GetUserPlane().battleMoveMgr.battleMove.speed.x);
        #endregion
        #region 防止移动出摄像机范围 
        {
            var dist = (transform.position - Camera.main.transform.position).z;
            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
                Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
                transform.position.z);
        }
        #endregion
    }

    void FixedUpdate(){
        //移动
        GetComponent<Rigidbody2D>().velocity = this.movement;
    }
}
