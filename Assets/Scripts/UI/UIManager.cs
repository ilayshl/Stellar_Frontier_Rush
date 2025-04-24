using TMPro;
using UnityEngine;

/// <summary>
/// Responsible for the Canvas and sets the text in its TextMeshGUI.
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] textsGUI;

    /// <summary>
    /// Updates the given text UI with given value, by the index:
    /// 0 = Health, 1 = Damage, 2 = Fire rate, 3 = Movement speed, 4 = Missile;
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    public void SetText(int index, string value)
    {
        textsGUI[index].text = value;
    }
}
