using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class mapSelect : MonoBehaviour
{
    public int starsNum = 0;
    public bool isSelect = false;
    public GameObject locks;
    public GameObject stars;
    public GameObject panel;
    public GameObject map;
    public Text startsText;//星星数

    public int startNum = 1;//开始的关卡数
    public int endNum = 4;
    private void Start()
    {
        //清楚游戏分数！谨慎使用、
        // PlayerPrefs.DeleteAll();

        if (PlayerPrefs.GetInt("totalNum", 0) >= starsNum)
        {
            isSelect = true;
        }
        if (isSelect)
        {
            locks.SetActive(false);
            stars.SetActive(true);

            int counts = 0;
            //获得的星星数
            for (int i = startNum; i <= endNum; i++)
            {
                counts += PlayerPrefs.GetInt("level"+i.ToString(),0);
            }
            startsText.text = counts.ToString() + "/15";

        }
    }

    // 鼠标点击
    public void Selected()
    {
        if (isSelect)
        {
            panel.SetActive(true);
            map.SetActive(false);
        }
    }
    //返回map选择
    public void PanelSelect()
    {
        panel.SetActive(false);
        map.SetActive(true);
    }



}

