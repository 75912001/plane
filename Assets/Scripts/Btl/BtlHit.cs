using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 战斗 命中 组件
public class BtlHit : MonoBehaviour {
    //组件归属
    public BtlPlane parent;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        todo
        //敌方子弹造成伤害，并销毁子弹
        BtlBulletMove btlBulletMove = otherCollider.gameObject.GetComponent<BtlBulletMove> ();
        if (null != btlBulletMove) {
            //btlBulletMove.parent.damage
            //this.Damage(bulletScript.damage);
            Destroy(btlBulletMove.gameObject);
            return;
        }
    }
}
#endregion