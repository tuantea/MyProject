using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{
    public static Canvas Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject Privacy_Policy;

    int levelNo;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        levelNo = SceneManager.GetActiveScene().buildIndex;
        levelText.text="Level"+levelNo.ToString();
        Privacy_Policy.SetActive(false);
    }
    public void NextLevel()
    {
        levelNo++;
        if (levelNo > 10)
        {
            levelNo = 1;
        }
        PlayerPrefs.SetInt("Level",levelNo);
        SceneManager.LoadScene(levelNo);
    }

    public void ReLoad()
    {
        SceneManager.LoadScene(levelNo);
    }
    
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://google.com");
    }

    public void Active_Privacy_Policy()
    {
        Privacy_Policy.SetActive(true);
    }
    // Update is called once per frame
    public void Deactive_Privacy_Policy()
    {
        Privacy_Policy.SetActive(false);
    }
}
