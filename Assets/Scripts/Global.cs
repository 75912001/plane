using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 全局数据
public class Global {
	public BattleMgr battleMgr;
	private Global(){
		this.battleMgr = new BattleMgr ();
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