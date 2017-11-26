using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    public Text Score;
    public GameObject Highscore;


    public void Setup(int score)
    {
        int m_oldScore = -1;
        if (PlayerPrefs.HasKey("CARHS"))
        {
            m_oldScore = PlayerPrefs.GetInt("CARHS");
            
        }

        if (score > m_oldScore)
        {
            Highscore.SetActive(true);
            PlayerPrefs.SetInt("CARHS", score);
        }
        Score.text = score.ToString();
 


        this.gameObject.SetActive(true);
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(0, LoadSceneMode.Single);   
        }
		
    }
}
