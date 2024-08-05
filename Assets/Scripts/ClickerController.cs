using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickerController : MonoBehaviour
{
    private ClickerState clicker;

    [SerializeField]
    private TextMeshProUGUI moneyText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private TextMeshProUGUI upgradeText;
    [SerializeField]
    private TextMeshProUGUI clickText;
    [SerializeField]
    private Button clickButton;
    [SerializeField]
    private Button upgradeButton;
    [SerializeField]
    private BillPoolManager billPoolManager;

    // Start is called before the first frame update
    void Start()
    {
        clicker = new ClickerState();

        clickButton.onClick.AddListener(OnClick);
        upgradeButton.onClick.AddListener(OnUpgradeClick);

        if (billPoolManager == null)
        {
            TryGetComponent(out billPoolManager);
        }

        UpdateView();
    }

    private void UpdateView()
    {
        moneyText.text = clicker.Money.ToString();
        levelText.text = "LV " + clicker.Level;
        clickText.text = "+" + clicker.MoneyPerClick;
        upgradeButton.interactable = clicker.Money >= clicker.NextUpgradeCost;
        upgradeText.text = "UPGRADE " + clicker.NextUpgradeCost;
    }

    public void OnUpgradeClick()
    {
        clicker.Upgrade();
        UpdateView();
    }

    public void OnClick()
    {
        clicker.Click();
        billPoolManager.SpawnBill();
        UpdateView();
    }

}
