using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Skeleton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Walk());
    }

    public IEnumerator Walk()
    {

        while (true)
        {
            Vector3 pos = transform.position;
            pos.z += -10f * Time.deltaTime;
            transform.position = pos;
            yield return null;
            if (transform.position.z < 20)
            {
                yield break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
