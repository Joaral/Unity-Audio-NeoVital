using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerWall : MonoBehaviour
{

    public GameObject boss;
    BossControler bossControler;

    // Start is called before the first frame update
    void Start()
    {
        bossControler = boss.GetComponent<BossControler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("COLISSION: " + collision.tag);

        string tmpString = collision.tag;

        bossControler.newTrigger(tmpString);

    }

}
