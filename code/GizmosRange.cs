using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosRange : MonoBehaviour
{

    public float range;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
