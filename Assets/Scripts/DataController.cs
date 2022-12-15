using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataController : MonoBehaviour
{
    // static - 단 하나만 존재하지만 언제 어디서든 접근 가능  
    // 싱글톤 만들기 
    private static DataController instance;

    public static DataController GetInstance() 
    {
        // instance 가 없다면 
        if (instance == null) 
        {
            // 만들어 진 게 없는지 찾아보기 
            instance = FindObjectOfType<DataController>();

            // 찾아봐도 없으면 
            if(instance == null) 
            {
                // 하나 만들어라 
                GameObject container = new GameObject("DataController");
                instance = container.AddComponent<DataController>();
            }
        }
        return instance;
    }

    // 화면에 보여주기 위해 생성 
    private ItemButton[] itemButtons;


    // ������ �̾ �� �� �ֵ��� 
    private int m_gold = 0;
    private int m_goldPerClick = 0;
    private int m_lv = 1; //==
    private int m_level = 0;
    private int m_light = 1; // 0: green, 1: yellow, 2: red
    private int m_time = 0;

    void Awake()
    {
        // 데이터 유지하고 싶으면 아래 코드 주석 처리 
        PlayerPrefs.DeleteAll();
        // ������ ������ �� �ڵ����� ���� 
        // ���� �ʱ�ȭ�� ���⼭ �� 
        
        // key-value
        m_gold = PlayerPrefs.GetInt("Gold"); // ���� ���ٸ� 0
        m_goldPerClick = PlayerPrefs.GetInt("GoldPerClick", 1); // ���� ���ٸ� 1
        m_lv = PlayerPrefs.GetInt("Lv", 1);
        m_level = PlayerPrefs.GetInt("Level");
        m_light = PlayerPrefs.GetInt("Light", 1);
        m_time = PlayerPrefs.GetInt("Time"); 

        // ======= 시간 확인 
        StartCoroutine("sickDagom");
        
        itemButtons = FindObjectsOfType<ItemButton>();
    }


    IEnumerator sickDagom()
    {
        
        while (true) {
            // 종료하면 으로 변경하면 될 것 같다 
            // -> 시연할 땐 버튼을 누르면 종료되었다고 가정하기 
                // DataController.GetInstance().AddTime(goldPerSec);
                DataController.GetInstance().AddTime(1);
                print(m_time); // 일부러 안지움 - 확인용 
                int expMinus = 100*(2*m_lv + 1) / 10;
                // int expMinus = DataController.GetInstance().GetLevel() / 10;
                // if(m_time >= 259200) {
                if(m_time >= 10 ) {
                    if(DataController.GetInstance().GetLevel() > 0)
                        // 여기서 하루에 한번씩 감소될 수 있도록 변경해야 함 
                        SetLevel(DataController.GetInstance().GetLevel() - expMinus);
                    if(DataController.GetInstance().GetLevel() <= 0) {
                        SetLevel(0);
                        // 건강 신호등 변경 
                        DataController.GetInstance().SetLight(2);
                        // 경험치 감소 종료 
                        break;
                    }
                }
            yield return new WaitForSeconds(1.0f);
        }
    }
//==
    public void SetLv(int newLevel)
    {
        // m_level = newLevel - (100*(2*m_lv + 1));
        if (newLevel >= 100*(2*m_lv + 1)) {
            m_level = newLevel - (100*(2*m_lv + 1));
            m_lv++;
        }
        else if(newLevel == 0) {
            m_level = 0;
        }
        PlayerPrefs.SetInt("Lv", m_lv);
        PlayerPrefs.SetInt("Level", m_level); // exp
    }

    public void SetLvMedicine()
    {
        m_level = 50*(2*m_lv + 1); 
        
        PlayerPrefs.SetInt("Lv", m_lv);
        PlayerPrefs.SetInt("Level", m_level); // exp
    }

    public int GetLv ()
    {
        return m_lv;
    }
    
    public int GetLevel ()
    {
        return m_level;
    }

    public void SetLevel(int newLevel)
    {
        m_level= newLevel;
        PlayerPrefs.SetInt("Level", m_level);
    }

    public int GetLight ()
    {
        return m_light;
    }

    public void SetLight(int newLight)
    {
        m_light = newLight;
        PlayerPrefs.SetInt("Light", m_light);
    }



    //==

    public void SetGole(int newGold)
    {
        m_gold = newGold;
        PlayerPrefs.SetInt("Gold", m_gold);
    }

    public void AddGold (int newGold)
    {
        m_gold += newGold;
        SetGole(m_gold);
    }

    //==
    public void AddTime (int newTime)
    {
        m_time += newTime;
        SetTime(m_time);
    }
    public void SetTime(int newTime)
    {
        m_time = newTime;
        PlayerPrefs.SetInt("Time", m_time);
    }

    public void SubGold(int newGold)
    {
        m_gold -= newGold;
        SetGole(m_gold);
    }

    public int GetGold ()
    {
        return m_gold;
    }

    public int GetGoldPerClick()
    {
        return m_goldPerClick;
    }

    public void SetGoldPerClick (int newGoldPerClick)
    {
        m_goldPerClick = newGoldPerClick;
        PlayerPrefs.SetInt("GoldPerClick", m_goldPerClick);

    }

    public void AddGoldPerClick(int newGoldPerClick)
    {
        m_goldPerClick += newGoldPerClick;
        SetGoldPerClick(m_goldPerClick);
        // DataController.GetInstance().AddGoldPerClick(goldByUpgrade);
    }

    public void LoadUpgradeButton(UpgradeButton upgradeButton)
    {
        string key = upgradeButton.upgradeName;

        upgradeButton.level = PlayerPrefs.GetInt(key + "_level");
        upgradeButton.goldByUpgrade = PlayerPrefs.GetInt(key + "_goldByUpgrade", 
        upgradeButton.startGoldByUpgrade);
        upgradeButton.currentCost = PlayerPrefs.GetInt(key + "_cost", upgradeButton.startCurrentCost);

        //== 
        upgradeButton.lv = PlayerPrefs.GetInt(key + "_Lv");
    }

    public void SaveUpgradeButton(UpgradeButton upgradeButton)
    {
        string key = upgradeButton.upgradeName;

        PlayerPrefs.SetInt(key + "_level", upgradeButton.level);
        PlayerPrefs.SetInt(key + "_goldByUpgrade", upgradeButton.goldByUpgrade);
        PlayerPrefs.SetInt(key + "_cost", upgradeButton.currentCost);
        //==
        PlayerPrefs.SetInt(key + "_lv", upgradeButton.lv);
    }
        // 신호등 로직 
    public void LoadSupplementButton(SupplementButton supplementButton)
    {
        string key = supplementButton.upgradeName;

        supplementButton.light = PlayerPrefs.GetInt(key + "_light", 1); 
    }

    public void SaveSupplementButton(SupplementButton supplementButton)
    {
        string key = supplementButton.upgradeName;
        PlayerPrefs.SetInt(key + "_Light", supplementButton.light);
    }

    //== end

    public void LoadMedicineButton(MedicineButton medicineButton)
    {
        string key = medicineButton.upgradeName;

        medicineButton.level = PlayerPrefs.GetInt(key + "_level");
        medicineButton.goldByUpgrade = PlayerPrefs.GetInt(key + "_goldByUpgrade", 
        medicineButton.startGoldByUpgrade);
        medicineButton.currentCost = PlayerPrefs.GetInt(key + "_cost", medicineButton.startCurrentCost);
    }

    public void SaveMedicineButton(MedicineButton medicineButton)
    {
        string key = medicineButton.upgradeName;

        PlayerPrefs.SetInt(key + "_level", medicineButton.level);
        PlayerPrefs.SetInt(key + "_goldByUpgrade", medicineButton.goldByUpgrade);
        PlayerPrefs.SetInt(key + "_cost", medicineButton.currentCost);
    }

    public void LoadItemButton(ItemButton itemButton) 
    {
        string key = itemButton.itemName;

        itemButton.level = PlayerPrefs.GetInt(key+ "_level");
        itemButton.currentCost = PlayerPrefs.GetInt(key + "_cost", itemButton.startCurrentCost);
        itemButton.goldPerSec = PlayerPrefs.GetInt(key + "_goldPerSec");

        // 아이템 구매 여부 확인 
        if(PlayerPrefs.GetInt(key + "_isPurchased") == 1)
        {
            itemButton.isPurchased = true;
        }
        else 
        {
            itemButton.isPurchased = false;
        }
    }

    public void SaveItemButton (ItemButton itemButton)
    {
        string key = itemButton.itemName;

        PlayerPrefs.SetInt(key+ "_level", itemButton.level);
        PlayerPrefs.SetInt(key + "_cost", itemButton.currentCost);
        PlayerPrefs.SetInt(key + "_goldPerSec", itemButton.goldPerSec);

        // 아이템 구매 여부 확인 
        if(itemButton.isPurchased == true)
        {
            PlayerPrefs.SetInt(key + "_isPurchased", 1);
        }
        else 
        {
            PlayerPrefs.SetInt(key + "_isPurchased", 0);
        }
    }

    public int GetGoldPerSec() 
    {
        int goldPerSec = 0;
        for (int i = 0; i < itemButtons.Length; i++) 
        {
            goldPerSec += itemButtons[i].goldPerSec;
        }

        return goldPerSec;
    }
} 
