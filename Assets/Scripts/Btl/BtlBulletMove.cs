using UnityEngine;

#region 战斗 子弹 移动 组件
public class BtlBulletMove : MonoBehaviour {
	//归属
	public BtlBullet parent;

	// Use this for initialization
	void Start () {
        //n秒后自动销毁
        Destroy(gameObject, 20);
    }
	
	// Update is called once per frame
	void Update () {
        BtlMove btlMove = this.parent.btlMove;
		switch (btlMove.moveTrace) {
		case EnumMoveTrace.Line://直线
            float xAbs = System.Math.Abs(btlMove.direction.x);
            float yAbs = System.Math.Abs(btlMove.direction.y);
            float sumAbs = xAbs + yAbs;
            sumAbs = (0 == sumAbs ? 1 : sumAbs);
            btlMove.movement.x = btlMove.speed.x * (btlMove.direction.x < 0 ? -1 : 1) * xAbs/sumAbs;
            btlMove.movement.y = btlMove.speed.y * (btlMove.direction.y < 0 ? -1 : 1) * yAbs/sumAbs;
			break;
		default:
			Debug.LogErrorFormat ("子弹 移动类型{0}", btlMove.moveTrace);
			break;
		}
	}
    void FixedUpdate(){
        // Apply movement to the rigidbody 
		GetComponent<Rigidbody2D>().velocity = this.parent.btlMove.movement;
    }
}
#endregion
