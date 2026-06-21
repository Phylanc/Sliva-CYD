using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;


namespace SlivaCYD1
{
    public class HpBar : MonoBehaviour
    {
       [SerializeField] private Image _HpBarFill;
       
       [SerializeField] PlayerHp _Hp;
       
       private Camera _camera;

       private void Awake()
       {
           _Hp.HpChanged += OnHpChanged;
           _camera = Camera.main;
       }

       private void OnDestroy()
       {
           _Hp.HpChanged -= OnHpChanged;
       }

       private void OnHpChanged(float valueAsPercentage)
       {
           Debug.Log(valueAsPercentage);
           _HpBarFill.fillAmount = valueAsPercentage;
       }

       // private void LateUpdate()
       // {
       //     float parentScaleX = transform.parent.localScale.x;
       //     transform.localScale = new Vector3(1f / parentScaleX, 1f, 1f);
       // }
    }
}
