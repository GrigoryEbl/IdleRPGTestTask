using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownAttackView : MonoBehaviour
{
    [SerializeField] private Entity _entity;
    [SerializeField] private Slider _sliderAttack;
    [SerializeField] private Slider _sliderPreparingAttack;

    private void OnEnable()
    {
        _entity.AttackPreparing += OnAttackPreparing;
        _entity.Attacked += OnAttack;
    }

    private void OnDisable()
    {
        _entity.AttackPreparing -= OnAttackPreparing;
        _entity.Attacked -= OnAttack;
    }

    private void OnAttackPreparing(float delay)
    {
        _sliderPreparingAttack.gameObject.SetActive(true);
        _sliderPreparingAttack.maxValue = delay;
        StartCoroutine(Process(_sliderPreparingAttack, delay));
    }

    private void OnAttack(float delay)
    {
        _sliderAttack.gameObject.SetActive(true);
        _sliderAttack.maxValue = delay;
        StartCoroutine(Process( _sliderAttack, delay));
    }
    private IEnumerator Process(Slider slider, float delay)
    {
        float startValue = 0; 
        float endValue = slider.maxValue; 
        float elapsedTime = 0f; 

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime; 
            slider.value = Mathf.Lerp(startValue, endValue, elapsedTime / delay);
            yield return null; 
        }

        
        slider.value = endValue;

        slider.gameObject.SetActive(false);

        yield return null;
    }
}
