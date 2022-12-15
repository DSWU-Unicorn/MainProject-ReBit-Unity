using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    // public DataController dataController;
    public int level = 0;

    public void OnClick() 
    {
        // int goldPerClick = DataController.GetInstance().GetGoldPerClick();
        int pointPerClick = 200;
        DataController.GetInstance().AddGold(pointPerClick);

        
        level = DataController.GetInstance().GetLevel() + 50; // 분리해 50 경험치 증가
        DataController.GetInstance().SetLevel(level);
        DataController.GetInstance().SetLv(level);
    }

}
