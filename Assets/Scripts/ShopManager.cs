using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public Text moneyText;

    void Awake()
    {
        if (instance != null) return;
        instance = this;
        moneyText.text = "MONEY : " + _money.ToString();
    }

    public int _money = 100;

    public void EarnLogic(int value)
    {
        _money += value;
        moneyText.text = "MONEY : " + _money.ToString();
    }

    public bool IsEnoughToBuy  (int price)
    {
        if (price <= _money) return true;
        return false;
    }

    public void BuyLogic (int price)
    {
            _money -= price;
        moneyText.text = "MONEY : " + _money.ToString();
    }
}
