using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public GameObject soundPrefab;

    public List<AudioClip> sources;
    public List<bool> isPlaying;

    private void Awake()
    {
        instance = this;
    }

    public void CreateSound(int num)
    {
        GameObject newSound = Instantiate(soundPrefab, transform);
        newSound.GetComponent<AudioSource>().clip = sources[num];
        newSound.GetComponent<AudioSource>().Play();
    }

    public IEnumerator TutorialSound()
    {
        isPlaying[0] = true;

        CreateSound(0);

        yield return new WaitForSeconds(5);

        DialogManager.instance.dialogText.gameObject.SetActive(true);
        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[1];

        CreateSound(1);

        yield return new WaitForSeconds(2.5f);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[2];

        CreateSound(2);

        yield return new WaitForSeconds(5);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[3];

        CreateSound(3);

        yield return new WaitForSeconds(5);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[4];

        CreateSound(4);

        yield return new WaitForSeconds(4);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[0];
        DialogManager.instance.dialogText.gameObject.SetActive(false);

        CreateSound(6);
        CameraCtrl.instance.animator.SetTrigger("Re");
        CameraCtrl.instance.countIdle = 4;
        CameraCtrl.instance.idleTimer = 2f;

        yield return new WaitForSeconds(0.7f);

        StageManager.instance.aimNexumPartList[0].GetComponent<LineRenderer>().enabled = true;

        yield return RaycastAstro.instance.isCanClick = true;
    }
}
