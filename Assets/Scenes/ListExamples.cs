using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

/// <summary>
/// An example class showcasing List usage and methods.
/// </summary>
public class ListExamples : MonoBehaviour
{
    [SerializeField] private List<int> list = new() {1, 2, 3, 4, 5};

    private void Start() {
        for(int i = 1; i <= 5; i++){
            int value = list[i];
            list.Add(i);
        }
        Debug.Log(list);
    }
}
