using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField]
    protected LevelHandler levelHandler;

    [SerializeField]
    protected AmbientTemp Temperature;

    [SerializeField]
    protected Material SkyBox;

    [SerializeField]
    protected Light skylight;

    private void Start()
    {
        levelHandler.OnNewSeason += OnNewSeason;
        
    }

    protected void OnNewSeason(LevelData data)
    {
        Temperature.RiseRate = data.RiseRate;
        StartCoroutine(GradualShift(data));
    }
    IEnumerator GradualShift(LevelData data)
    {
        var oldTopSky = SkyBox.GetColor("_SkyColor1");
        var oldMidSky = SkyBox.GetColor("_SkyColor2");
        var oldBotSky = SkyBox.GetColor("_SkyColor3");
        var oldLight = skylight.color;

        var time = 0f;
        var duration = .5f;

        float ratio = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            ratio = time / duration;
            SkyBox.SetColor("_SkyColor1", Color.Lerp(oldTopSky, data.Environment.TopSky, ratio));
            SkyBox.SetColor("_SkyColor2", Color.Lerp(oldMidSky, data.Environment.MidSky, ratio));
            SkyBox.SetColor("_SkyColor3", Color.Lerp(oldBotSky, data.Environment.BotSky, ratio));
            skylight.color = Color.Lerp(oldLight, data.Environment.Lighting, ratio);

            yield return new WaitForEndOfFrame();
        }

        SkyBox.SetColor("_SkyColor1", data.Environment.TopSky);
        SkyBox.SetColor("_SkyColor2", data.Environment.MidSky);
        SkyBox.SetColor("_SkyColor3", data.Environment.BotSky);

        skylight.color = data.Environment.Lighting;
    }
}
