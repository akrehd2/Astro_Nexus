using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

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

    public GameObject skipUI;
    public GameObject reUI;
    public PlayableDirector playerbleDirector;
    public TimelineAsset timelineAsset;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // 시작 별 파티클 조작
        if (isStageClear[Stage] == false)
        {
            if (Stage != 9 && Stage != 21 && Stage != 28 && Stage != 34)
            {
                stageAimNexumList[Stage].aimNexumGameobjectList.First().GetComponent<AstroCtrl>().isStartPoint = true;
            }
        }

        //스테이지 도달 시 부분 활성화
        if (Stage != 9 && Stage != 21 && Stage != 28 && Stage != 34)
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

        if (isStageClear[27] && Stage == 28)
        {
            if (SoundManager.instance.isPlaying[5] == false)
            {
                StartCoroutine(stage27Clear());
            }
        }

        if (isStageClear[33] && Stage == 34)
        {
            if (SoundManager.instance.isPlaying[6] == false)
            {
                StartCoroutine(Ending());
            }
        }
    }

    public IEnumerator stage0Clear()
    {
        RaycastAstro.instance.isCanClick = false;

        SoundManager.instance.isPlaying[2] = true;

        SoundManager.instance.CreateSound(5);

        DialogManager.instance.dialogText.gameObject.SetActive(true);
        DialogManager.instance.dialogKorText.gameObject.SetActive(true);
        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[5];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[5];

        yield return new WaitForSeconds(5);

        ShowHint.instance.StartShowHint();

        yield return new WaitForSeconds(2);

        DialogManager.instance.dialogText.gameObject.SetActive(false);
        DialogManager.instance.dialogKorText.gameObject.SetActive(false);

        yield return RaycastAstro.instance.isCanClick = true;
    }

    public IEnumerator stage8Clear()
    {
        RaycastAstro.instance.isCanClick = false;

        SoundManager.instance.isPlaying[3] = true;

        for (int i = 0; i < 8; i++)
        {
            aimNexumPartList[i].SetActive(true);
        }

        SoundManager.instance.CreateSound(17);

        DialogManager.instance.dialogText.gameObject.SetActive(true);
        DialogManager.instance.dialogKorText.gameObject.SetActive(true);
        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[9];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[9];

        yield return new WaitForSeconds(5);

        SoundManager.instance.CreateSound(20);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[12];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[12];

        Stage += 1;

        yield return new WaitForSeconds(2);

        ShowHint.instance.StartShowHint();

        yield return new WaitForSeconds(2);

        DialogManager.instance.dialogText.gameObject.SetActive(false);
        DialogManager.instance.dialogKorText.gameObject.SetActive(false);

        yield return RaycastAstro.instance.isCanClick = true;
    }

    public IEnumerator stage20Clear()
    {
        RaycastAstro.instance.isCanClick = false;

        SoundManager.instance.isPlaying[4] = true;

        for (int i = 10; i < 20; i++)
        {
            aimNexumPartList[i].SetActive(true);
        }

        SoundManager.instance.CreateSound(18);

        DialogManager.instance.dialogText.gameObject.SetActive(true);
        DialogManager.instance.dialogKorText.gameObject.SetActive(true);
        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[10];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[10];

        yield return new WaitForSeconds(7);

        SoundManager.instance.CreateSound(19);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[11];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[11];

        Stage += 1;

        yield return new WaitForSeconds(3);

        ShowHint.instance.StartShowHint();

        yield return new WaitForSeconds(2);

        DialogManager.instance.dialogText.gameObject.SetActive(false);
        DialogManager.instance.dialogKorText.gameObject.SetActive(false);

        yield return RaycastAstro.instance.isCanClick = true;
    }

    public IEnumerator stage27Clear()
    {
        RaycastAstro.instance.isCanClick = false;

        SoundManager.instance.isPlaying[5] = true;

        for (int i = 22; i < 27; i++)
        {
            aimNexumPartList[i].SetActive(true);
        }

        SoundManager.instance.CreateSound(21);

        DialogManager.instance.dialogText.gameObject.SetActive(true);
        DialogManager.instance.dialogKorText.gameObject.SetActive(true);
        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[13];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[13];

        yield return new WaitForSeconds(5);

        SoundManager.instance.CreateSound(22);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[14];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[14];

        yield return new WaitForSeconds(3);

        SoundManager.instance.CreateSound(23);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[15];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[15];

        Stage += 1;

        yield return new WaitForSeconds(3);

        ShowHint.instance.StartShowHint();

        yield return new WaitForSeconds(2);

        DialogManager.instance.dialogText.gameObject.SetActive(false);
        DialogManager.instance.dialogKorText.gameObject.SetActive(false);

        yield return RaycastAstro.instance.isCanClick = true;
    }

    public IEnumerator Ending()
    {
        SoundManager.instance.isPlaying[6] = true;

        for (int i = 0; i < 34; i++)
        {
            aimNexumPartList[i].SetActive(true);
        }

        SoundManager.instance.CreateSound(24);

        DialogManager.instance.dialogText.gameObject.SetActive(true);
        DialogManager.instance.dialogKorText.gameObject.SetActive(true);
        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[16];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[16];

        yield return new WaitForSeconds(5);

        SoundManager.instance.CreateSound(25);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[17];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[17];

        yield return new WaitForSeconds(5);

        SoundManager.instance.CreateSound(26);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[18];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[18];

        yield return new WaitForSeconds(6);

        SoundManager.instance.CreateSound(27);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[19];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[19];

        yield return new WaitForSeconds(3);

        DialogManager.instance.dialogText.gameObject.SetActive(false);
        DialogManager.instance.dialogKorText.gameObject.SetActive(false);

        RaycastAstro.instance.isCanClick = false;
        RaycastAstro.instance.isCanRestart = true;

        playerbleDirector.Play(timelineAsset);

        yield return null;
    }
}
