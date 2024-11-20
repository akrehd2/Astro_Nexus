using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ShowHint : MonoBehaviour
{
    public static ShowHint instance;

    public LineRenderer hintLineRenderer;
    public LineRenderer stageLineRenderer;

    public List<Vector3> linePositions;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hintLineRenderer = GetComponent<LineRenderer>();
        hintLineRenderer.enabled = false;
    }

    public void StartShowHint()
    {
        StartCoroutine("ShowHintLine");
    }

    public IEnumerator ShowHintLine()
    {
        yield return new WaitForSeconds(1.3f);

        stageLineRenderer = StageManager.instance.aimNexumPartList[StageManager.instance.Stage].GetComponent<LineRenderer>();

        if (stageLineRenderer.gameObject.name == "AstroNexus")
        {
            yield break;
        }

        hintLineRenderer.enabled = true;

        Vector3 currentPoint = stageLineRenderer.GetPosition(hintLineRenderer.positionCount);

        linePositions.Add(currentPoint);

        StageManager.instance.stageAimNexumList[StageManager.instance.Stage].aimNexumGameobjectList[linePositions.Count - 1].GetComponent<AstroCtrl>().OnCastingParticle();

        hintLineRenderer.positionCount = linePositions.Count;

        hintLineRenderer.SetPositions(linePositions.ToArray());

        int R = Random.Range(11, 14);
        SoundManager.instance.CreateSound(R);

        yield return new WaitForSeconds(0.1f);

        if (hintLineRenderer.positionCount < stageLineRenderer.positionCount)
        {
            StartCoroutine("AddNextLine");
        }

        yield return null;
    }

    public IEnumerator AddNextLine()
    {
        Vector3 currentPoint = stageLineRenderer.GetPosition(hintLineRenderer.positionCount);

        Vector3 lastPoint = linePositions[linePositions.Count - 1];

        linePositions.Add(currentPoint);

        StageManager.instance.stageAimNexumList[StageManager.instance.Stage].aimNexumGameobjectList[linePositions.Count - 1].GetComponent<AstroCtrl>().OnCastingParticle();

        hintLineRenderer.positionCount = linePositions.Count;

        float t = 0;

        while (t <= 1.1f)
        {
            linePositions[linePositions.Count - 1] = Vector3.Lerp(lastPoint, currentPoint, t);
            hintLineRenderer.SetPositions(linePositions.ToArray());
            t += 0.1f;

            yield return new WaitForSeconds(0.003f);
        };

        int R = Random.Range(11, 14);
        SoundManager.instance.CreateSound(R);

        if (hintLineRenderer.positionCount < stageLineRenderer.positionCount)
        {
            StartCoroutine("AddNextLine");
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            StartCoroutine("RemovePrevLine");
        }

        yield return null;
    }

    public IEnumerator RemovePrevLine()
    {
        if (hintLineRenderer.positionCount > 1)
        {
            Vector3 firstPoint = linePositions[0];

            Vector3 prevPoint = linePositions[1];

            float t = 0;

            while (t <= 1.1f)
            {
                linePositions[0] = Vector3.Lerp(firstPoint, prevPoint, t);
                hintLineRenderer.SetPositions(linePositions.ToArray());
                t += 0.1f;

                yield return new WaitForSeconds(0.003f);
            };
        }

        linePositions.Remove(linePositions[0]);
        hintLineRenderer.positionCount = linePositions.Count;
        hintLineRenderer.SetPositions(linePositions.ToArray());

        if (hintLineRenderer.positionCount > 0)
        {
            StartCoroutine("RemovePrevLine");
        }

        yield return null;
    }
}
