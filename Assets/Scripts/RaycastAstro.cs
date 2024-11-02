using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RaycastAstro : MonoBehaviour
{
    LineRenderer lineRenderer;
    float lineWidth = 0.01f;

    public GameObject clickParticle;
    public GameObject clickGameobject;

    public List<GameObject> nexumGameobject;
    public List<Vector3> linePoints;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.widthMultiplier = lineWidth;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 nowMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        clickParticle.transform.position = nowMousePosition;

        if (linePoints != null)
        {
            lineRenderer.positionCount = linePoints.Count;
        }
        else
        {
            lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            clickParticle.SetActive(true);

            if (Physics.Raycast(ray, out hit))
            {
                clickGameobject = hit.transform.gameObject;

                linePoints.Add(clickGameobject.transform.position);
            }
        }

        if (clickGameobject != null)
        {
            if (Input.GetMouseButton(0))
            {
                lineRenderer.enabled = true;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.transform.name);

                    if (hit.transform.gameObject != clickGameobject)
                    {
                        if (!nexumGameobject.Contains(clickGameobject))
                        {
                            nexumGameobject.Add(clickGameobject);
                        }

                        if (!nexumGameobject.Contains(hit.transform.gameObject))
                        {
                            nexumGameobject.Add(hit.transform.gameObject);
                        }
                        else
                        {
                            linePoints[nexumGameobject.IndexOf(hit.transform.gameObject)] = hit.transform.position;

                            if (hit.transform.gameObject == nexumGameobject[nexumGameobject.Count - 1])
                            {
                                if (linePoints.Count > nexumGameobject.Count)
                                {
                                    linePoints[nexumGameobject.Count] = nowMousePosition;
                                }
                                else
                                {
                                    linePoints.Add(nowMousePosition);
                                }
                            }
                            else
                            {
                                linePoints[nexumGameobject.Count] = nowMousePosition;
                            }
                        }
                    }
                    else
                    {
                        if (hit.transform.gameObject == clickGameobject)
                        {
                            if (linePoints.Count > 1)
                            {
                                linePoints[linePoints.Count - 1] = nowMousePosition;
                            }
                            else
                            {
                                linePoints.Add(nowMousePosition);
                            }
                        }
                    }
                }
                else
                {
                    if (linePoints.Count - nexumGameobject.Count < 2)
                    {
                        if (nexumGameobject.Count == 0)
                        {
                            Debug.Log("nohit");
                            //linePoints.Add(nowMousePosition);
                        }
                        else if (linePoints.Count == nexumGameobject.Count)
                        {
                            Debug.Log("nohit");
                            linePoints.Add(nowMousePosition);
                        }
                        else
                        {
                            linePoints[nexumGameobject.Count] = nowMousePosition;
                        }
                    }
                    else
                    {
                        if (nexumGameobject.Count != 0)
                        {
                            linePoints[nexumGameobject.Count] = nowMousePosition;
                        }
                        else
                        {
                            linePoints[1] = nowMousePosition;
                        }
                    }
                }

                lineRenderer.SetPositions(linePoints.ToArray());
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (nexumGameobject == null)
                {
                    clickGameobject = null;
                    nexumGameobject.Clear();
                    linePoints.Clear();

                    return;
                }

                if (string.Join(", ", StageManager.instance.stageAimNexumList[StageManager.instance.Stage].aimNexumGameobjectList) == string.Join(", ", nexumGameobject))
                {
                    clickGameobject = null;
                    nexumGameobject.Clear();
                    linePoints.Clear();

                    StageManager.instance.aimNexumPartList[StageManager.instance.Stage].GetComponent<LineRenderer>().enabled = true;

                    StageManager.instance.isStageClear[StageManager.instance.Stage] = true;
                    StageManager.instance.Stage += 1;
                }
                else
                {
                    if (nexumGameobject.Count > 0)
                    {
                        Vignette vignette;
                        PostCtrl.instance.postVolume.profile.TryGet<Vignette>(out vignette);
                        vignette.intensity.value = 0.6f;

                        StageManager.instance.showStageAim[StageManager.instance.Stage] = false;

                        CameraCtrl.instance.animator.SetTrigger("Re");
                        CameraCtrl.instance.idleTimer = 1;
                        CameraCtrl.instance.countIdle = 4;
                    }

                    clickGameobject = null;
                    nexumGameobject.Clear();
                    linePoints.Clear();
                }

                lineRenderer.positionCount = 0;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            clickParticle.SetActive(false);
        }
    }
}
