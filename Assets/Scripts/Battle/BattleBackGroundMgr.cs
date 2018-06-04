using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 战斗背景管理器
public class BattleBackGroundMgr {
    //背景名 列表
    public List<string> backGroundNameList;

    public BattleBackGroundMgr(){
        this.backGroundNameList = new List<string>();
    }
	public void Clear(){
		this.backGroundNameList.Clear ();
	}
	//添加背景
	public void Add(string backGroundName){
		this.backGroundNameList.Add(backGroundName);
	}
}
#endregion