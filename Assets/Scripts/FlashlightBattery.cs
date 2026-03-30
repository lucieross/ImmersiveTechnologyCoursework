using UnityEngine;
using TMPro;

public class FlashlightBattery : MonoBehaviour
{
    [Header("Battery Settings")]
    public float batteryLevel = 100f;
    public float drainRate = 0.3f;
    public float maxBattery = 100f;

    [Header("UI Reference")]
    [SerializeField] TextMeshProUGUI batteryText;

    [Header("Light Reference")]
    [SerializeField] GameObject lightObject;

    void Start()
    {
        if (lightObject != null) lightObject.SetActive(true);
    }

    void Update()
    {
        // 2. The battery drains as long as there is power left
        if (batteryLevel > 0)
        {
            batteryLevel -= drainRate * Time.deltaTime;
            batteryLevel = Mathf.Clamp(batteryLevel, 0, maxBattery);

            // --- Flicker Logic ---
            if (batteryLevel < 15f)
            {
                // Randomly flicker the light when it's almost dead
                lightObject.SetActive(Random.value > 0.3f);
            }
        }
        else
        {
            // 3. If battery hits 0, kill the light and the UI
            if (lightObject != null) lightObject.SetActive(false);
            if (batteryText != null) batteryText.gameObject.SetActive(false);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (batteryText != null && batteryLevel > 0)
        {
            batteryText.text = "Battery: " + Mathf.Round(batteryLevel) + "%";
        }
    }

    public void AddBattery(float amount)
    {
        batteryLevel += amount;
        batteryLevel = Mathf.Clamp(batteryLevel, 0, maxBattery);

        // 4. Bring the light and UI back to life if we picked up a battery
        if (batteryLevel > 0)
        {
            if (lightObject != null) lightObject.SetActive(true);
            if (batteryText != null) batteryText.gameObject.SetActive(true);
        }
    }

    public void ShowUI()
    {
        if (batteryText != null) batteryText.gameObject.SetActive(true);
    }

}
