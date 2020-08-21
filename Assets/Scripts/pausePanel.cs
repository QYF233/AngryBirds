using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausePanel : MonoBehaviour
{
    private Animator animator;
    public GameObject button;//暂停按钮
    private void Awake()
    {
        animator = GetComponent<Animator>();//获取动画
    }


    // 流程 ： 1.点击暂停按钮——2.按钮消失——3.播放暂停动画（弹出面板）——4.游戏暂停——
    //          5.点击继续按钮——6.游戏继续——7.播放继续动画（面板消失）——8.出现暂停按钮——

    //点击重玩按钮
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    //1.点击暂停按钮
    public void Pause()
    {
        animator.SetBool("isPause", true);//3.播放暂停动画
        button.SetActive(false);//2.按钮消失
        if (GameManager._instance.birds.Count > 0)
        {
            if (!GameManager._instance.birds[0].isRelease)//如果没有飞出，不可进行操作。
            {
                GameManager._instance.birds[0].canMove = true;
            }
        }

    }
    public void Home()
    {
        SceneManager.LoadScene(1);
    }
    //5.点击继续按钮
    public void Resume()
    {
        Time.timeScale = 1;//6.游戏继续！！！！一定要先取消暂停
        animator.SetBool("isPause", false);//7.播放继续动画
    }

    // 暂停动画播放完调用
    public void PauseAnimEnd()
    {
        Time.timeScale = 0;//4.游戏暂停
    }

    // 动画播放完调用
    public void ResumeAnimEnd()
    {
        button.SetActive(true);//8.出现暂停按钮
    }
}
