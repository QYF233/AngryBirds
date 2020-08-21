using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenBird : bird
{
 //重写绿色小鸟技能:回旋
    public override void ShowSkill()
    {
        base.ShowSkill();
        Vector3 speed = rg.velocity;
        speed.x *= -1;
        rg.velocity = speed;
    }

}
