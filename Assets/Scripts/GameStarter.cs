using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public float timer;
    public float waitTimer;

    public int activeObj;

    public List<GameObject> TitleObj;
    public List<Vector3> linePositions;

    public GameObject Title;
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = Title.GetComponent<LineRenderer>();

        activeObj = 0;
        timer = 1;
        waitTimer = 1.5f;
    }

    void Update()
    {
        if (activeObj < TitleObj.Count)
        {
            if (timer > 0)
            {
                timer -= 1 * Time.deltaTime;

                if(timer < 0.5f)
                {
                    if (SoundManager.instance.isPlaying[1] == false)
                    {
                        SoundManager.instance.CreateSound(8);
                        SoundManager.instance.isPlaying[1] = true;
                    }
                }
            }
            else
            {
                TitleObj[activeObj].SetActive(true);
                linePositions.Add(TitleObj[activeObj].transform.position);

                if (linePositions != null)
                {
                    lineRenderer.positionCount = linePositions.Count;
                }
                else
                {
                    lineRenderer.positionCount = 0;
                }

                lineRenderer.SetPositions(linePositions.ToArray());

                activeObj++;
                timer = 0.03f;
            }
        }
        else
        {
            if (SoundManager.instance.isPlaying[0] == false)
            {
                StartCoroutine(SoundManager.instance.TutorialSound());
            }

            if (waitTimer > 0)
            {
                waitTimer -= 1 * Time.deltaTime;
            }
            else
            {
                CameraCtrl.instance.isGame = true;
            }
        }
    }
}
