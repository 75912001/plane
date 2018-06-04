using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBulletMove : MonoBehaviour {

    public BattleBullet battleBullet;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (this.battleBullet.battleBulletMoveMgr.bulletMoveId)
        {
            case 1://直线
                this.battleBullet.battleBulletMoveMgr.Update();
                
                break;
            default:
                Debug.LogErrorFormat("子弹 移动类型{0}", this.battleBullet.battleBulletMoveMgr.bulletMoveId);
                break;
        }
	}
    void FixedUpdate()
    {
        // Apply movement to the rigidbody 
        GetComponent<Rigidbody2D>().velocity = this.battleBullet.battleBulletMoveMgr.movement;
    }
}