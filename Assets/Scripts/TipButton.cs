using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipButton : MonoBehaviour
{
    public int level = 0; // 경험치 
    public void OnClick() 
    {
        // int goldPerClick = DataController.GetInstance().GetGoldPerClick();
        int pointPerClick = 100;
        DataController.GetInstance().AddGold(pointPerClick);

        level = DataController.GetInstance().GetLevel() + 25; // 오늘의 팁 보면 25 경험치 증가
        DataController.GetInstance().SetLevel(level);
        DataController.GetInstance().SetLv(level);
    }
}
