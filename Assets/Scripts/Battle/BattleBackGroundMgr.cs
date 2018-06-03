using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBackGroundMgr {

    //背景名 列表
    public List<string> backGroundNameList;

    public BattleBackGroundMgr(){
        this.backGroundNameList = new List<string>();
        #region 加载背景
        this.backGroundNameList.Add("Prefabs/BackGround/forest_01");
        this.backGroundNameList.Add("Prefabs/BackGround/forest_02");
        this.backGroundNameList.Add("Prefabs/BackGround/forest_03");
        this.backGroundNameList.Add("Prefabs/BackGround/forest_02");
        #endregion
    }
}
