using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yellowBird : bird
{
    //重写小鸟技能：点击左键加速
    public override void ShowSkill()
    {
        base.ShowSkill();
        rg.velocity *= 2;
    }
}
