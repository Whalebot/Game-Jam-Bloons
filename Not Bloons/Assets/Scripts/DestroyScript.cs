using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DelayDestroy());
    }

    //Destroy itself after 2 sconds
    IEnumerator DelayDestroy() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
