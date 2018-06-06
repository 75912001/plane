using UnityEngine;

#region 战斗管理器
public class BattleMgr{
	//战斗关卡
	public uint level;
    //战机 管理器
    public BattlePlaneMgr battlePlaneMgr;
    //战斗 背景 管理器
    public BattleBackGroundMgr battleBackGroundMgr;

	public BattleMgr(){
        this.battlePlaneMgr = new BattlePlaneMgr();
        this.battleBackGroundMgr = new BattleBackGroundMgr();
    }
	public void Clear(){
		this.level = 0;
		this.battlePlaneMgr.Clear();
		this.battleBackGroundMgr.Clear();
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