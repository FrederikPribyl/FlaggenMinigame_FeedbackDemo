using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum OptionID { ProgressBar, Animation, Particles, SlotDepth }

public class Settings : MonoBehaviour
{

    public static Settings Instance;

    [SerializeField] Transform settingsPanel;
    [SerializeField] OptionToggle[] toggles;
    [SerializeField] Button allOn, allOff, resetButton;
    [SerializeField] RectTransform progressBar;

    Flag[] flags;
    Slot[] slots;

    private void Start()
    {
        Instance = this;
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].toggle.onValueChanged.AddListener((bool v) => UpdateSettings());
        }
        allOn.onClick.AddListener(() => ToggleAllToggles(true));
        allOff.onClick.AddListener(() => ToggleAllToggles(false));
        resetButton.onClick.AddListener(ResetScene);


        flags = FindObjectsOfType<Flag>();
        slots = FindObjectsOfType<Slot>();
        settingsPanel.gameObject.SetActive(false);


        ToggleAllToggles(false);
        UpdateSettings();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePanel();
        }
    }

    public void ResetScene()
    {
        // Scene scene = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
        foreach(Flag f in flags) f.Reset();
        UpdateSettings();
    }

    public void ToggleAllToggles(bool t)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].toggle.isOn = t;
        }

        UpdateSettings();
    }

    public void TogglePanel()
    {
        settingsPanel.gameObject.SetActive(!settingsPanel.gameObject.activeSelf);
    }

    public void UpdateSettings()
    {
        // Progress Bar:
        progressBar.gameObject.SetActive(GetOption(OptionID.ProgressBar));
        // Particles:
        foreach (Slot s in slots) s.particles_enabled = GetOption(OptionID.Particles);
        // Animation:
        foreach (Flag f in flags)
        {
            f.animation_enabled = GetOption(OptionID.Animation);
            if(f.locked == false)
            f.GetComponent<Animator>().SetBool("rotated", GetOption(OptionID.Animation));
        }
        // Slot Sprite Depth:
        foreach(Slot s in slots)
            s.SetDepthSprite(GetOption(OptionID.SlotDepth));

    }

    public bool GetOption(OptionID id)
    {
        foreach (OptionToggle ot in toggles)
            if (ot.option.Equals(id))
                return ot.toggle.isOn;
        return false;
        // return null;
    }
}

[System.Serializable]
public class OptionToggle
{
    public OptionID option;
    public Toggle toggle;
}