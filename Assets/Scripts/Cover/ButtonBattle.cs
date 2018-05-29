using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonBattleEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button btn = this.GetComponent<Button> ();
		UIEventListener btnListener = btn.gameObject.AddComponent<UIEventListener> ();
		btnListener.OnClick += delegate(GameObject gb) {
			Debug.Log(gb.name + " OnClick");

			Global.Instance.battleMgr.level = 1;
			SceneManager.LoadScene("Scenes/Battle");//切换到场景 
		};

		btnListener.OnMouseEnter += delegate(GameObject gb) {
			Debug.Log(gb.name + " OnMouseEnter");
		};

		btnListener.OnMouseExit += delegate(GameObject gb) {
			Debug.Log(gb.name + " OnMOuseExit");
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
