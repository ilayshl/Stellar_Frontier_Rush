
using TMPro;
using UnityEngine;

public class UIManager: MonoBehaviour {
    [SerializeField] private TextMeshProUGUI[] textsGUI;

    void Start() {
        
    }

    /// <summary>
    /// 0 = Health, 1 = Bullet, 2 = Fire rate, 3 = Movement speed;
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    public void SetText(int index, string value) {
        textsGUI[index].text=value;
    }

}
