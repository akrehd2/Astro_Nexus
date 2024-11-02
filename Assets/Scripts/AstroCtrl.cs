using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroCtrl : MonoBehaviour
{
    public GameObject[] particles;

    void Start()
    {
        float R = Random.Range(0.2f, 0.8f);
        transform.localScale = new Vector3(R, R, R);

        foreach (var particle in particles)
        {
            int bR = Random.Range(0, 1);

            if (bR == 0)
            {
                particle.SetActive(true);
            }
            else
            {
                particle.SetActive(false);
            }
        }
    }
}
