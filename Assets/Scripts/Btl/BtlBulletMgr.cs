﻿using System.Collections.Generic;
using UnityEngine;

//todo 将子弹中的属性 提取出来 放在 子弹的配置表中


#region 战斗中子弹
public class BtlBullet{
	//归属
	public BtlBulletMgr parent;
	//子弹
	public GameObject gameObject;
    public BtlMove btlMove;
    //子弹名称
    public string bulletName;
    //子弹
    public XmlBullet xmlBullet;
    //开火冷却 秒
    public float fireCoolDown;

    public EnumCamp camp;

    //碰撞对象
    //碰撞后是否消失
    //二次爆炸后 触发新的子弹管理器
    //子弹归属者消失后,子弹是否消失
    //是否暂停
    public BtlBullet(){
        this.btlMove = new BtlMove();
        this.camp = EnumCamp.Red;
    }
    //是否可以攻击
    public bool CanFire(){
		return this.fireCoolDown <= 0.0f;
    }
	public void Clear(){
		this.parent = null;
		this.gameObject = null;
		this.bulletName = "";
		this.fireCoolDown = 0f;
        this.btlMove.Clear();
        this.camp = EnumCamp.Red;
	}
}
#endregion

#region 战斗中子弹管理器
public class BtlBulletMgr {
    public List<BtlBullet> btlBulletList;
	//归属飞机
	public BtlPlane parent;
    public BtlBulletMgr(){
        this.btlBulletList = new List<BtlBullet>();
    }
    public void Add(XmlBullet xmlBullet){
        BtlBullet battleBullet = new BtlBullet();
		battleBullet.parent = this;
        battleBullet.bulletName = xmlBullet.prefabs;
        battleBullet.xmlBullet = xmlBullet;
		battleBullet.fireCoolDown = xmlBullet.coolDown;
        this.btlBulletList.Add(battleBullet);
    }
	public void Clear(){
		this.btlBulletList.Clear ();
	}
}
#endregion

