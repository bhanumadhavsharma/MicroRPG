using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI inventoryText;
    public Image healthBarFill;
    public Image xpBarFill;
    public TextMeshProUGUI interactText;

    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        UpdateLevelText();
        UpdatePlayerHealth();
        UpdatePlayerXP();
        UpdateInventoryList();
    }

    void UpdateLevelText()
    {
        levelText.text = "Lvl\n" + player.curLevel;
    }

    void UpdatePlayerHealth()
    {
        healthBarFill.fillAmount = (float)player.curHp / (float)player.maxHp;
    }

    void UpdatePlayerXP()
    {
        xpBarFill.fillAmount = (float)player.curXp / (float)player.xpToNextLevel;
    }

    public void UpdateInventoryList()
    {
        inventoryText.text = "";
        foreach (string item in player.inventory)
        {
            inventoryText.text += item + "\n";
        }
    }

    public void SetInteractText(Vector3 pos, string text)
    {
        interactText.gameObject.SetActive(true);
        interactText.text = text;
        interactText.transform.position = Camera.main.WorldToScreenPoint(pos + Vector3.up);
    }

    public void DisableInteractText()
    {
        if (interactText.gameObject.activeInHierarchy)
        {
            interactText.gameObject.SetActive(false);
        }
    }
}
