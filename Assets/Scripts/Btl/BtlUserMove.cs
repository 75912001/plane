using UnityEngine;

#region 战斗中用户操作移动组件
public class BtlUserMove : MonoBehaviour {
    //组件归属
    public BtlPlane parent;
    // Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        this.parent.btlMove.movement = new Vector2(inputX* this.parent.btlMove.speed.x, inputY* this.parent.btlMove.speed.x);

        #region 防止移动出摄像机范围 
        {
            var dist = (transform.position - Camera.main.transform.position).z;
            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
                Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
                transform.position.z);
        }
        #endregion
    }

    void FixedUpdate(){
        //移动
        GetComponent<Rigidbody2D>().velocity = this.parent.btlMove.movement;
    }
}
#endregion
