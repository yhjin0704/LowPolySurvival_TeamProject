using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UICrafting : MonoBehaviour
{
    public RecipeSlot[] slots;
    public Transform dropPosition;

    public GameObject UIInventory; //인벤토리를 참조하기 위한 필드
    public GameObject recipeWindow;
    public Transform slotPanel;

    [Header("Selected Item")]
    private RecipeSlot selectedRecipe;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI RequiredMaterialName;
    public TextMeshProUGUI RequiredMaterialAmount;
    public TextMeshProUGUI ItemOptionsName;
    public TextMeshProUGUI ItemOptionsValue;
    public GameObject craftButton;

    private int curEquipIndex;

    private UIInventory inventory; //인벤토리 스크립트
    private PlayerController controller;
    private PlayerCondition condition;

    void Start()
    {
        inventory= UIInventory.GetComponent<UIInventory>();
        controller = PlayerManager.Instance.Player.controller;
        condition = PlayerManager.Instance.Player.condition;
        dropPosition = PlayerManager.Instance.Player.dropPosition;

        controller.inventory += Toggle;

        recipeWindow.SetActive(false);
        slots = new RecipeSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<RecipeSlot>();
            slots[i].index = i;
            slots[i].recipeMaker = this;
            slots[i].Set(); 
        }

        ClearSelectedItemWindow();
    }

    void ClearSelectedItemWindow()
    {
        selectedRecipe = null;

        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        ItemOptionsName.text = string.Empty;
        ItemOptionsValue.text = string.Empty;
        RequiredMaterialName.text = string.Empty;
        RequiredMaterialAmount.text = string.Empty;

        craftButton.SetActive(false);
    }

    public void Toggle()
    {
        Debug.Log("토글함수_제작UI");
        if (IsOpen())
        {
            recipeWindow.SetActive(false);
        }
        else
        {
            recipeWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return recipeWindow.activeInHierarchy;
    }

    // PlayerController 먼저 수정

    /// <summary>
    /// 제작한 아이템을 인벤토리에 추가하는 메서드
    /// </summary>
    public void AddCraftedItem()
    {
        ItemData data = selectedRecipe.ItemRecipe.desiredItem;

        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++;
                inventory.UpdateUI();
                PlayerManager.Instance.Player.itemData = null;
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            inventory.UpdateUI();
            PlayerManager.Instance.Player.itemData = null;
            return;
        }

        ThrowItem(data);
        PlayerManager.Instance.Player.itemData = null;
    }

    public void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    /// <summary>
    /// 만든 아이템이 겹쳐질 수 있는 아이템인지 확인하고 UIInventory에 할당하기 위한 메서드
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].item == data && inventory.slots[i].quantity < data.maxStackAmount)
            {
                return inventory.slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].item == null)
            {
                return inventory.slots[i];
            }
        }
        return null;
    }


    // ItemSlot 스크립트 먼저 수정
    public void SelectItem(int index)
    {
        if (slots[index].ItemRecipe == null) return;

        selectedRecipe = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedRecipe.ItemRecipe.desiredItem.displayName;
        selectedItemDescription.text = selectedRecipe.ItemRecipe.desiredItem.description;

        RequiredMaterialName.text = string.Empty;
        RequiredMaterialAmount.text = string.Empty;

        if (0 < selectedRecipe.ItemRecipe.desiredItem.consumables.Length)
        {
            for (int i = 0; i < selectedRecipe.ItemRecipe.desiredItem.consumables.Length; i++)
            {
                ItemOptionsName.text += selectedRecipe.ItemRecipe.desiredItem.consumables[i].type.ToString() + "\n";
                ItemOptionsValue.text += selectedRecipe.ItemRecipe.desiredItem.consumables[i].value.ToString() + "\n";
            }
        }
        else if(selectedRecipe.ItemRecipe.desiredItem.equipPrefab!=null)
        {
            ItemOptionsName.text = "Damage";
            ItemOptionsValue.text = selectedRecipe.ItemRecipe.desiredItem.damage.ToString() + "\n";
        }
        else
        {
            ItemOptionsName.text = string.Empty;
            ItemOptionsValue.text = string.Empty;
        }

        for (int i = 0; i < selectedRecipe.ItemRecipe.requiredItems.Length; i++)
        {
            RequiredMaterialName.text += selectedRecipe.ItemRecipe.requiredItems[i].materials.displayName.ToString() + "\n";
            RequiredMaterialAmount.text += selectedRecipe.ItemRecipe.requiredItems[i].amount.ToString() + "\n";
        }
        craftButton.SetActive(true);
    }

    public void OnCraftButton()
    {
        if(HasMaterials())
        {
            RemoveMaterials();
            AddCraftedItem();
        }
        else
        {
            Debug.Log("제작불가, 아이템이 없습니다.");
        }
    }

    /// <summary>
    /// 제작에 쓰인 재료를 차감하는 메서드
    /// </summary>
    public void RemoveMaterials()
    {
        ItemRecipe recipe = selectedRecipe.ItemRecipe;
        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            int amountToRemove = recipe.requiredItems[i].amount;
            for (int j = 0; j < inventory.slots.Length; j++)
            {
                //인벤토리의 자원이 제작 레시피의 재료가 맞는지 확인한 뒤
                if (inventory.slots[j].item == recipe.requiredItems[i].materials)
                {
                    //만약 한 슬롯 안에 충분한 자원이 있다면
                    if (inventory.slots[j].quantity >= amountToRemove)
                    {
                        //필요한 양만큼 차감
                        inventory.slots[j].quantity -= amountToRemove;
                        //이후, 슬롯의 양이 0보다 작거나 같다면 슬롯 초기화 (재료로 들어간 장비를 착용중이었던 경우, 장착 해제)
                        if (inventory.slots[j].quantity <= 0)
                        {
                            if (inventory.slots[j].equipped)
                            {
                                Debug.Log("장착해제");
                                //장착 해제 메서드
                            }

                            inventory.slots[j].item = null;
                        }
                        //이 자원은 전부 냈으니, 다음 자원으로 넘어가기
                        break;
                    }
                    else //만약 한 슬롯에 충분하지 않은 자원이 있다면
                    {
                        //필요한 자원을 슬롯에 들어있는 양만큼 차감 (재료로 들어간 장비를 착용중이었던 경우 장착 해제)
                        amountToRemove -= inventory.slots[j].quantity;
                        if (inventory.slots[j].equipped)
                        {
                            Debug.Log("장착해제");
                            //장착 해제 메서드
                        }
                        inventory.slots[j].item = null;
                        inventory.slots[j].quantity = 0;

                        if (amountToRemove == 0)
                        {
                            break;
                        }
                    }
                }
            }
        }
        inventory.UpdateUI();
    }

    /// <summary>
    /// 제작에 필요한 자원이 UIInventory에 충분히 있는지 확인하는 메서드
    /// </summary>
    /// <returns></returns>
    public bool HasMaterials()
    {
        //재료를 찾아야 할 레시피를 지정하고
        ItemRecipe recipe = selectedRecipe.ItemRecipe;
        for (int i = 0; i < recipe.requiredItems.Length; i++) //레시피 안에 들어있는 재료 종류만큼 반복문 시작
        {
            //찾았다는 표시를 하기 위한 불 값과 제작에 필요한 양을 선언하고
            bool Found = false;
            int amountToRemove = recipe.requiredItems[i].amount;
            for (int j = 0; j < inventory.slots.Length; j++) //인벤토리의 슬롯 수만큼 반복문 시작
            {
                //인벤토리의 자원이 제작 레시피의 재료가 맞는지 확인한 뒤
                if (inventory.slots[j].item == recipe.requiredItems[i].materials)
                {
                    //만약 한 슬롯 안에 충분한 자원이 있다면
                    if (inventory.slots[j].quantity >= amountToRemove)
                    {
                        Found = true;
                    }
                    else //만약 한 슬롯에 충분하지 않은 자원이 있다면
                    {
                        amountToRemove -= inventory.slots[j].quantity;
                        if (amountToRemove == 0)
                        {
                            Found = true;
                        }
                    }
                }
            }
            //없다면 false 반환
            if (!Found)
            {
                return false;
            }
        }
        //전부 있다면(Found=true) true 반환
        return true;
    }
}
