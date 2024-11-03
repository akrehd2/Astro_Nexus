using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [System.Serializable]
    public class StageAimNexumList
    {
        public List<GameObject> aimNexumGameobjectList;

        public StageAimNexumList(List<GameObject> aimNexumGameobjectList)
        {
            this.aimNexumGameobjectList = aimNexumGameobjectList;
        }
    }

    public static StageManager instance;

    public int Stage;

    public List<GameObject> aimNexumList;
    public List<GameObject> aimNexumPartList;

    public List<StageAimNexumList> stageAimNexumList;

    public List<bool> isStageClear;
    public List<bool> showStageAim;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // 시작 별 파티클 조작
        if (isStageClear[Stage] == false)
        {
            if (Stage != 9 && Stage != 21)
            {
                stageAimNexumList[Stage].aimNexumGameobjectList.First().GetComponent<AstroCtrl>().isStartPoint = true;
            }
        }

        //스테이지 도달 시 부분 활성화
        if (Stage != 9 && Stage != 21)
        {
            foreach (GameObject Part in aimNexumPartList)
            {
                if (Part != aimNexumPartList[Stage])
                {
                    if (Part.name != "AstroNexus")
                    {
                        Part.SetActive(false);
                    }
                }
                else
                {
                    Part.SetActive(true);
                }
            }
        }

        // 특정 스테이지 클리어 했을 때
        if (isStageClear[0])
        {
            if (SoundManager.instance.isPlaying[2] == false)
            {
                StartCoroutine(stage0Clear());
            }
        }

        if (isStageClear[8] && Stage == 9)
        {
            if (SoundManager.instance.isPlaying[3] == false)
            {
                StartCoroutine(stage8Clear());
            }
        }

        if (isStageClear[20] && Stage == 21)
        {
            if (SoundManager.instance.isPlaying[4] == false)
            {
                StartCoroutine(stage20Clear());
            }
        }
    }

    public IEnumerator stage0Clear()
    {
        SoundManager.instance.isPlaying[2] = true;

        SoundManager.instance.CreateSound(5);

        DialogManager.instance.dialogText.gameObject.SetActive(true);
        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[5];

        yield return new WaitForSeconds(7);

        DialogManager.instance.dialogText.gameObject.SetActive(false);

        yield return null;
    }

    public IEnumerator stage8Clear()
    {
        SoundManager.instance.isPlaying[3] = true;

        for (int i = 0; i < 8; i++)
        {
            aimNexumPartList[i].SetActive(true);
        }

        SoundManager.instance.CreateSound(17);

        DialogManager.instance.dialogText.gameObject.SetActive(true);
        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[9];

        yield return new WaitForSeconds(5);

        SoundManager.instance.CreateSound(20);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[12];

        Stage += 1;

        yield return new WaitForSeconds(4);

        DialogManager.instance.dialogText.gameObject.SetActive(false);
        
        yield return null;
    }

    public IEnumerator stage20Clear()
    {
        SoundManager.instance.isPlaying[4] = true;

        for (int i = 10; i < 20; i++)
        {
            aimNexumPartList[i].SetActive(true);
        }

        SoundManager.instance.CreateSound(18);

        DialogManager.instance.dialogText.gameObject.SetActive(true);
        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[10];

        yield return new WaitForSeconds(5);

        SoundManager.instance.CreateSound(19);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[11];

        Stage += 1;

        yield return new WaitForSeconds(5);

        DialogManager.instance.dialogText.gameObject.SetActive(false);

        yield return null;
    }
}
