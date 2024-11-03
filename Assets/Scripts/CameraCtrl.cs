using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public static CameraCtrl instance;

    public GameObject cameraParent;

    public Vector3 initPosition;
    public List<Vector3> cameraParentPosition;
    public List<Vector3> cameraParentRotation;

    public Animator animator;

    public bool isGame;

    public int countIdle;
    public float idleTimer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(isGame)
        {
            cameraParent.transform.position = Vector3.Lerp(cameraParent.transform.position, cameraParentPosition[StageManager.instance.Stage], Time.deltaTime);
            cameraParent.transform.localEulerAngles = Vector3.Lerp(cameraParent.transform.localEulerAngles, cameraParentRotation[StageManager.instance.Stage], Time.deltaTime);

            if (StageManager.instance.showStageAim[StageManager.instance.Stage] == false)
            {
                if(idleTimer > 0)
                {
                    idleTimer -= 1 * Time.deltaTime;
                }
                else
                {
                    StageManager.instance.aimNexumPartList[StageManager.instance.Stage].GetComponent<LineRenderer>().enabled = true;

                    if (countIdle > 0)
                    {
                        SoundManager.instance.CreateSound(10);
                        animator.SetTrigger("Idle");
                        countIdle--;
                        idleTimer = 1;
                    }
                    else if (countIdle == 0)
                    {
                        SoundManager.instance.CreateSound(6);
                        animator.SetTrigger("Re");
                        countIdle--;
                        idleTimer = 0.7f;
                    }
                    else if (countIdle == -10)
                    {
                        StageManager.instance.aimNexumPartList[StageManager.instance.Stage].GetComponent<LineRenderer>().enabled = false;
                    }
                    else
                    {
                        StageManager.instance.showStageAim[StageManager.instance.Stage] = true;
                    }
                }
            }
            else
            {
                StageManager.instance.aimNexumPartList[StageManager.instance.Stage].GetComponent<LineRenderer>().enabled = false;
            }
        }
        else
        {
            cameraParent.transform.position = Vector3.Lerp(cameraParent.transform.position, initPosition, Time.deltaTime);
        }
    }
}
