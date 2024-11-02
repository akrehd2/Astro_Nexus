using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public static CameraCtrl instance;

    public GameObject cameraParent;

    public Vector3 initPosition;
    public List<Vector3> cameraParentPosition;

    public Animator animator;

    public bool isGame;

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
            animator.SetBool("isGame", true);
        }
        else
        {
            cameraParent.transform.position = Vector3.Lerp(cameraParent.transform.position, initPosition, Time.deltaTime);
            animator.SetBool("isGame", false);
        }
    }
}
