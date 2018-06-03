using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//开火
public class BattleFire : MonoBehaviour {

	public BattlePlane battlePlane;
	void Awake(){
		this.battlePlane = new BattlePlane ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//更新开火冷却时间
//		if(0 < this.battlePlane.
	}
}
