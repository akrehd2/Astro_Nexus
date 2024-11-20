using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroCtrl : MonoBehaviour
{
    public GameObject[] particles;

    public bool isStartPoint;

    public GameObject startParticle;
    public GameObject castingParticle;
    public GameObject castingParticle2;

    void Start()
    {
        float R = Random.Range(0.2f, 0.8f);
        transform.localScale = new Vector3(R, R, R);

        foreach (var particle in particles)
        {
            int bR = Random.Range(0, 2);

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

    private void Update()
    {
        if (isStartPoint)
        {
            startParticle.SetActive(true);
        }
        else
        {
            startParticle.SetActive(false);
        }
    }

    public void OnCastingParticle()
    {
        castingParticle.SetActive(true);
    }

    public void OnCastingParticle2()
    {
        castingParticle2.SetActive(true);
    }
}
