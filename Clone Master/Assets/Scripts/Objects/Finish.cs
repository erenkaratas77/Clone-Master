using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Finish : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isThisBossFight;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(GetComponent<BoxCollider>());
            if (!isThisBossFight)
            {
               
                StartCoroutine(GameManager.Instance.levelEnd());
                
            }
            else
            {
                Camera.main.GetComponent<camFallow>().dist = new Vector3(Mathf.Clamp(10, 7, 10), Mathf.Clamp(15, 12, 15), -8);
                other.GetComponentInParent<Player>().isBossFight = true;
                Camera.main.GetComponent<camFallow>().newTarget = transform.parent.GetComponentInChildren<bossArea>().transform.gameObject;
            }

           
        }
    }
}
