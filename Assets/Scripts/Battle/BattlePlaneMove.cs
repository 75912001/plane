using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlaneMove : MonoBehaviour {
    //组件归属
    public BattlePlane parent;
    //速度
    public Vector2 speed = new Vector2(0,0);
    //方向
    public Vector2 direction = new Vector2(0,0);
    public Vector2 movement = new Vector2(0, 0);
    public EnumMoveTrace moveTrace;
    //加速度
    //   public int acc;
    //是否暂停
    // Use this for initialization
    void Start () {
        this.moveTrace = EnumMoveTrace.Line;
	}
	
	// Update is called once per frame
	void Update () {
        BattlePlane plane = this.parent;
        if(plane == Global.Instance.battleMgr.GetUserPlane()){
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
            }
        }
        switch (this.moveTrace){
            case EnumMoveTrace.Line:
                this.movement = new Vector2(this.speed.x * this.direction.x, this.speed.y * this.direction.y);
                break;
            default:
                Debug.LogErrorFormat("战机 移动类型{0}", this.moveTrace);
                break;
        }
    }
    void FixedUpdate(){
        GetComponent<Rigidbody2D>().velocity = this.movement;
    }
}
