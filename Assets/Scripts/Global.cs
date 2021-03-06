﻿#region 全局数据
public class Global {
	public BtlMgr btlMgr;
    public XmlGameLevelMgr xmlGameLevelMgr;
    public XmlPlaneMgr xmlPlaneMgr;
    public XmlBulletMgr xmlBulletMgr;
	private Global(){
		this.btlMgr = new BtlMgr ();
        this.xmlGameLevelMgr = new XmlGameLevelMgr();
        this.xmlPlaneMgr = new XmlPlaneMgr();
        this.xmlBulletMgr = new XmlBulletMgr();

        this.xmlGameLevelMgr.LoadXml();
        this.xmlPlaneMgr.LoadXml();
        this.xmlBulletMgr.LoadXml();
	}
	//private static readonly Global instance = new Global();
	private static Global instance = new Global();
	public static Global Instance{
		get{
			return instance;
		}
	}

}
#endregion


#region 阵营
public enum EnumCamp:uint
{
	Blue = 1,//蓝
	Red = 2//红
};
#endregion
#region 运动轨迹
public enum EnumMoveTrace :uint
{
    //直线
    Line = 1
};
#endregion

#region 图层 layer
public enum EnumLayer:uint
{
    DropItem = 13,//掉落物品
    Enemy = 14,//敌人
    EnemyBullet = 15,//敌人子弹
    User = 16,//用户
    UserBullet = 17//用户子弹
    
};
#endregion