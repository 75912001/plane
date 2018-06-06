using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 战斗 子弹 移动 组件
public class BattleBulletMove : MonoBehaviour {
	//归属
	public BattleBullet parent;
	//子弹移动类型
	public EnumMoveTrace bulletMoveTrace;
	//速度
	public Vector2 speed = new Vector2(0,0);
	//方向
	public Vector2 direction = new Vector2(0, 0);
	public Vector2 movement = new Vector2(0, 0);
	public void Clear(){
		this.bulletMoveTrace = 0;
		this.speed = new Vector2(0, 0);
		this.direction = new Vector2(0, 0);
		this.movement = new Vector2(0, 0);
	}
	// Use this for initialization
	void Start () {
        //n秒后自动销毁
        Destroy(gameObject, 10);
    }
	
	// Update is called once per frame
	void Update () {
		switch (this.bulletMoveTrace) {
		case EnumMoveTrace.Line://直线
			this.movement = new Vector2 (this.speed.x * this.direction.x, this.speed.y * this.direction.y);
			break;
		default:
			Debug.LogErrorFormat ("子弹 移动类型{0}", this.bulletMoveTrace);
			break;
		}
	}
    void FixedUpdate(){
        // Apply movement to the rigidbody 
		GetComponent<Rigidbody2D>().velocity = this.movement;
    }
}
#endregion
