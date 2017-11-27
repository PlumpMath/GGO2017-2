using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Tutorial : MonoBehaviour
{

    public GameObject[] Pages;


    private int m_Index = 0;
    // Use this for initialization
    void Start()
    {
        m_Index = 0;
        TurnPage();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Index++;
            if (m_Index >= Pages.Length)
            {
                SceneManager.LoadScene(1, LoadSceneMode.Single);       

            }
            else
            {
                TurnPage();
            }
        }
    }

    private void TurnPage()
    {
        foreach (GameObject g in Pages)
        {
            g.SetActive(false);
        }
        Pages[m_Index].SetActive(true);
    }
}
