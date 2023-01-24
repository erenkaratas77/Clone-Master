using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Ramp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.DOBlendableMoveBy(new Vector3(0, 2, 0), .75f).SetLoops(2, LoopType.Yoyo);
        }
    }
}