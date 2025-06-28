using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy_healthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;


    public void UpdateHealthBar(float currval, float maxval)
    {
        slider.value = currval / maxval;
    }
    void Update()
    {
        
    }
}
