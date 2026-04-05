using UnityEngine;
using System.Collections;

public class LightingManager : MonoBehaviour
{
    private LightmapData[] storedLightmaps;
    [SerializeField] GameObject flashlightObject;
    [SerializeField] GameObject directionalLight;

    private FlashlightBattery batteryScript;
    public bool powerCut = false;

    public AudioSource PowerOutage;

    void Start()
    {
        RenderSettings.fog = false;

        // Force the flashlight to be ACTIVE immediately on load
        if (flashlightObject != null)
        {
            flashlightObject.SetActive(true);

            batteryScript = flashlightObject.GetComponent<FlashlightBattery>();
            // Enable the battery script so it starts working/draining immediately
            if (batteryScript != null) batteryScript.enabled = true;
        }

        // Keep the "darkness" light off until the trigger happens
        if (directionalLight != null) directionalLight.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!powerCut)
        {
            StartCoroutine(CutThePower());
        }
        
    }

    public IEnumerator CutThePower()
    {
        PowerOutage.Play();
        yield return new WaitForSeconds(1f);
        powerCut = true;
        yield return new WaitForSeconds(0.5f);
        PowerOutage.Stop();
        storedLightmaps = LightmapSettings.lightmaps;
        LightmapSettings.lightmaps = new LightmapData[0];

        if (directionalLight != null) directionalLight.SetActive(true);

        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 0f;
        RenderSettings.fogEndDistance = 5f;

        RenderSettings.ambientLight = Color.black;
    }

    public void RestorePower()
    {
        LightmapSettings.lightmaps = storedLightmaps;
        RenderSettings.fog = false;
        RenderSettings.ambientLight = new Color(0.2f, 0.2f, 0.2f);
    }

    void Update()
    {
        // This keeps your light beam pointing where the flashlight model is pointing
        if (directionalLight != null && flashlightObject != null && flashlightObject.activeInHierarchy)
        {
            directionalLight.transform.rotation = flashlightObject.transform.rotation;
        }
    }
}
