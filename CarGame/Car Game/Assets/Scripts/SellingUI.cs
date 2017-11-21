using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingUI : MonoBehaviour
{
    [Header("Data")]
    public Sprite[] Currencies;
    public string[] Titles;
    public string[] MainBodies;


    [Header("UI Refs")]
    public Image CurrencyIcon;
    public Image CurrencyIconBottom;
    public Text Title;
    public Text MainBody;
    public Text CurrencyLose;
    public Text CashGain;

    private int amt;
    private int gain;
    private CollectibleObject.CollectibleType thisType;

    public void ShowSellingUI(CollectibleObject.CollectibleType type, int PerObject, int UpFront)
    {
        this.gameObject.SetActive(true);


        thisType = type;
        switch (type)
        {

            case CollectibleObject.CollectibleType.Gold:
                CurrencyIcon.sprite = CurrencyIconBottom.sprite = Currencies[0];
                Title.text = Titles[0];
                MainBody.text = string.Format(MainBodies[0], PerObject.ToString(), UpFront.ToString());
                break;
            case CollectibleObject.CollectibleType.Cadaver:
                CurrencyIcon.sprite = CurrencyIconBottom.sprite = Currencies[1];
                Title.text = Titles[1];
                MainBody.text = string.Format(MainBodies[1], PerObject.ToString(), UpFront.ToString());
                break;
            case CollectibleObject.CollectibleType.Scrap:
                CurrencyIcon.sprite = CurrencyIconBottom.sprite = Currencies[2];
                Title.text = Titles[2];
                MainBody.text = string.Format(MainBodies[2], PerObject.ToString(), UpFront.ToString());
                break;
        }


        amt = StaticWorld.instance.PlayerData.GetCurrencyForType(type);

        CurrencyLose.text = amt.ToString();

        gain = (PerObject * amt) - UpFront;

        CashGain.text = gain.ToString();





    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoIt();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Bail();
        }
    }

    private void DoIt()
    {
        StaticWorld.instance.PlayerData.CashTransaction(gain);
        StaticWorld.instance.PlayerData.CurrencyTransaction(-amt, thisType);
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;


    }

    private void Bail()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
