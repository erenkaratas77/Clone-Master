using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSG.MeshAnimator;
public class bossArea : MonoBehaviour
{
    // Start is called before the first frame update
    public float Health;
    bool warStarted;
    bool soundPlayed;
    public MeshAnimatorBase[] meshAnimators;

    bool enough;

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Health <= 0 && !enough)
        {
            SoundManager.Instance.BossFight.gameObject.SetActive(false);
            SoundManager.Instance.BossDeath.Play();
            ExampleArmy.Instance._unitSpeed = 0;
            meshAnimators = Player.Instance.transform.GetComponentsInChildren<MeshAnimatorBase>();

            foreach (MeshAnimatorBase meshAnimator in meshAnimators)
            {
                meshAnimator.Play("Idle");
            }

            GetComponent<Animator>().SetBool("isDead", true);
            StartCoroutine(LevelManager.Instance.win(4f));
            enough = true;
        }
        else if (warStarted && !enough)
        {
            for(int i = 0; i <ExampleArmy.Instance._spawnedUnits.Count; i++)
            {
                ExampleArmy.Instance._spawnedUnits[i].transform.position= Vector3.MoveTowards(ExampleArmy.Instance._spawnedUnits[i].transform.position, transform.position, 4 * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(activateSound());
            warStarted = true;
            Player.Instance.warStarted = true;
            Destroy(GetComponent<BoxCollider>());


        }
    }
   
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health -= 0.01f;
        }
    }

    IEnumerator activateSound()
    {
        yield return new WaitForSeconds(.5f);

        if (!soundPlayed)
        {
            SoundManager.Instance.BossFight.gameObject.SetActive(true);
            soundPlayed = true;
        }



    }

}
