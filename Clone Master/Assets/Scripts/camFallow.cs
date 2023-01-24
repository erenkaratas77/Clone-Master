using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class camFallow : MonoBehaviour
{
    public GameObject target, endPlayers, newTarget;
    public Vector3 dist;
    public float speed, levelEndHigh;

    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target.GetComponent<Player>().canCamFallow && !target.GetComponent<Player>().isBossFight)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position + dist, Time.deltaTime * speed);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x / 10, -1, 1), transform.position.y, transform.position.z);
        }
        else if (target.GetComponent<Player>().isBossFight)
        {
            dist = new Vector3(Mathf.Clamp(dist.x, 7, 10), Mathf.Clamp(dist.y, 12, 15), dist.z);
            dist.x -= 0.02f;
            dist.y -= 0.02f;
            transform.position = Vector3.Lerp(transform.position, target.transform.position + dist, Time.deltaTime);
            transform.DOLookAt(newTarget.transform.position, .5f);

        }


    }
}
