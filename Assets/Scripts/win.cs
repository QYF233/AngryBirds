using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour
{
    // 动画完成后显示星星
    public void Show(){
        GameManager._instance.ShowStars();
    }
}
