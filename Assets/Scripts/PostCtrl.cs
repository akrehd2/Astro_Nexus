using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostCtrl : MonoBehaviour
{
    public static PostCtrl instance;
    public Volume postVolume;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        postVolume = GetComponent<Volume>();
    }

    private void Update()
    {
        if (RaycastAstro.instance.isCanClick)
        {
            Vignette vignette;
            postVolume.profile.TryGet<Vignette>(out vignette);
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.45f, Time.deltaTime);
        }
    }

    public void init()
    {
        Vignette vignette;
        postVolume.profile.TryGet<Vignette>(out vignette);
        vignette.intensity.value = 0.45f;

        vignette.center.overrideState = false;
        vignette.smoothness.overrideState = false;
        vignette.rounded.overrideState = false;
    }
}
