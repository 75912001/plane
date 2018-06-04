using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 战斗 子弹 移动 组件
public class BattleBulletMove : MonoBehaviour {
	//组件归属
	public BattleBullet parent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.parent.battleBulletMoveMgr.Update();
	}
    void FixedUpdate(){
        // Apply movement to the rigidbody 
		GetComponent<Rigidbody2D>().velocity = this.parent.battleBulletMoveMgr.movement;
    }
}
#endregion

#region 战斗中 子弹 移动 管理器
public class BattleBulletMoveMgr{
	//归属
	public BattleBullet parent;
	//子弹移动类型
	public int bulletMoveId;
	//速度
	public Vector2 speed;
	//方向
	public Vector2 direction;
	public Vector2 movement;
	public void Update(){
		switch (this.bulletMoveId) {
		case 1://直线
			this.movement = new Vector2 (this.speed.x * this.direction.x, this.speed.y * this.direction.y);
			break;
		default:
			Debug.LogErrorFormat ("子弹 移动类型{0}", this.bulletMoveId);
			break;
		}
	}
	public void Clear(){
		this.bulletMoveId = 0;
		this.speed = new Vector2(0, 0);
		this.direction = new Vector2(0, 0);
		this.movement = new Vector2(0, 0);
	}
}
#endregion