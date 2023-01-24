using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    public bool isThisBoss;
    bool isSoundPlayed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) //isTriger drops down
    {
        if (other.gameObject.tag == "Player")
        {
            ExampleArmy.Instance._spawnedUnits.Remove(other.gameObject);
            RadialFormation.Instance._amount -= 1;
            other.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            other.transform.GetComponent<Rigidbody>().useGravity = true;
            other.transform.GetComponent<Rigidbody>().drag = 0;

            other.transform.parent = GameObject.Find("Trash").transform;
        }
       
    }
    private void OnCollisionEnter(Collision collision) //collision hits
    {
        if (collision.gameObject.tag == "Player")
        {
            Handheld.Vibrate();
            if (!isSoundPlayed && isThisBoss)
            {
                StartCoroutine(hitSoundActivate());
            }
            SoundManager.Instance.Dead.Play();
            ExampleArmy.Instance._spawnedUnits.Remove(collision.gameObject);
            RadialFormation.Instance._amount -= 1;

            if (RadialFormation.Instance._amount % 3 == 0)
            {
                collision.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            }
            collision.transform.GetChild(1).GetComponent<ParticleSystem>().Play();


            collision.transform.GetComponent<MeshRenderer>().enabled = false;
            collision.transform.GetComponent<SphereCollider>().enabled = false;
            collision.transform.GetComponentInParent<Player>().radius = 1;

            StartCoroutine(kill(collision.gameObject));


        }

    }

    IEnumerator kill(GameObject g)
    {
        g.transform.parent = Trash.Instance.transform;
        yield return new WaitForSeconds(3);
        Destroy(g);
    }

    IEnumerator hitSoundActivate()
    {
        isSoundPlayed = true;
        SoundManager.Instance.BossHit.Play();
        yield return new WaitForSeconds(2);
        isSoundPlayed = false;
    }
}
