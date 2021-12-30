using UnityEngine;
using UnityEngine.UI;

public class BarView : MonoBehaviour
{
    [SerializeField] private Image barImage;


    public void SetBarAmount(float amount)
    {
        barImage.fillAmount = amount;
    }
}
