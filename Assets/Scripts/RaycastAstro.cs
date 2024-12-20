using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaycastAstro : MonoBehaviour
{
    public static RaycastAstro instance;

    public bool isCanClick;
    public bool isCanRestart = false;
    public float skipTimer;
    public float restartTimer;

    LineRenderer lineRenderer;
    float lineWidth = 0.1f;

    public GameObject clickParticle;
    public GameObject clickGameobject;

    public GameObject clearParticle;

    public List<GameObject> nexumGameobject;
    public List<Vector3> linePoints;

    private void Awake()
    {
        instance = this;
    }

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

        if (isCanClick == true)
        {
            if (StageManager.instance.aimNexumPartList[StageManager.instance.Stage].GetComponent<LineRenderer>().enabled == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    clickParticle.SetActive(true);

                    if (Physics.Raycast(ray, out hit))
                    {
                        clickGameobject = hit.transform.gameObject;
                        clickGameobject.GetComponent<AstroCtrl>().OnCastingParticle2();

                        int R = Random.Range(11, 14);
                        SoundManager.instance.CreateSound(R);
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
                            hit.transform.GetComponent<AstroCtrl>().OnCastingParticle2();

                            if (hit.transform.gameObject != clickGameobject)
                            {
                                if (!nexumGameobject.Contains(clickGameobject))
                                {
                                    nexumGameobject.Add(clickGameobject);
                                }

                                if (!nexumGameobject.Contains(hit.transform.gameObject))
                                {
                                    int R = Random.Range(11, 14);
                                    SoundManager.instance.CreateSound(R);
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
                                    Debug.Log("nohit1");
                                    //linePoints.Add(nowMousePosition);
                                }
                                else if (linePoints.Count == nexumGameobject.Count)
                                {
                                    Debug.Log("nohit2");
                                    linePoints.Add(nowMousePosition);
                                    linePoints[nexumGameobject.Count - 1] = nexumGameobject[nexumGameobject.Count - 1].transform.position;
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

                            clearParticle.transform.position = CameraCtrl.instance.cameraParentPosition[StageManager.instance.Stage];
                            clearParticle.SetActive(true);

                            StageManager.instance.aimNexumPartList[StageManager.instance.Stage].GetComponent<LineRenderer>().enabled = true;

                            if (StageManager.instance.Stage != 0)
                            {
                                ShowHint.instance.StartShowHint();
                            }

                            SoundManager.instance.CreateSound(7);

                            StageManager.instance.stageAimNexumList[StageManager.instance.Stage].aimNexumGameobjectList.First().GetComponent<AstroCtrl>().isStartPoint = false;
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

                                SoundManager.instance.CreateSound(9);
                                SoundManager.instance.CreateSound(6);

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
            }
        }
        else if(SoundManager.instance.isCanSkip == true)
        {
            StageManager.instance.skipUI.GetComponent<Image>().color = new Color(1, 1, 1, skipTimer);
            StageManager.instance.skipUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, skipTimer);
            StageManager.instance.skipUI.GetComponent<Image>().fillAmount = skipTimer;

            if (Input.GetMouseButton(0))
            {
                //��ŵ
                skipTimer += 0.33f * Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0))
            {
                skipTimer = 0;
            }

            if (skipTimer >= 1)
            {
                StageManager.instance.skipUI.SetActive(false);

                clearParticle.transform.position = CameraCtrl.instance.cameraParentPosition[StageManager.instance.Stage];
                clearParticle.SetActive(true);

                SoundManager.instance.SkipTutorial();

                DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[0];
                DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[0];
                DialogManager.instance.dialogText.gameObject.SetActive(false);
                DialogManager.instance.dialogKorText.gameObject.SetActive(false);

                var tempSoundList = new List<GameObject>();

                for (int i = 0; i < SoundManager.instance.transform.childCount; i++)
                {
                    tempSoundList.Add(SoundManager.instance.transform.GetChild(i).gameObject);
                }

                foreach (GameObject sound in tempSoundList)
                {
                    Destroy(sound);
                }

                SoundManager.instance.CreateSound(7);

                PostCtrl.instance.init();

                ShowHint.instance.StartShowHint();
                skipTimer = 0;
                isCanClick = true;
                SoundManager.instance.isCanSkip = false;
            }
        }
        else if (isCanRestart == true)
        {
            StageManager.instance.reUI.GetComponent<Image>().color = new Color(1, 1, 1, restartTimer);
            StageManager.instance.reUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, restartTimer);
            StageManager.instance.reUI.GetComponent<Image>().fillAmount = restartTimer;

            if (Input.GetMouseButton(0))
            {
                //�����
                restartTimer += 0.33f * Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0))
            {
                restartTimer = 0;
            }

            if (restartTimer >= 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            clickParticle.SetActive(false);
        }
    }
}
