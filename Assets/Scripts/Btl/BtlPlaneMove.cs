using UnityEngine;

#region 战斗 飞机移动组件
public class BtlPlaneMove : MonoBehaviour {
    //组件归属
    public BtlPlane parent;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        BtlPlane plane = this.parent;
        if(plane.isUser()){
            return;
        }
        if (!plane.isVisble){
            Renderer renderer = gameObject.GetComponent<Renderer>();
            if (null == renderer){
                Debug.LogErrorFormat("BattlePlaneMove获取Renderer失败");
            }
            if (renderer.isVisibleExt(Camera.main)){
                //进入摄像头
                plane.isVisble = true;
            }
        } else{
            //离开镜头，销毁对象
            Renderer renderer = gameObject.GetComponent<Renderer>();
            if (null == renderer){
                Debug.LogErrorFormat("BattlePlaneMove获取Renderer失败");
            }
            if (!renderer.isVisibleExt(Camera.main)){
                plane.isVisble = false;
                Destroy(gameObject);
				Global.Instance.btlMgr.btlPlaneMgr.btlPlaneEnemyList.Remove (plane);
				//Debug.LogFormat ("plane cnt:{0}", Global.Instance.btlMgr.btlPlaneMgr.btlPlaneEnemyList.Count);
            }
        }
        BtlMove btlMove = plane.btlMove;
        switch (btlMove.moveTrace){
            case EnumMoveTrace.Line:
                btlMove.movement = new Vector2(btlMove.speed.x * btlMove.direction.x, btlMove.speed.y * btlMove.direction.y);
                break;
            default:
                Debug.LogErrorFormat("战机 移动类型{0}", btlMove.moveTrace);
                break;
        }
    }
    void FixedUpdate(){
        GetComponent<Rigidbody2D>().velocity = this.parent.btlMove.movement;
    }

}
#endregion