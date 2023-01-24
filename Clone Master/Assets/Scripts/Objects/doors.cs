using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doors : MonoBehaviour
{
    // Start is called before the first frame update
    public int operationValue;
    public bool isThisTimesDoor;

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

            
            BoxCollider[] colliders;
            colliders = transform.parent.GetComponentsInChildren<BoxCollider>();
            SoundManager.Instance.GatesGrow.Play();
            foreach (BoxCollider c in colliders)
            {
                c.enabled = false;
            }

            if (isThisTimesDoor)
            {
                int howMuch = RadialFormation.Instance._amount;

                for (int i = 0; i < operationValue - 1; i++)
                {
                    RadialFormation.Instance.increaseAmount(howMuch);
                }
                transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
                GetComponentInChildren<Animator>().SetBool("isPassed", true);
                Destroy(this);
            }
            else
            {
                RadialFormation.Instance.increaseAmount(operationValue);
                transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
                GetComponentInChildren<Animator>().SetBool("isPassed", true);
                Player.Instance.GetCombination();
                Destroy(this);
            }
        }

     
    }
    
}
