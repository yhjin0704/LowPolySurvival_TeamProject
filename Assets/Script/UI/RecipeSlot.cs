using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
    public ItemRecipe ItemRecipe;

    public UICrafting recipeMaker;
    public Button button;
    public Image icon;
    public TextMeshProUGUI quatityText;
    private Outline outline;

    public int index;
    public bool equipped;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = equipped;
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = ItemRecipe.desiredItem.icon;
        quatityText.text = ItemRecipe.quantities > 1 ? $"x{ItemRecipe.quantities}" : string.Empty;
    }

    public void OnClickButton()
    {
        recipeMaker.SelectItem(index);
    }
}
