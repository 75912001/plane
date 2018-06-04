using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 战斗中的飞机
public class BattlePlane{
    //飞机
    public GameObject gameObject;
    //飞机名称
    public string name;
    //子弹管理器
    public BattleBulletMgr battleBulletMgr;
    //移动管理器
    public BattleMoveMgr battleMoveMgr;
    //是否僚机
	public bool isWingman;
    //归属飞机
	public BattlePlane parent;
    //todo 僚机AI
    //todo hp
    //todo 碰撞后给予敌方造成的伤害
    //todo 是否可碰撞己方飞机
    //入场飞行是否结束
    public bool isEnterSceneEnd;
    public BattlePlane(){
        this.battleBulletMgr = new BattleBulletMgr();
		this.battleBulletMgr.parent = this;
        this.battleMoveMgr = new BattleMoveMgr();
    }
    //是否无敌
    public bool IsInvincible(){
        return false == this.isEnterSceneEnd;
    }
	//是否可以开火
	public bool CanFire(){
		return false != this.isEnterSceneEnd;
	}
	public void Clear(){
		this.gameObject = null;
		this.name = "";
		this.battleBulletMgr.Clear ();
		this.battleMoveMgr.Clear ();
		this.isEnterSceneEnd = false;
		this.isWingman = false;
	}
}
#endregion

#region 战斗中的飞机管理器
public class BattlePlaneMgr{
    public BattlePlane battlePlaneUser;
	public List<BattlePlane> battlePlaneEnemyList;
    public BattlePlaneMgr(){
        this.battlePlaneUser = new BattlePlane();
		this.battlePlaneEnemyList = new List<BattlePlane> ();
    }
	public void Clear(){
		this.battlePlaneUser.Clear ();
		this.battlePlaneEnemyList.Clear ();
	}
}
#endregion