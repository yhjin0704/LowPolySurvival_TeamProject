using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UICrafting : MonoBehaviour
{
    public RecipeSlot[] slots;
    public Transform dropPosition;

    public GameObject UIInventory; //�κ��丮�� �����ϱ� ���� �ʵ�
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

    private UIInventory inventory; //�κ��丮 ��ũ��Ʈ
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
        Debug.Log("����Լ�_����UI");
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

    // PlayerController ���� ����

    /// <summary>
    /// ������ �������� �κ��丮�� �߰��ϴ� �޼���
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
    /// ���� �������� ������ �� �ִ� ���������� Ȯ���ϰ� UIInventory�� �Ҵ��ϱ� ���� �޼���
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


    // ItemSlot ��ũ��Ʈ ���� ����
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
            Debug.Log("���ۺҰ�, �������� �����ϴ�.");
        }
    }

    /// <summary>
    /// ���ۿ� ���� ��Ḧ �����ϴ� �޼���
    /// </summary>
    public void RemoveMaterials()
    {
        ItemRecipe recipe = selectedRecipe.ItemRecipe;
        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            int amountToRemove = recipe.requiredItems[i].amount;
            for (int j = 0; j < inventory.slots.Length; j++)
            {
                //�κ��丮�� �ڿ��� ���� �������� ��ᰡ �´��� Ȯ���� ��
                if (inventory.slots[j].item == recipe.requiredItems[i].materials)
                {
                    //���� �� ���� �ȿ� ����� �ڿ��� �ִٸ�
                    if (inventory.slots[j].quantity >= amountToRemove)
                    {
                        //�ʿ��� �縸ŭ ����
                        inventory.slots[j].quantity -= amountToRemove;
                        //����, ������ ���� 0���� �۰ų� ���ٸ� ���� �ʱ�ȭ (���� �� ��� �������̾��� ���, ���� ����)
                        if (inventory.slots[j].quantity <= 0)
                        {
                            if (inventory.slots[j].equipped)
                            {
                                Debug.Log("��������");
                                //���� ���� �޼���
                            }

                            inventory.slots[j].item = null;
                        }
                        //�� �ڿ��� ���� ������, ���� �ڿ����� �Ѿ��
                        break;
                    }
                    else //���� �� ���Կ� ������� ���� �ڿ��� �ִٸ�
                    {
                        //�ʿ��� �ڿ��� ���Կ� ����ִ� �縸ŭ ���� (���� �� ��� �������̾��� ��� ���� ����)
                        amountToRemove -= inventory.slots[j].quantity;
                        if (inventory.slots[j].equipped)
                        {
                            Debug.Log("��������");
                            //���� ���� �޼���
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
    /// ���ۿ� �ʿ��� �ڿ��� UIInventory�� ����� �ִ��� Ȯ���ϴ� �޼���
    /// </summary>
    /// <returns></returns>
    public bool HasMaterials()
    {
        //��Ḧ ã�ƾ� �� �����Ǹ� �����ϰ�
        ItemRecipe recipe = selectedRecipe.ItemRecipe;
        for (int i = 0; i < recipe.requiredItems.Length; i++) //������ �ȿ� ����ִ� ��� ������ŭ �ݺ��� ����
        {
            //ã�Ҵٴ� ǥ�ø� �ϱ� ���� �� ���� ���ۿ� �ʿ��� ���� �����ϰ�
            bool Found = false;
            int amountToRemove = recipe.requiredItems[i].amount;
            for (int j = 0; j < inventory.slots.Length; j++) //�κ��丮�� ���� ����ŭ �ݺ��� ����
            {
                //�κ��丮�� �ڿ��� ���� �������� ��ᰡ �´��� Ȯ���� ��
                if (inventory.slots[j].item == recipe.requiredItems[i].materials)
                {
                    //���� �� ���� �ȿ� ����� �ڿ��� �ִٸ�
                    if (inventory.slots[j].quantity >= amountToRemove)
                    {
                        Found = true;
                    }
                    else //���� �� ���Կ� ������� ���� �ڿ��� �ִٸ�
                    {
                        amountToRemove -= inventory.slots[j].quantity;
                        if (amountToRemove == 0)
                        {
                            Found = true;
                        }
                    }
                }
            }
            //���ٸ� false ��ȯ
            if (!Found)
            {
                return false;
            }
        }
        //���� �ִٸ�(Found=true) true ��ȯ
        return true;
    }
}
