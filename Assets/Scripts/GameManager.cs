
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI Quota;
    public float points = 0;

    private void Update()
    {
        Quota.text = $"Quota {points.ToString()}/8";
    }
}
