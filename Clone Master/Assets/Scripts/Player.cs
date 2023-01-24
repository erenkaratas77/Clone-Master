using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using FSG.MeshAnimator;
public class Player : MonoBehaviour
{
    public static Player Instance;
    public TextMeshPro counter;
    public float moveSpeed;
    public float horizontal, boundary;
    public bool warStarted;
    public bool isFinished, isBossFight;
    public bool canCamFallow = true;
    Vector3 mousePosition;

    public MeshAnimatorBase[] meshAnimators;

    public Transform unitParent, endPlayers;
    public float radius;
    float newRadius;

    public bool failed;
    bool destroyedCounter, enough;

    Vector3 lastPosition = Vector3.zero;
    public float mySpeed;

    // Start is called before the first frame update
    private List<MeshAnimatorBase> myPlayers;
    private List<List<MeshAnimatorBase>> m_Result;
    private MeshAnimatorBase[] current;
    private int m_Length;

    private void Awake()
    {
        Instance = this;

        StartCoroutine(startUnits());
        
    }
    void Start()
    {

        counter.gameObject.SetActive(true);
        foreach (MeshAnimatorBase meshAnimator in meshAnimators)
        {
            meshAnimator.Play("Run");
        }

    }

    // Update is called once per frame
    void Update()
    {
        GetCombination();
        boundary = (10 - radius) / 2;

        checkArePlayersLive();

        if (!warStarted && !failed)
        {
            movement();
        }
        if((isFinished || isBossFight) && !enough)
        {
            if (!destroyedCounter)
            {
                Destroy(counter.gameObject);
                destroyedCounter = true;
            }
            if (endPlayers.childCount <= 0 && !isBossFight && GameManager.Instance.didLineUp)
            {
                int myCoin = PlayerPrefs.GetInt("myCoin");
                PlayerPrefs.SetInt("myCoin", (myCoin + (GameManager.Instance.howMuchPlayer) * ( (PlayerPrefs.GetInt("incomeValue")/10) + 1) ));
                SoundManager.Instance.WinSound.Play();
                StartCoroutine(LevelManager.Instance.win(2));
                enough = true;
            }
           
        }
        counter.text = GetComponentInChildren<RadialFormation>()._amount.ToString();
        
    }
    

    void checkArePlayersLive()
    {
        if (RadialFormation.Instance._amount <= 0 && !isFinished && !enough)
        {
            failed = true;
            enough = true;
            StartCoroutine(LevelManager.Instance.fail(0.5f));

        }
    }
    void movement()
    {
        horizontal = 0;

        if (!isFinished)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0) && mousePosition.x!=0)
            {
                horizontal = (Input.mousePosition.x - mousePosition.x) / Screen.width * 1.5f;
                mousePosition = Input.mousePosition;
            }
        }

        SoundManager.Instance.Run.gameObject.SetActive(true);
        transform.position += transform.right * horizontal* 5;

        transform.position += transform.forward * Time.deltaTime * moveSpeed;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -boundary-0.4f, boundary+0.2f), transform.position.y, transform.position.z);
    }

    public void GetCombination()
    {
        int n = unitParent.childCount;
        if (n > 1)
        {
            for (int i = 0; i < n; i++)
            {
                for(int t = i + 1; t < n; t++)
                {
                    newRadius = (unitParent.GetChild(i).transform.position-unitParent.GetChild(t).transform.position).magnitude;

                    if (newRadius > radius)
                    {
                        radius = newRadius;
                    }
                }
               
            }
        }
        
    }

    IEnumerator startUnits()
    {
        if (PlayerPrefs.GetInt("startUnitValue") > 1)
        {
            RadialFormation.Instance._amount = PlayerPrefs.GetInt("startUnitValue");
            ExampleArmy.Instance._unitSpeed = 0.1f;
            yield return new WaitForEndOfFrame();
        }
        while (!this.enabled)
        {
            meshAnimators = GetComponentsInChildren<MeshAnimatorBase>();

            foreach (MeshAnimatorBase meshAnimator in meshAnimators)
            {
                meshAnimator.Play("Idle");
            }
            yield return new WaitForSeconds(0.005f);
        }
       
    }

    

}
