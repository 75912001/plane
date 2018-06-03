
using UnityEngine;
//战斗中的飞机
public class BattlePlane
{
    //飞机
    public GameObject gameObject;

    //子弹管理器
    public BattleBulletMgr battleBulletMgr;

    //移动管理器
    public BattleMoveMgr battleMoveMgr;
    //是否僚机
    //归属飞机
    //僚机AI
    //是否无敌
    //hp
    //碰撞后给予敌方造成的伤害
    //是否可碰撞己方飞机

    //入场飞行是否结束
    public bool isEnterSceneEnd = false;
    public BattlePlane()
    {
        this.battleBulletMgr = new BattleBulletMgr();
        this.battleMoveMgr = new BattleMoveMgr();
    }
    
}

//战斗中的飞机管理器
public class BattlePlaneMgr
{
    public BattlePlane battlePlaneUser;
    public BattlePlaneMgr()
    {
        this.battlePlaneUser = new BattlePlane();
    }
}
