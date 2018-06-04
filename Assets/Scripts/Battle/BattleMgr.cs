using UnityEngine;

#region 战斗管理器
public class BattleMgr{
	//战斗关卡
	public int level;

    public BattlePlaneMgr battlePlaneMgr;

    public BattleBackGroundMgr battleBackGroundMgr;

	public BattleMgr(){
        this.battlePlaneMgr = new BattlePlaneMgr();
        this.battleBackGroundMgr = new BattleBackGroundMgr();
		#region 加载背景
		this.battleBackGroundMgr.Add("Prefabs/BackGround/forest_01");
		this.battleBackGroundMgr.Add("Prefabs/BackGround/forest_02");
		this.battleBackGroundMgr.Add("Prefabs/BackGround/forest_03");
		this.battleBackGroundMgr.Add("Prefabs/BackGround/forest_02");
		#endregion
	}
	public void Clear(){
		this.level = 0;
		this.battlePlaneMgr.Clear ();
		this.battleBackGroundMgr.Clear ();
	}
    public GameObject GetUserPlaneGameObject(){
        return this.battlePlaneMgr.battlePlaneUser.gameObject;
    }
    public Transform GetUserPlaneTransform(){
        return this.battlePlaneMgr.battlePlaneUser.gameObject.transform;
    }
    public BattlePlane GetUserPlane(){
        return this.battlePlaneMgr.battlePlaneUser;
    }
}
#endregion