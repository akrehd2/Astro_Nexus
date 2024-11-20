using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public float waitTimer = 1.5f; // 모든 타이틀 오브젝트가 활성화된 후 대기 시간
    public List<GameObject> TitleObj; // 활성화할 타이틀 오브젝트 리스트
    public List<Vector3> linePositions; // 라인 렌더러의 포지션 리스트
    public GameObject Title; // 타이틀 오브젝트
    private LineRenderer lineRenderer; // 라인 렌더러

    private void Start()
    {
        lineRenderer = Title.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0; // 초기 포지션 수 설정
        StartCoroutine(ShowTitleObjects()); // 타이틀 오브젝트를 보여주는 코루틴 시작
    }

    private IEnumerator ShowTitleObjects()
    {
        yield return new WaitForSeconds(0.5f);

        // 사운드 재생
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

            // 라인 렌더러 포지션 업데이트
            lineRenderer.positionCount = linePositions.Count;
            lineRenderer.SetPositions(linePositions.ToArray());

            // 보간 애니메이션을 실행하기 전에 충분한 포지션이 있는지 확인
            if (linePositions.Count > 1)
            {
                yield return StartCoroutine(AnimateLineRenderer(linePositions[linePositions.Count - 2], linePositions[linePositions.Count - 1]));
            }

            // 대기
            yield return null;
        }

        // 모든 타이틀 오브젝트가 활성화된 후 사운드 시작
        if (SoundManager.instance.isPlaying[0] == false)
        {
            SoundManager.instance.OnTutorial();
        }

        // 대기 시간 동안 대기
        yield return new WaitForSeconds(waitTimer);

        // 게임 시작
        CameraCtrl.instance.isGame = true;
    }

    private IEnumerator AnimateLineRenderer(Vector3 startPoint, Vector3 endPoint)
    {
        float t = 0f;
        float duration = 0.03f; // 애니메이션 지속 시간

        while (t < 1f)
        {
            t += Time.deltaTime / duration; // 시간에 따라 t 증가
            Vector3 currentPoint = Vector3.Lerp(startPoint, endPoint, t); // 보간 계산
            linePositions[linePositions.Count - 1] = currentPoint; // 현재 포지션 업데이트
            lineRenderer.SetPositions(linePositions.ToArray()); // 라인 렌더러 업데이트
            yield return null; // 다음 프레임까지 대기
        }
    }
}
