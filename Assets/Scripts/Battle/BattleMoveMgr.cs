using UnityEngine;
public class BattleMove
{
    //速度
    public Vector2 speed = new Vector2(0,0);
    //方向
    public int direction;
    //加速度
    public int acc;
    //是否暂停
}

public class BattleMoveMgr {
    public BattleMove battleMoveUser;
    public BattleMoveMgr(){
        this.battleMoveUser = new BattleMove();
    }

}
