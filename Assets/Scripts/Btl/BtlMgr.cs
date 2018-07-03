using UnityEngine;

#region 战斗管理器
public class BtlMgr{
    
    //战机 管理器
    public BtlPlaneMgr btlPlaneMgr;
    //战斗 背景 管理器
    public BtlBGMgr btlBGMgr;
    public int gameLevel = 1;

	public BtlMgr(){
        this.btlPlaneMgr = new BtlPlaneMgr();
        this.btlBGMgr = new BtlBGMgr();
    }
	public void Clear(){
		this.btlPlaneMgr.Clear();
		this.btlBGMgr.Clear();
	}
    public GameObject GetUserPlaneGameObject(){
        return this.btlPlaneMgr.btlPlaneUser.gameObject;
    }
    public Transform GetUserPlaneTransform(){
        return this.btlPlaneMgr.btlPlaneUser.gameObject.transform;
    }
    public BtlPlane GetUserPlane(){
        return this.btlPlaneMgr.btlPlaneUser;
    }
}
#endregion