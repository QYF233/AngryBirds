using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redBird : bird
{
 void Update()
    {
        //鼠标一直按下，进行位置跟随
        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // transform.position += new Vector3(0,0,10);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            //进行位置限定
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;//单位化向量
                pos *= maxDis;//控制最大长度
                transform.position = pos + rightPos.position;
            }
            Line();
        }

        //相机跟随
        float posX = transform.position.x;//小鸟的x
        Camera.main.transform.position =
            Vector3.Lerp(Camera.main.transform.position,
            new Vector3(Mathf.Clamp(posX, 0, 15), Camera.main.transform.position.y, Camera.main.transform.position.z),
            smooth * Time.deltaTime);
    }
}
