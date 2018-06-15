using UnityEngine;

#region 战斗中 移动基础
public class BtlMove {
    //速度
    public Vector2 speed = new Vector2(0, 0);
    //方向
    public Vector2 direction = new Vector2(0, 0);
    public Vector2 movement = new Vector2(0, 0);
    //移动轨迹
    public EnumMoveTrace moveTrace;
    //加速度
    //   public int acc;
    //是否暂停
    public BtlMove(){
        this.moveTrace = EnumMoveTrace.Line;
    }
    public void Clear(){
        this.speed = new Vector2(0, 0);
        this.direction = new Vector2(0, 0);
        this.movement = new Vector2(0, 0);
        this.moveTrace = EnumMoveTrace.Line;
    }
}
#endregion
