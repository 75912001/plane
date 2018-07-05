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
        //敌方子弹造成伤害，并销毁子弹
        BtlBulletMove btlBulletMove = otherCollider.gameObject.GetComponent<BtlBulletMove> ();
        if (null != btlBulletMove) {
            BtlPlane plane = this.parent;
            if (!plane.IsInvincible()){

                if (plane.hp <= btlBulletMove.parent.xmlBullet.damage)
                {
                    plane.hp = 0;

                    #region 掉落物品
                    {
                        GameObject prefabs = (GameObject)Resources.Load("Prefabs/Anim/crystal-big_1");
                        if (null == prefabs)
                        {
                            Debug.LogErrorFormat("Prefabs/Anim/crystal-big_1未找到");
                        }
                        GameObject gameObject = Instantiate(prefabs, plane.gameObject.transform.position, plane.gameObject.transform.rotation);
                        //gameObject.transform.SetParent(userPlane.gameObject.transform);
                        gameObject.layer = (int)EnumLayer.DropItem;
                    }
                    #endregion

                    Destroy(plane.gameObject);
                }
                else
                {
                    plane.hp -= btlBulletMove.parent.xmlBullet.damage;
                }
            }

            Destroy(btlBulletMove.gameObject);
            return;
        }
    }
}
#endregion