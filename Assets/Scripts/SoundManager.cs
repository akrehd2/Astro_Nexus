using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public GameObject soundPrefab;

    public List<AudioClip> sources;
    public List<bool> isPlaying;
    public bool isCanSkip;

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

    public GameObject CreateSoundReturnObj(int num)
    {
        GameObject newSound = Instantiate(soundPrefab, transform);
        newSound.GetComponent<AudioSource>().clip = sources[num];
        newSound.GetComponent<AudioSource>().Play();

        return newSound;
    }

    public void OnTutorial()
    {
        StartCoroutine("TutorialSound");
    }

    public void SkipTutorial()
    {
        StopCoroutine("TutorialSound");
    }

    public IEnumerator TutorialSound()
    {
        isPlaying[0] = true;

        CreateSound(0);

        yield return new WaitForSeconds(5);

        isCanSkip = true;

        DialogManager.instance.dialogText.gameObject.SetActive(true);
        DialogManager.instance.dialogKorText.gameObject.SetActive(true);
        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[1];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[1];

        CreateSound(1);

        yield return new WaitForSeconds(2.5f);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[2];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[2];

        CreateSound(2);

        yield return new WaitForSeconds(5);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[3];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[3];

        CreateSound(3);

        yield return new WaitForSeconds(5);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[6];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[6];

        CreateSound(14);

        Vignette vignette;
        PostCtrl.instance.postVolume.profile.TryGet<Vignette>(out vignette);
        vignette.center.overrideState = true;
        vignette.smoothness.overrideState = true;
        vignette.rounded.overrideState = true;

        while (vignette.intensity.value < 0.98f)
        {
            vignette.center.value = Vector2.Lerp(vignette.center.value, new Vector2(0.23f, 0.6f), Time.deltaTime);
            vignette.smoothness.value = Mathf.Lerp(vignette.smoothness.value, 1, Time.deltaTime);
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 1, Time.deltaTime);

            yield return null;
        }

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[7];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[7];

        CreateSound(15);

        yield return new WaitForSeconds(3);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[8];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[8];

        CreateSound(16);

        while (vignette.intensity.value > 0.46f)
        {
            vignette.center.value = Vector2.Lerp(vignette.center.value, new Vector2(0.5f, 0.5f), Time.deltaTime);
            vignette.smoothness.value = Mathf.Lerp(vignette.smoothness.value, 0.2f, Time.deltaTime);
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.45f, Time.deltaTime);

            yield return null;
        }

        vignette.center.overrideState = false;
        vignette.smoothness.overrideState = false;
        vignette.rounded.overrideState = false;

        yield return new WaitForSeconds(3);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[4];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[4];

        CreateSound(4);

        yield return new WaitForSeconds(4);

        DialogManager.instance.dialogText.text = DialogManager.instance.dialogStrings[0];
        DialogManager.instance.dialogKorText.text = DialogManager.instance.dialogKorStrings[0];
        DialogManager.instance.dialogText.gameObject.SetActive(false);
        DialogManager.instance.dialogKorText.gameObject.SetActive(false);

        StageManager.instance.skipUI.SetActive(false);
        ShowHint.instance.StartShowHint();

        yield return RaycastAstro.instance.isCanClick = true;
    }
}
