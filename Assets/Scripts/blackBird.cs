using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackBird : bird
{
    public List<pig> blocks = new List<pig>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Add(collision.gameObject.GetComponent<pig>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Remove(collision.gameObject.GetComponent<pig>());
        }
    }

    //重写黑色小鸟技能:点击爆炸
    public  override void ShowSkill()
    {
        base.ShowSkill();
        if (blocks.Count > 0 && blocks != null)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Dead();
            }
        }
        OnClean();
    }

    public void OnClean()
    {
        //让相机不跟随，小鸟不运动
        rg.velocity = Vector3.zero;
        //爆炸特效
        Instantiate(boom, transform.position, Quaternion.identity);
        //禁用小鸟
        render.enabled = false;
        //禁用circle collider 2d
        GetComponent<CircleCollider2D>().enabled = false;
        //清除拖尾效果
        myTrail.ClearTrails();
    }

    //重写
    protected override void Next()
    {
        GameManager._instance.birds.Remove(this);//移除小鸟
        Destroy(gameObject);//销毁对象
        //生成爆炸特效
        GameManager._instance.NextBird();
    }
}
