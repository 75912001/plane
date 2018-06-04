

//战斗中子弹
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBullet
{
    //子弹类型
    public int bulletId;

    //开火冷却
    public float fireCoolDown = 1.0f;
    //开火 秒
    public float fireTime = 1.0f;

    //子弹名称
    public string bulletName;
    //移动管理器
    public BattleBulletMoveMgr battleBulletMoveMgr;
    //破坏力
    public int damage = 1;
    //碰撞对象
    //碰撞后是否消失
    //二次爆炸后 触发新的子弹管理器
    //子弹归属者
    public BattlePlane battlePlane;
    //子弹归属者消失后,子弹是否消失
    //是否暂停
    //子弹发射位置
    public BattleBullet()
    {
        this.battleBulletMoveMgr = new BattleBulletMoveMgr();
    }
    //是否可以攻击
    public bool CanFire(){
        return this.fireCoolDown <= 0.0f;
           // && this.battlePlane.isEnterSceneEnd;
    }
}

//战斗中子弹管理器
public class BattleBulletMgr {
    public List<BattleBullet> battleBulletList;
    public BattleBulletMgr(){
        this.battleBulletList = new List<BattleBullet>();
    }
    public void Add(string bulletName)
    {
        BattleBullet battleBullet = new BattleBullet();
        battleBullet.bulletName = bulletName;
        battleBullet.battleBulletMoveMgr.bulletMoveId = 1;
        this.battleBulletList.Add(battleBullet);
    }
}


public class BattleBulletMoveMgr
{
    //子弹移动类型
    public int bulletMoveId;
    //速度
    public Vector2 speed = new Vector2(0, 10);
    //方向
    public Vector2 direction = new Vector2(0, 1);
    public Vector2 movement;
    public void Update()
    {
        this.movement = new Vector2(this.speed.x * this.direction.x, this.speed.y * this.direction.y);
    }
}