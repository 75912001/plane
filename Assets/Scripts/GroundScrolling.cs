using System.Collections.Generic;
using UnityEngine;

//背景滚动
//支持从上向下循环滚动效果
public class GroundScrolling : MonoBehaviour {
	//速度
	public Vector2 speed = new Vector2(0, 0);
	//方向
	public Vector2 direction = new Vector2(0, -1);
	//拥有Renderer(渲染器)的 升序
	//按照position.y 进行升序排序
	private List<Transform> rendererList;

	bool run = true;

	//暂停
	public void Pause(){
		this.run = false;
	}
	//开始
	public void Run(){
		this.run = true;
	}

	void Awake(){
		this.rendererList = new List<Transform> ();
	}
	// Use this for initialization
	void Start () {
		#region 应该在初始化场景时设置好具体参数
		this.speed.y = 0.1f;
        #endregion

        #region 创建背景
        float fixY = 0.0f;
		foreach (var data in Global.Instance.btlMgr.btlBGMgr.bgNameList) {
			GameObject backGroundPrefabs = (GameObject)Resources.Load(data);
			if (null == backGroundPrefabs) {
				Debug.LogErrorFormat ("GroundScrolling未找到{0}",data);
			}

			#region 拼图
			float y = backGroundPrefabs.GetComponent<SpriteRenderer> ().bounds.size.y;
			Vector3 newPosition = transform.position;
			newPosition.y -= Camera.main.orthographicSize;
			newPosition.y += y/2;
			newPosition.y += fixY;
			fixY += y;
			#endregion

			GameObject prefab = Instantiate (backGroundPrefabs, newPosition, transform.rotation);
			prefab.transform.SetParent(transform);
		}
		#endregion

		for (int i = 0; i < transform.childCount; i++) {
			Transform child = transform.GetChild (i);
			Renderer renderer = child.gameObject.GetComponent<Renderer> ();
			if (null == renderer) {
				Debug.LogErrorFormat ("GroundScrolling未找到Renderer");
			} else {
				this.rendererList.Add (child);
			}
		}
		this.rendererList.Sort((x,y) => x.position.y.CompareTo(y.position.y));//position.y升序
        //this.childList.Sort((x, y) => -x.position.y.CompareTo(y.position.y));//position.y降序
    }

    // Update is called once per frame
    void Update () {
		if (!this.run) {
			return;
		}

        Vector3 movement = new Vector3 (this.speed.x * this.direction.x,
			this.speed.y * this.direction.y, 0);
		movement *= Time.deltaTime;
		transform.Translate (movement);
		if (0 < this.rendererList.Count) {
			Transform firstChild = this.rendererList[0];
			// Check if the child is already (partly) before the camera.
			// We test the position first because the IsVisibleFrom
			// method is a bit heavier to execute.
			if (firstChild.position.y < Camera.main.transform.position.y) {
				//如果子节点在摄像机之外，看是否完全在外面，并且移动到队列后面
				Renderer firstRenderer = firstChild.gameObject.GetComponent<Renderer> ();
				// Add only the visible children
				if (null != firstRenderer) {
					if (!firstRenderer.isVisibleExt (Camera.main)) {
						// Get the last child position.
						Transform lastChild = rendererList [rendererList.Count - 1];
						Renderer lastRenderer = lastChild.gameObject.GetComponent<Renderer> ();
						if (null != lastRenderer) {
							Vector3 lastSize = (lastRenderer.bounds.max - lastRenderer.bounds.min);
							Vector3 firstSize = (firstRenderer.bounds.max - firstRenderer.bounds.min);
							// Set the position of the recyled one to be AFTER
							// the last child.
							// Note: Only work for horizontal scrolling currently.
							firstChild.position = new Vector3 (firstChild.position.x, lastChild.transform.position.y+lastSize.y/2+firstSize.y/2, firstChild.position.z);

							// Set the recycled child to the last position
							// of the backgroundPart list.
							rendererList.Remove (firstChild);
							rendererList.Add (firstChild);
						} else {
							Debug.LogErrorFormat ("lastChild未找到Renderer");
						}
					}
				} else {
					Debug.LogErrorFormat ("firstChild未找到Renderer");
				}
			}
		}
	}
}
