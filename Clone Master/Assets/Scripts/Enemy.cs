using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FSG.MeshAnimator;
public class Enemy : MonoBehaviour
{
    bool enough;
    private void Update()
    {
        if (GetComponentInParent<EnemyArea>().warStarted && !Player.Instance.failed)
        {
            transform.position = Vector3.MoveTowards(transform.position,Player.Instance.transform.position, 5 * Time.deltaTime);
            GetComponent<MeshAnimatorBase>().Play("Run");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !enough)
        {
            enough = true;
            collision.transform.GetComponent<SphereCollider>().enabled = false;
            transform.GetComponent<SphereCollider>().enabled = false;

            SoundManager.Instance.Dead.Play();
            //Playerları yok ediyor
            ExampleArmy.Instance._spawnedUnits.Remove(collision.gameObject);
            RadialFormation.Instance._amount -= 1;
            //collision.transform.GetComponentInChildren<ParticleSystem>().Play();

            if (RadialFormation.Instance._amount %10 == 0)
            {
                collision.transform.GetChild(0).GetComponent<ParticleSystem>().Play();

            }
            collision.transform.GetChild(1).GetComponent<ParticleSystem>().Play();


            collision.transform.GetComponent<MeshRenderer>().enabled = false;
            collision.transform.parent = Trash.Instance.transform;



            //GameManager.Instance.callParticle(GameManager.Instance.blueBloodParticles, transform);

            //Enemyleri Yok ediyor 

            GetComponentInParent<EnemyArea>().GetComponentInChildren<ExampleArmy>()._spawnedUnits.Remove(this.gameObject);
            GetComponentInParent<EnemyArea>().GetComponentInChildren<RadialFormation>()._amount -= 1;

            if (RadialFormation.Instance._amount % 10 == 0)
            {
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();

            }
            collision.transform.GetChild(1).GetComponent<ParticleSystem>().Play();


            transform.GetComponent<MeshRenderer>().enabled = false;
            transform.parent = Trash.Instance.transform;

            Destroy(this);
        }
    }

    IEnumerator kill(GameObject g)
    {
        yield return new WaitForSeconds(3);
        Destroy(g);
    }
}
