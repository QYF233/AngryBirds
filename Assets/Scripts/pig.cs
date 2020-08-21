using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pig : MonoBehaviour
{

    public float maxSpeed = 10;
    public float minSpeed = 5;
    private SpriteRenderer render;
    public Sprite hurt;//受伤图片
    public GameObject boom;
    public GameObject score;
    public bool isPig = false;
    public AudioClip hurtClip;
    public AudioClip dead;
    public AudioClip birdCollision;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>(); //赋值
    }

    //碰撞检测OnCollisionEnter2D    触发检测OnTriggerEnter2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudoPlay(birdCollision);
            collision.transform.GetComponent<bird>().Hurt();//碰撞让小鸟受伤
        }


        //判断碰撞时的速度,大于最大速度直接死亡，速度合适,让猪受伤，
        if (collision.relativeVelocity.magnitude > maxSpeed)
        {//直接死亡
            Dead();
        }
        else if (collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed)
        {
            render.sprite = hurt;//切换受伤猪
            AudoPlay(hurtClip);
        }
    }

    public void Dead()
    {
        if (isPig)
        {//猪死，移除
            GameManager._instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);

        GameObject go = Instantiate(score, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(go, 1.5f);
        AudoPlay(dead);
    }

    //播放音乐
    public void AudoPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

}
