﻿using System.Collections;
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
    //是否僚机
	public bool isWingman;
    //归属飞机
	public BattlePlane parentBattlePlane;
    //todo 僚机AI
    //todo hp
    //todo 碰撞后给予敌方造成的伤害
    //todo 是否可碰撞己方飞机
    //入场飞行是否结束
    public bool isEnterSceneEnd;
    //是否可见的.在摄像机可见
    public bool isVisble;
    public bool isUser(){
        return this == Global.Instance.battleMgr.battlePlaneMgr.battlePlaneUser;
    }
    public BattlePlane(){
        this.battleBulletMgr = new BattleBulletMgr();
		this.battleBulletMgr.parent = this;
    }
    public void Clear(){
        this.gameObject = null;
        this.name = "";
        this.battleBulletMgr.Clear();
        this.isEnterSceneEnd = false;
        this.isWingman = false;
        this.isVisble = false;
    }

    //是否无敌
    public bool IsInvincible(){
        return false == this.isEnterSceneEnd;
    }
	//是否可以开火
	public bool CanFire(){
		return false != this.isEnterSceneEnd && this.isVisble;
	}
}
#endregion

#region 战斗中的飞机管理器
public class BattlePlaneMgr{
    public BattlePlane battlePlaneUser;
	public List<BattlePlane> battlePlaneEnemyList;
    public List<BattlePlane> parkingApronBattlePlaneEnemyList;
    public float enemyFlyTime;
    //敌机 起飞 冷却时间
    public float enemyFlyCoolDownTime;
    public BattlePlaneMgr(){
        this.battlePlaneUser = new BattlePlane();
		this.battlePlaneEnemyList = new List<BattlePlane> ();
        this.parkingApronBattlePlaneEnemyList = new List<BattlePlane>();
        this.enemyFlyCoolDownTime = 1.0f;
        this.enemyFlyTime = 1.0f;
    }
	public void Clear(){
		this.battlePlaneUser.Clear();
		this.battlePlaneEnemyList.Clear();
        this.parkingApronBattlePlaneEnemyList.Clear();
        this.enemyFlyCoolDownTime = 1.0f;
        this.enemyFlyTime = 1.0f;
    }
}
#endregion