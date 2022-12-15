using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    // upgrade 내용을 화면에 띄우는 역할 
    public Text upgradeDisplayer; 

    public string upgradeName;

    [HideInInspector]  
    // public 인데 unity의 inspector에서는 보이지 않아 수정되지 않음 
    public int goldByUpgrade = 0;

    public int startGoldByUpgrade = 300;

    [HideInInspector]
    // 지금 현재의 아이템 가격
    public int currentCost = 300;

    public int startCurrentCost = 300;

    // level 을 exp라고 생각해야 함 
    // ===== 나중에 변경하기 
    public int level = 0;
    public int lv = 0;

    // upgrade 할 때 마다 가격과 증가폭을 높여 밸런스를 맞추는 역할 
    // upgrade 는 1.07승씩 증가 
    public float upgradePow = 1.07f;

    // cost는 3.14승씩 증가
    public float costPow = 3.14f;

    // public으로 레퍼런스를 가져오려면 매번 유니티 창에서 드래그 드롭으로 값을 할당해줘야 함 
    // 근데 싱글톤 사용하면 그걸 안할 수 있음 
        // 싱글톤 - 단 하나만 존재하며, 언제 어디서든 접근 가능하게 하는 것 -> DataController.cs 최상단에 instance 부분 보기 
    // public DataController dataController;


    // start는 awake 보다 느림 
    // 시작할 때 사용한다는 건 동일 
    void Start() { 
        /*
        // dataController 사용해서 주석처리
        currentCost = startCurrentCost;
        level = 1;
        goldByUpgrade = startGoldByUpgrade;
        */
        // 시작할 때 저장된 데이터를 가져옴
        // level = 0;
        DataController.GetInstance().LoadUpgradeButton(this);
        UpdateUI();
    }

    public void PurchaseUpgrade() {
        // 싱글톤이기에 해당 변수를 선언하지 않고 클래스 이름에 dot을 찍어 접근할 수 있음 
        if(DataController.GetInstance().GetGold() >= 300) // gold가 충분하면
        {
            // level = DataController.GetInstance().GetLevel;
            // lv = DataController.GetInstance().GetLv;

            // DataController.GetInstance().SetLevel(level);
            // DataController.GetInstance().SetLv(lv);

            DataController.GetInstance().SubGold(300);
            level = DataController.GetInstance().GetLevel() + 150; // 콜라 한번먹으면 150 경험치 증가
            DataController.GetInstance().SetLevel(level);
            DataController.GetInstance().SetLv(level);
            // 가격과 한번에 사용되는 양을 증가시킨다 
            // UpdateUpgrade();
            // 구매에 성공했으면 ui도 갱신 
            UpdateUI();
            // 아이템을 구매했을 때 저장함
            DataController.GetInstance().SaveUpgradeButton(this);
            
        }
    }

    public void UpdateUpgrade() {
        goldByUpgrade = startGoldByUpgrade * (int)Mathf.Pow(upgradePow, level);
        currentCost = startCurrentCost * (int) Mathf.Pow(costPow, level);
    }

    public void UpdateUI()
    {
        // upgradeDisplayer.text = "콜라 : 300원 \n경험치 150 상승\nexp: " + level;
                //upgradeDisplayer.text = "콜라 : 300원";

        // upgradeDisplayer.text = upgradeName + "\nCost: " + currentCost + "\nLevel: " + level + "\nNext New GoldPerClick: " + goldByUpgrade;
    }
}
