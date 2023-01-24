using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSG.MeshAnimator;
public class endScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "levelEndPlayer")
        {
            Handheld.Vibrate();
            collision.transform.parent = GameObject.Find("Trash").transform;
            collision.transform.GetComponent<MeshAnimatorBase>().Play("Idle");
            collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            collision.transform.GetComponent<Rigidbody>().freezeRotation=true;
            collision.transform.GetComponent<Rigidbody>().useGravity = true;

        }
    }
}
