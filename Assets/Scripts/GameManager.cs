using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<bird> birds;
    public List<pig> pigs;
    public static GameManager _instance;
    private Vector3 orgainPos;//初始位置
    public GameObject win;
    public GameObject lose;
    public GameObject[] stars;//星星数组
    private int totalNum = 10;//关卡总数
    public Text levelText;//关卡
    public int starsNum;//星星数组


    private void Awake()
    {
        _instance = this;
        if (birds.Count > 0)
        {
            orgainPos = birds[0].transform.position;
        }

    }

    //游戏开始调用初始化
    public void Start()
    {
        //  PlayerPrefs.DeleteAll();
        levelText.text = "第" + System.Text.RegularExpressions.Regex.Replace(PlayerPrefs.GetString("nowLevel"), @"[^0-9]+", "") + "关";
        Initialized();

    }
    // 初始化小鸟
    private void Initialized()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {//第一只小鸟
                birds[i].transform.position = orgainPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
                birds[i].canMove = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
                birds[i].canMove = false;
            }
        }
    }

    public void NextBird()
    {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                //进行下一只小鸟
                Initialized();
            }
            else
            {
                //输了
                lose.SetActive(true);
            }
        }
        else
        {
            //赢了
            win.SetActive(true);
        }
    }

    public void ShowStars()
    {
        StartCoroutine("show");
    }
    IEnumerator show()
    {
        for (; starsNum < birds.Count + 1; starsNum++)
        {
            if (starsNum >= stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);
        }

    }

    public void Replay()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }
    public void NextLevel()
    {
        SaveData();
        //     int nextLevel = int.Parse(PlayerPrefs.GetString("nowLevel").Substring(5, 1)) + 1;//获取下一关的关卡名。
        string result = System.Text.RegularExpressions.Regex.Replace(PlayerPrefs.GetString("nowLevel"), @"[^0-9]+", "");
        int nextLevel = int.Parse(result) + 1;//获取下一关的关卡名。

        print("---" + nextLevel);
        PlayerPrefs.SetString("nowLevel", "level" + nextLevel.ToString());//复制给nowLevel
        SceneManager.LoadScene(2);
    }
    public void SaveData()
    {
        // print("当前星星数"+starsNum+"********以前星星数"+PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel"), starsNum)+"***星星总数"+totalNum);
        if (starsNum >= PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel"), starsNum))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
        }
        //存储所有的星星个数
        int sum = 0;
        for (int i = 1; i < totalNum; i++)
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
        // print("sum"+sum);
        PlayerPrefs.SetInt("totalNum", sum);
        //  print("totalNum"+PlayerPrefs.GetInt("totalNum",sum));
    }
}
