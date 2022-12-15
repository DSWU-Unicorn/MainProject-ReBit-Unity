using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text goldDisplayer;
    // public DataController dataController;
    public Text goldPerClickDisplayer;
    public Text goldPerSecDisplayer;
    public Text LightDisplayer;

    //==
    //== 
    public Text light;

    void Update() {
        // 보통 게임은 초당 30~60번 정도 화면을 갱신함 
        // goldDisplayer.text = "GOLD: " + dataController.GetGold();
        goldDisplayer.text = "" + DataController.GetInstance().GetGold();
        goldPerClickDisplayer.text = "LEVEL: " + DataController.GetInstance().GetLv();
        goldPerSecDisplayer.text = "" +  DataController.GetInstance().GetLevel(); //exp
        LightDisplayer.text = "Light: " +  DataController.GetInstance().GetLight();
    }
}
