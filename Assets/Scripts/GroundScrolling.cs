using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//背景滚动
//支持从上向下循环滚动效果
public class GroundScrolling : MonoBehaviour {
	//背景名 列表
	public List<string> backGroundNameList;
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
		this.backGroundNameList = new List<string>();
		this.rendererList = new List<Transform> ();
	}
	// Use this for initialization
	void Start () {
		#region 应该在初始化场景时设置好具体参数 todo
		this.speed.y = 0.6f;

		#region 加载背景
		this.backGroundNameList.Add ("Prefabs/BackGround/forest_01");
		this.backGroundNameList.Add ("Prefabs/BackGround/forest_02");
		this.backGroundNameList.Add ("Prefabs/BackGround/forest_03");
		this.backGroundNameList.Add ("Prefabs/BackGround/forest_02");
		#endregion

		#endregion


		#region 创建背景
		float fixY = 0.0f;
		foreach (var data in this.backGroundNameList) {
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


        #region 加载飞机
        {
            string strPlaneName = "Prefabs/Plane/p_09d_0";
            GameObject planePrefabs = (GameObject)Resources.Load(strPlaneName);
            if (null == planePrefabs)
            {
                Debug.LogErrorFormat("GroundScrolling未找到{0}", strPlaneName);
            }

            GameObject level = GameObject.Find("Level");
            if (null == level)
            {
                Debug.LogErrorFormat("Level未找到");
            }
            Transform foreGround_10 = level.transform.Find("ForeGround_10");
            if (null == foreGround_10)
            {
                Debug.LogErrorFormat("ForeGround_10未找到");
            }

            Vector3 newPosition = foreGround_10.position;
            newPosition.x = 0;
            newPosition.y = -Camera.main.orthographicSize;

            Global.Instance.battleMgr.userPlaneGameObject = Instantiate(planePrefabs, newPosition, foreGround_10.rotation);
            Global.Instance.battleMgr.userPlaneGameObject.transform.SetParent(foreGround_10);

            //Global.Instance.battleMgr.userPlaneGameObject.transform.position = Vector3.Lerp(newPosition, foreGround_10.position, Time.time);
            Debug.LogFormat("Time.time:{0}", Time.time);
        }

        #endregion

    }

    // Update is called once per frame
    void Update () {
		if (!this.run) {
			return;
		}
        Vector3 newPosition = Global.Instance.battleMgr.userPlaneGameObject.transform.parent.position;
        newPosition.x = 0;
        newPosition.y = -Camera.main.orthographicSize/2;
        Global.Instance.battleMgr.userPlaneGameObject.transform.position = Vector3.Lerp(newPosition, Global.Instance.battleMgr.userPlaneGameObject.transform.parent.position, Time.time*0.3f);
        if(Vector3.Distance(newPosition, Global.Instance.battleMgr.userPlaneGameObject.transform.parent.position) < 1.0f)
        {
            Debug.LogFormat("gogogo:{0}", Time.time);
        }
       // Debug.LogFormat("Time.time:{0}", Time.time);

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
