

using UnityEngine;

public class BattleMgr{
	//战斗关卡
	public int level;

    public BattlePlaneMgr battlePlaneMgr;
    //战斗
    public void Battle(){

	}
	public BattleMgr(){
        this.battlePlaneMgr = new BattlePlaneMgr();
	}
	public void Clear(){
		this.level = 0;
	}
    public GameObject GetUserPlaneGameObject()
    {
        return this.battlePlaneMgr.battlePlaneUser.gameObject;
    }
    public Transform GetUserPlaneTransform()
    {
        return this.battlePlaneMgr.battlePlaneUser.gameObject.transform;
    }

}
