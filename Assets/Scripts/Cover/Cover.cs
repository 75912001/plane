using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		#region 加载封面
		string coverName = "Prefabs/cover_01";
		GameObject prefabs = (GameObject)Resources.Load (coverName);
		if (null == prefabs) {
			Debug.LogErrorFormat ("Cover未找到{0}",coverName);
		}

		GameObject level = GameObject.Find ("Level");
		if (null == level) {
			Debug.LogErrorFormat ("Level未找到");
		}
		Transform foreGround_10 = level.transform.Find ("ForeGround_10");
		if (null == foreGround_10) {
			Debug.LogErrorFormat ("ForeGround_10未找到");
		}
		GameObject prefab = Instantiate (prefabs, foreGround_10.position, foreGround_10.rotation);
		prefab.transform.SetParent(foreGround_10);
		#endregion
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
