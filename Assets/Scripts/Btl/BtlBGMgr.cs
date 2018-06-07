using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 战斗背景管理器
public class BtlBGMgr {
    //背景名 列表
    public List<string> bgNameList;

    public BtlBGMgr(){
        this.bgNameList = new List<string>();
    }
	public void Clear(){
		this.bgNameList.Clear();
	}
	//添加背景
	public void Add(string bgName){
		this.bgNameList.Add(bgName);
	}
}
#endregion