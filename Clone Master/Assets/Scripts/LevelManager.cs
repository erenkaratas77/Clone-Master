using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    Vector3 mousePosition;

    public int level,myCoin;
    public Canvas canvas;
    public Text levelText;

    [Header("Panels")]
    public GameObject startPanel;
    public GameObject finishPanel;
    public GameObject failPanel;


    [Header("Values")]
    public TextMeshProUGUI startUnitTMP;
    public TextMeshProUGUI unitLVL;
    public TextMeshProUGUI incomeTMP;
    public TextMeshProUGUI incomeLVL;
    public TextMeshProUGUI myCoinsTMP;

    int startUnitValue, incomeValue;


    float firstPlayerDistToFinish;
    float playerDistToFinish;

    void Awake()
    {
        Instance = this;

        setPlayerPrefsFirstTime();
      
        //levelText.text = "Level " + (level + 1);
        level = level % transform.childCount;
        transform.GetChild(level).gameObject.SetActive(true);



    }
    private void Start()
    {
        firstPlayerDistToFinish = (transform.GetChild(level).GetComponentInChildren<Finish>().transform.position - Player.Instance.transform.position).magnitude;
    }

    void Update()
    {
        myCoin = PlayerPrefs.GetInt("myCoin");
        myCoinsTMP.text = PlayerPrefs.GetInt("myCoin").ToString();

        if (!Player.Instance.enabled)
        {
            StartSlide();
        }
        else
        {
            calculateDist();
        }
    }

    public void play()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(true); // kayýp slider aranýyor
        canvas.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = (level+1).ToString();
        Player.Instance.enabled = true;
        startPanel.SetActive(false);


    }
    
    public IEnumerator fail(float f)
    {
        //canvas.SetTrigger("lose");
        SoundManager.Instance.Run.gameObject.SetActive(false);
        SoundManager.Instance.LoseSound.Play();
        yield return new WaitForSeconds(f);
        failPanel.SetActive(true);
    }

    public void failRestart()
    {
        Application.LoadLevel(0);
    }

    public IEnumerator win(float f)
    {
        //canvas.SetTrigger("win");
        //PlayerPrefs.SetInt("tutorial", 1);
        SoundManager.Instance.Run.gameObject.SetActive(false);
        SoundManager.Instance.CoinSound.Play();
        yield return new WaitForSeconds(f);
        finishPanel.SetActive(true);
    }
    public void winRestart()
    {
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        Application.LoadLevel(0);
    }

    public void increaseIncome()
    {
        if (PlayerPrefs.GetInt("myCoin") >= (PlayerPrefs.GetInt("incomeValue") * 100))
        {
            int myCoin = PlayerPrefs.GetInt("myCoin");
            PlayerPrefs.SetInt("myCoin", myCoin - (PlayerPrefs.GetInt("incomeValue") * 100));


            incomeValue = PlayerPrefs.GetInt("incomeValue");
            PlayerPrefs.SetInt("incomeValue", incomeValue + 1);
            incomeValue = PlayerPrefs.GetInt("incomeValue");

            incomeTMP.text = (incomeValue * 100).ToString();
            incomeLVL.text = (incomeValue) + " <br>LVL";
        }
    }
    public void increaseStartUnit()
    {
        if (PlayerPrefs.GetInt("myCoin") >= (PlayerPrefs.GetInt("startUnitValue")*100))
        {
            int myCoin = PlayerPrefs.GetInt("myCoin");
            PlayerPrefs.SetInt("myCoin", myCoin - (PlayerPrefs.GetInt("startUnitValue") * 100));
            RadialFormation.Instance._amount += 1;
            

            startUnitValue = PlayerPrefs.GetInt("startUnitValue");
            PlayerPrefs.SetInt("startUnitValue", startUnitValue + 1);
            startUnitValue = PlayerPrefs.GetInt("startUnitValue");

            startUnitTMP.text = (startUnitValue * 100).ToString();
            unitLVL.text = (startUnitValue) + " <br>LVL";
        }
       



    }
    public void paraver()
    {
        int myCoin = PlayerPrefs.GetInt("myCoin");
        PlayerPrefs.SetInt("myCoin",myCoin+100);
    }

    void StartSlide()
    {
        float horizontal = 0;
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;

        }
        if (Input.GetMouseButton(0))
        {
            horizontal = (Input.mousePosition.x - mousePosition.x) / Screen.width * 5f;
            mousePosition = Input.mousePosition;
        }

        if (Mathf.Abs(horizontal) > 0.03f)
        {
            play();
        }
    }

    void calculateDist()
    {
        playerDistToFinish= (transform.GetChild(level).GetComponentInChildren<Finish>().transform.position - Player.Instance.transform.position).magnitude;
        if (!Player.Instance.isFinished)
        {
            canvas.transform.GetComponentInChildren<Slider>().value = (firstPlayerDistToFinish - playerDistToFinish) / firstPlayerDistToFinish;

        }
        else
        {
            canvas.transform.GetComponentInChildren<Slider>().value = 1;
        }
    }

    void setPlayerPrefsFirstTime()
    {
        level = PlayerPrefs.GetInt("level");
        myCoin = PlayerPrefs.GetInt("myCoin");

        if (!PlayerPrefs.HasKey("startUnitValue"))
        {
            PlayerPrefs.SetInt("startUnitValue", 1);
        }
        if (!PlayerPrefs.HasKey("incomeValue"))
        {
            PlayerPrefs.SetInt("incomeValue", 1);
        }


        startUnitTMP.text = (PlayerPrefs.GetInt("startUnitValue") *100).ToString();
        unitLVL.text = PlayerPrefs.GetInt("startUnitValue") + " <br>LVL";

        incomeTMP.text = (PlayerPrefs.GetInt("incomeValue") *100).ToString();
        incomeLVL.text = PlayerPrefs.GetInt("incomeValue") + " <br>LVL";

        myCoinsTMP.text = PlayerPrefs.GetInt("myCoin").ToString();

    }


}