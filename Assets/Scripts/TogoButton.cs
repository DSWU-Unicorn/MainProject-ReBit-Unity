using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogoButton : MonoBehaviour
{
    public int level = 0;
    public void OnClick() 
    {
        // int goldPerClick = DataController.GetInstance().GetGoldPerClick();
        int pointPerClick = 300;
        DataController.GetInstance().AddGold(pointPerClick);

        level = DataController.GetInstance().GetLevel() + 80; // 분리해 하면 80 경험치 증가
        DataController.GetInstance().SetLevel(level);
        DataController.GetInstance().SetLv(level);
    }
}
