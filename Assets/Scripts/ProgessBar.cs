using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgessBar : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    // Start is called before the first frame update
    public void ProgessBarImage(float count, float total)
    {
        progressBar.fillAmount = count / total;
    }
    public void ShowParent()
    {
        progressBar.transform.parent.gameObject.SetActive(true);
    }
}
