using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class EnemyArea : MonoBehaviour
{
    public TextMeshPro counter;
    public bool warStarted;

    Vector3 enemyPos;
    // Start is called before the first frame update
    void Start()
    {
        enemyPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        counter.text = GetComponentInChildren<RadialFormation>()._amount.ToString();
        if (GetComponentInChildren<RadialFormation>()._amount <= 0)
        {
            Player.Instance.warStarted = false;
            Player.Instance.radius = 1;
            ExampleArmy.Instance._unitSpeed = 0.4f;
            GetComponentInChildren<Animator>().SetBool("circleGrowing", true);
            Destroy(counter.gameObject);
            Destroy(this);

        }
          else if (warStarted)
        {
            
            Player.Instance.transform.position = Vector3.MoveTowards(Player.Instance.transform.position, transform.position, 4 * Time.deltaTime);
            Player.Instance.GetComponentInChildren<ExampleArmy>()._unitSpeed = 2;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            warStarted = true;
            GameObject.Find("Player").GetComponent<Player>().warStarted = true;
            Destroy(GetComponent<BoxCollider>());

           
        }
    }
}
