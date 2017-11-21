using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public  class StaticWorld : MonoBehaviour
{

    public static StaticWorld instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }





    public GibPooler Gibs;
    public CollectiblePooler Collectibles;

    public UIController mUI;

    public PlayerData PlayerData;

    public SellingUI Seller;

    [Header("Timer")]
    public Text m_Timer;
    public GradientText m_Grad;
    public Color GreenTop;
    public Color GreenBottom;
    public Color YellowTop;
    public Color YellowBottom;
    public Color RedTop;
    public Color RedBottom;

    private float m_LevelDuration = 20.0f;
    private float minutes;

    void Update()
    {
        m_LevelDuration -= Time.deltaTime;

        minutes = Mathf.FloorToInt(m_LevelDuration / 60.0f);
        if (minutes >= 2.0f)
        {
            m_Grad.topColor = GreenTop;
            m_Grad.bottomColor = GreenBottom;
        }
        else if (minutes >= 1.0f)
        {
            m_Grad.topColor = YellowTop;
            m_Grad.bottomColor = YellowBottom;
        }
        else
        {
            m_Grad.topColor = RedTop;
            m_Grad.bottomColor = RedBottom;
        }

        m_Timer.text = string.Format("{0}:{1}:{2}", 
            minutes.ToString("00"), 
            (m_LevelDuration % 60.0f).ToString("00"), 
            ((m_LevelDuration % 1) * 100.0f).ToString("00"));


        if (m_LevelDuration < 0)
        {
            SceneManager.LoadScene("Title", LoadSceneMode.Single);
        }

    }
}
