using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public float waitTimer = 1.5f; // ��� Ÿ��Ʋ ������Ʈ�� Ȱ��ȭ�� �� ��� �ð�
    public List<GameObject> TitleObj; // Ȱ��ȭ�� Ÿ��Ʋ ������Ʈ ����Ʈ
    public List<Vector3> linePositions; // ���� �������� ������ ����Ʈ
    public GameObject Title; // Ÿ��Ʋ ������Ʈ
    private LineRenderer lineRenderer; // ���� ������

    private void Start()
    {
        lineRenderer = Title.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0; // �ʱ� ������ �� ����
        StartCoroutine(ShowTitleObjects()); // Ÿ��Ʋ ������Ʈ�� �����ִ� �ڷ�ƾ ����
    }

    private IEnumerator ShowTitleObjects()
    {
        yield return new WaitForSeconds(0.5f);

        // ���� ���
        if (SoundManager.instance.isPlaying[1] == false)
        {
            SoundManager.instance.CreateSound(8);
            SoundManager.instance.isPlaying[1] = true;
        }

        yield return new WaitForSeconds(0.5f);

        for (int activeObj = 0; activeObj < TitleObj.Count; activeObj++)
        {
            TitleObj[activeObj].SetActive(true);
            TitleObj[activeObj].GetComponent<AstroCtrl>().OnCastingParticle();
            linePositions.Add(TitleObj[activeObj].transform.position);

            // ���� ������ ������ ������Ʈ
            lineRenderer.positionCount = linePositions.Count;
            lineRenderer.SetPositions(linePositions.ToArray());

            // ���� �ִϸ��̼��� �����ϱ� ���� ����� �������� �ִ��� Ȯ��
            if (linePositions.Count > 1)
            {
                yield return StartCoroutine(AnimateLineRenderer(linePositions[linePositions.Count - 2], linePositions[linePositions.Count - 1]));
            }

            // ���
            yield return null;
        }

        // ��� Ÿ��Ʋ ������Ʈ�� Ȱ��ȭ�� �� ���� ����
        if (SoundManager.instance.isPlaying[0] == false)
        {
            SoundManager.instance.OnTutorial();
        }

        // ��� �ð� ���� ���
        yield return new WaitForSeconds(waitTimer);

        // ���� ����
        CameraCtrl.instance.isGame = true;
    }

    private IEnumerator AnimateLineRenderer(Vector3 startPoint, Vector3 endPoint)
    {
        float t = 0f;
        float duration = 0.03f; // �ִϸ��̼� ���� �ð�

        while (t < 1f)
        {
            t += Time.deltaTime / duration; // �ð��� ���� t ����
            Vector3 currentPoint = Vector3.Lerp(startPoint, endPoint, t); // ���� ���
            linePositions[linePositions.Count - 1] = currentPoint; // ���� ������ ������Ʈ
            lineRenderer.SetPositions(linePositions.ToArray()); // ���� ������ ������Ʈ
            yield return null; // ���� �����ӱ��� ���
        }
    }
}
