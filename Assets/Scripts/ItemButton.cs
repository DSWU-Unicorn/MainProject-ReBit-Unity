using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{

    // UI 갱신시 필요 - using UnityEngine.UI; 추가 
    public Text itemDisplayer;

    public string itemName;
    
    // 아이템 레벨
    // 구매 레밸이기에 0에서 시작 
    public int level = 0;

    public int time = 0;
    [HideInInspector]
    // 현재 아이템 가격 
    public int currentCost;

    // 아이템 시작 가격
    public int startCurrentCost = 1;

    [HideInInspector]
    // 초당 얼마나 가격을 인상할지
    // -> 초당 얼마나 아플지... 
    public int goldPerSec;

    // 3일당 얼마나 exp 감소시킬지 
    public int startGoldPerSec = 3;

    public float costPow = 3.14f;

    public float upgradePow = 1.07f;

    // 아이템 구매 여부 
    [HideInInspector]
    public bool isPurchased = false; 

    void Start() 
    {
        /* 
        // 데이터 저장으로 삭제 - DataController.cs 에 함수 추가
        currentCost = startCurrentCost;
        goldPerSec = startGoldPerSec;
        */
        DataController.GetInstance().LoadItemButton(this);
        StartCoroutine("AddGoldLoop");
        UpdateUI();
    }

    public void PurchaseItem()
    {
        if(DataController.GetInstance().GetGold() >= currentCost)
        {
            isPurchased = true; // 종료되었으면 
            // DataController.GetInstance().SubGold(currentCost);
            // level++;

            // UpdateItem();
            UpdateUI();

            DataController.GetInstance().SaveItemButton(this);
        }
    }

    // 초당 골드가 증가하도록
    // 지연시간 사용 
    IEnumerator AddGoldLoop()
    {
        while (true) {
            // 종료하면 으로 변경하면 될 것 같다 
            // -> 시연할 땐 버튼을 누르면 종료되었다고 가정하기 
            if(isPurchased)
            {
                DataController.GetInstance().AddTime(goldPerSec);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    public void UpdateItem()
    {
        // 여기에 작성한 수식은 알아서 밸런스 맞춰서 변경하면 된다 
        goldPerSec = goldPerSec + startGoldPerSec * (int) Mathf.Pow(upgradePow, level);
        currentCost = startCurrentCost * (int)Mathf.Pow(costPow, level);
    }

    public void UpdateUI()
    {
        itemDisplayer.text = itemName +"\ntime: " + time + "\nGold Per Sec: "
        + goldPerSec + "\nIsPurchased: " + isPurchased;
    }
}
