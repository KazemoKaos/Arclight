using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    LootManager lootTable;
    [HideInInspector] public SetShopPrice ssp;
    [SerializeField] GameObject itemSpawnLocation; //Empty game object. Use to hide instantiated items
    [SerializeField] Transform shopInventory; //shop inventory container in UI
    [SerializeField] GameObject shopItemPrefab; //The shop item prefab

    //Lists
    [HideInInspector] public List<Loot> shopItems; //List of SO (shopItems)
    List<GameObject> loadedItems; //List of Instantiated GO
    List<GameObject> shopButtons; //List shop buttons

    //UI Elements
    [Header("Textboxes")]
        [SerializeField] TextMeshProUGUI itemDescription;
        [SerializeField] GameObject weaponDescription;
    [Header("Player Money Text")]
        [SerializeField] TextMeshProUGUI playerMoney;
    [Header("Weapon Stats Text")]
        [SerializeField] TextMeshProUGUI weaponName;
        [SerializeField] TextMeshProUGUI cost;
        [SerializeField] TextMeshProUGUI damage;
        [SerializeField] TextMeshProUGUI reload;
        [SerializeField] TextMeshProUGUI ads;
        [SerializeField] TextMeshProUGUI ready;
        [SerializeField] TextMeshProUGUI recoil;
        [SerializeField] TextMeshProUGUI rof;
        [SerializeField] TextMeshProUGUI mag;
        [SerializeField] TextMeshProUGUI range;
        [SerializeField] TextMeshProUGUI ammoType;

    int maxInventory = 6; //Max items the shop can sell.
    [HideInInspector] public int selectedItem = -1;
    [HideInInspector] public int playerCurrency;

    private void Start()
    {
        lootTable = GetComponent<LootManager>();
        ssp = GetComponent<SetShopPrice>();
        shopItems = new List<Loot>();
        loadedItems = new List<GameObject>();
        shopButtons = new List<GameObject>();
    }

    public void initializeShop()
    {
        ssp.run(); //Get prices
        shopItems = lootTable.GetLootDrop(); //Get items to sell
        populateShop(); //Set up shop UI
    }

    void populateShop()
    {
        for (int i = 0; i < maxInventory; i++)
        {
            Loot si = shopItems[i];
            GameObject shopItem = Instantiate(shopItemPrefab, shopInventory); //Create shop item(Button) prefab in the shop inventory
            shopButtons.Add(shopItem);
            GameObject item = Instantiate(si.lootObject, itemSpawnLocation.transform); //Create item to gather info
            loadedItems.Add(item);
            
            //Assign loot attributes
            shopItem.transform.GetChild(0).GetComponent<Image>().sprite = si.lootSprite;
            shopItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = si.lootName;

            //Assign OnClick event
            int index = i; //Needs index because unity will make all of the buttons have the same i.
            shopItem.GetComponent<Button>().onClick.AddListener(() => buttonClick(si, item, index));
        }
        itemSpawnLocation.SetActive(false); //Hides the created objects
    }

    void buttonClick(Loot loot, GameObject item, int index)
    {
        selectedItem = index;

        //Remove any description and display the selected item description.
        clearDescription();
        if (loot.lootType == type.Weapon)
        {
            WeaponStatsUI statsUI = item.GetComponent<WeaponStatsUI>();
            weaponName.text = statsUI.weaponName.text;
            cost.text = ssp.getPrice(loot).ToString(); 
            damage.text = statsUI.damage.text;
            reload.text = statsUI.reload.text;
            ads.text = statsUI.ads.text;
            ready.text = statsUI.ready.text;
            recoil.text = statsUI.recoil.text;
            rof.text = statsUI.rof.text;
            mag.text = statsUI.mag.text;
            range.text = statsUI.weapon.range.ToString();
            ammoType.text = statsUI.weapon.GetAmmoTypeName();
            weaponDescription.SetActive(true);
        }
        if (loot.lootType == type.Item)
        {
            itemDescription.text = loot.lootName + "\n\n" +
                "Cost: $" + ssp.getPrice(loot).ToString() + "\n" +
                item.GetComponent<ItemPickup>().item.ItemDescription;
            itemDescription.gameObject.SetActive(true);
        }
    }

    public void clearDescription()
    {
        itemDescription.gameObject.SetActive(false);
        weaponDescription.SetActive(false);
    }

    public void successfulTransaction()
    {
        clearDescription();
        itemDescription.gameObject.SetActive(true);
        itemDescription.text = "\n\n\nSold!";
        loadedItems[selectedItem].gameObject.transform.parent = null; //Detach from parent
        loadedItems[selectedItem].gameObject.transform.position = 
            itemSpawnLocation.transform.position + Vector3.up * 100; //Places item on the floor.
        shopButtons[selectedItem].GetComponent<Button>().interactable = false; //Turn button off
        resetSelector(); //Prevents the player from buying an item that is bought.
    }
    public void resetSelector()
    {
        selectedItem = -1;
    }

    public void uPoorDescription()
    {
        clearDescription();
        itemDescription.gameObject.SetActive(true);
        itemDescription.text = "\n\n\nNot enough money!";
    }

    public void nothingSelectedDescription()
    {
        clearDescription();
        itemDescription.gameObject.SetActive(true);
        itemDescription.text = "\n\n\nNo item selected!";
    }

    void updateCurrency(int x)
    {
        playerCurrency = x;
        playerMoney.text = "$" + playerCurrency;
    }

    private void OnEnable()
    {
        PlayerCurrency.UpdateCurrencyUI += updateCurrency;
    }

    private void OnDisable()
    {
        PlayerCurrency.UpdateCurrencyUI -= updateCurrency;
    }
}
