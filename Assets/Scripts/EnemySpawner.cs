using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(Enemy e)
    {
        Instantiate(e, new Vector3(transform.position.x, transform.position.y, 0), this.transform.rotation);
    }
}
