using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Flag[] flags;
    public static GameManager Instance;
    public RectTransform progressBar_fill;
    float progressBar_max_width;
    public Gradient progressBar_gradient;

    private void Start() {
        Instance = this;
        flags = GameObject.FindObjectsOfType<Flag>();
        progressBar_max_width = progressBar_fill.sizeDelta.x;
    }

    private void Update() {
        CheckProgress();
    }

    void CheckProgress(){
        int flags_open = 0;
        foreach(Flag f in flags){
            flags_open += f.locked ? 0 : 1;
        }
        if(flags_open == 0) Debug.Log("finished");
        float ratio = (float)(flags.Length - flags_open) / (float)flags.Length;
        progressBar_fill.sizeDelta = new Vector2(progressBar_max_width * ratio, progressBar_fill.sizeDelta.y);
        progressBar_fill.GetComponent<Image>().color = progressBar_gradient.Evaluate(ratio);
    }
}
