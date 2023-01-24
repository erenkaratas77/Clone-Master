using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<ParticleSystem> blueBloodParticles = new List<ParticleSystem>();
    public List<ParticleSystem> redBloodParticles = new List<ParticleSystem>();
    public List<ParticleSystem> fightParticles = new List<ParticleSystem>();

    public GameObject indicator;
    [Header("Prefabs")]
    public GameObject levelEndPlayerPref;

    public int howMuchPlayer;
    public bool didLineUp;
    bool firstTime=true;
    void Awake()
    {
        Instance = this;    
        
    }   
    public IEnumerator addBonusPeople()
    {
        bool isClicked=false;
        indicator.transform.DOScale(1.2f, .5f);
        while (!isClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                float howMuch = indicator.GetComponent<Animator>().GetFloat("bonusePeople");
                RadialFormation.Instance.increaseAmount((int)howMuch);

            }
            else if (Input.GetMouseButtonUp(0))
            {
                indicator.transform.DOScale(0f, .5f);
                yield return new WaitForSeconds(.7f);
                
                Player.Instance.isBossFight = true;
                isClicked = true;
            }
        }
       
    }
    public IEnumerator levelEnd()
    {
        Player.Instance.isFinished = true;
        GameObject endPlayers = GameObject.Find("End Players");
        Player.Instance.transform.DOMoveX(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(RadialFormation.Instance.align(20));

        howMuchPlayer = RadialFormation.Instance._amount;
        float bottomCount, line = 1;

        bottomCount = (Mathf.Sqrt(1 + 4 * howMuchPlayer) - 1) / 2;
        bottomCount = (int)bottomCount;

        Camera.main.GetComponent<camFallow>().levelEndHigh = bottomCount * 1.2f;

        float stickmanOffset = .5f;
        float topLevelEndPlayerDistToStairs;
        for (int i = 0; i < bottomCount * 2; i++)
        {
            float[] positions = new float[100];
            int myVal = (int)line / 2;

            for (int t = 0; t < (int)line; t++)
            {
                float pos;

                if ((int)line % 2 != 0) pos = (t <= myVal ? t : -((int)line - t));
                else pos = ((t + 1) <= myVal ? (t + 1) - .4f : -(((int)line + 1) - (t + 1)) + .4f);
                pos *= stickmanOffset;

                var finalPlayer = Instantiate(levelEndPlayerPref, new Vector3(pos, Player.Instance.transform.position.y, Player.Instance.transform.position.z), Quaternion.identity);
               

                RadialFormation.Instance._amount -= 1;

                finalPlayer.transform.parent = endPlayers.transform;
                positions[i] = pos;
                

            }

            yield return new WaitForSeconds(0.07f);

            endPlayers.transform.position = new Vector3(endPlayers.transform.position.x, endPlayers.transform.position.y+1.2f, endPlayers.transform.position.z);


            line += 0.5f;
        }
        didLineUp = true;
        endPlayers.transform.position= new Vector3(endPlayers.transform.position.x, endPlayers.transform.position.y -1.2f, endPlayers.transform.position.z);

        Player.Instance.canCamFallow = false;
        Camera.main.transform.DOMoveX(15, 2);
        Camera.main.transform.DORotate(new Vector3(0, -35, 0), 2);

        RaycastHit hit;
        if (Physics.Raycast(endPlayers.transform.GetChild(0).transform.position, endPlayers.transform.GetChild(0).TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            
            topLevelEndPlayerDistToStairs = Vector3.Distance(hit.transform.position, endPlayers.transform.GetChild(0).transform.position);
            Camera.main.transform.DOMoveY(5, 1).OnComplete(() => camFunc(bottomCount, topLevelEndPlayerDistToStairs));
            Camera.main.transform.DOMoveZ(Camera.main.transform.position.z + topLevelEndPlayerDistToStairs+3, topLevelEndPlayerDistToStairs / 10).SetEase(Ease.Linear);

        }
        
        RadialFormation.Instance._amount = 0;
    }

    public void Move(List<ParticleSystem> list, int oldIndex, int newIndex)
    {
        List<ParticleSystem> tempList = new List<ParticleSystem>(list);
        ParticleSystem item = list[oldIndex];
        tempList.RemoveAt(oldIndex);
        list.Clear();
        int j = 0;
        for (int i = 0; i < tempList.Count + 1; i++)
        {
            list.Add(i == newIndex ? item : tempList[j]);
            j += i == newIndex ? 0 : 1;
        }
    }

    public void callParticle(List<ParticleSystem> list, Transform t)
    {
        if ((list[0].transform.position - list[5].transform.position).magnitude > 5 || firstTime)
        {
            firstTime = false;
            list[0].transform.position = t.position;
            list[0].Play();
            Move(list, 0, 5);
        }
       

    }

    void camFunc(float bottomCount,float topLevelEndPlayerDistToStairs)
    {
        Camera.main.transform.DOMoveY(bottomCount * 2.8f, topLevelEndPlayerDistToStairs / 9).SetEase(Ease.Linear);

    }

}
