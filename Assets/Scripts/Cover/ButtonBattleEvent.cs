using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button btn = this.GetComponent<Button> ();
		UIEventListener btnListener = btn.gameObject.AddComponent<UIEventListener> ();
		btnListener.OnClick += delegate(GameObject gb) {
			Debug.Log(gb.name + " OnClick");
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
