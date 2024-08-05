using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerState
{
    public ulong Money { get; private set; }
    public ulong NextUpgradeCost { get; private set; }
    public int Level { get; private set; }
    public ulong MoneyPerClick { get; private set; }

    public ClickerState()
    {
        Money = 0;
        Level = 1;
        MoneyPerClick = CalculateMoneyPerClick(Level);
        NextUpgradeCost = CalculateUpgradeCost(Level);
    }

    public void Click()
    {
        Money += MoneyPerClick;
    }

    public void Upgrade()
    {
        if (Money >= NextUpgradeCost)
        {
            Money -= NextUpgradeCost;
        }

        Level++;
        MoneyPerClick = CalculateMoneyPerClick(Level);
        NextUpgradeCost = CalculateUpgradeCost(Level);
    }

    private ulong CalculateMoneyPerClick(int level)
    {
        if (level <= 5)
        {
            return (ulong)level;
        }
        return (ulong)(2 * level);
    }

    private ulong CalculateUpgradeCost(int level)
    {
        if (level == 1)
        {
            return 10;
        }
        return (ulong)(Math.Pow(1.5, level) * 10);
    }
}
