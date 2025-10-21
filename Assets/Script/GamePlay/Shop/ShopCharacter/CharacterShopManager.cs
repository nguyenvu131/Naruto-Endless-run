using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShopManager : MonoBehaviour {

	 public static CharacterShopManager instance;

    public List<CharacterData> characters;   // Danh sách nhân vật
    public Transform contentPanel;           // Nơi chứa button nhân vật
    public GameObject characterButtonPrefab; // Prefab UI button nhân vật

    private int selectedCharacter = 0;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        LoadCharacters();
        CreateShopUI();
    }

    void LoadCharacters()
    {
        // Load trạng thái unlock từ PlayerPrefs
        for (int i = 0; i < characters.Count; i++)
        {
            if (i == 0)
            {
                characters[i].isUnlocked = true; // Nhân vật mặc định free
            }
            else
            {
                int unlocked = PlayerPrefs.GetInt("Character_" + characters[i].characterID, 0);
                characters[i].isUnlocked = unlocked == 1;
            }
        }

        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
    }

    void CreateShopUI()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < characters.Count; i++)
        {
            int index = i;
            GameObject newButton = Instantiate(characterButtonPrefab, contentPanel);
            newButton.transform.Find("Name").GetComponent<Text>().text = characters[i].characterName;
            newButton.transform.Find("Icon").GetComponent<Image>().sprite = characters[i].characterSprite;

            Button buyButton = newButton.transform.Find("BuyButton").GetComponent<Button>();
            Text buyText = buyButton.transform.Find("Text").GetComponent<Text>();

            if (characters[i].isUnlocked)
            {
                buyText.text = (selectedCharacter == i) ? "Selected" : "Select";
                buyButton.onClick.AddListener(delegate { SelectCharacter(index); });
            }
            else
            {
                buyText.text = "Buy " + characters[i].price;
                buyButton.onClick.AddListener(delegate { BuyCharacter(index); });
            }
        }
    }

    public void BuyCharacter(int index)
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);

        if (coins >= characters[index].price)
        {
            coins -= characters[index].price;
            PlayerPrefs.SetInt("Coins", coins);

            characters[index].isUnlocked = true;
            PlayerPrefs.SetInt("Character_" + characters[index].characterID, 1);

            SelectCharacter(index);
            CreateShopUI();
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public void SelectCharacter(int index)
    {
        selectedCharacter = index;
        PlayerPrefs.SetInt("SelectedCharacter", index);
        CreateShopUI();
    }

    public int GetSelectedCharacter()
    {
        return selectedCharacter;
    }
}
