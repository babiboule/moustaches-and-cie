using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    private Button _button;
    private bool isBig;
    
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
    }

    private void OnMouseEnter()
    {
        if (_button.interactable)
        {
            _button.transform.localScale += new Vector3(.1f, .1f, .1f);
            isBig = true;
        }
    }

    private void OnMouseExit()
    {
        if (_button.interactable && isBig)
        {
            _button.transform.localScale -= new Vector3(.1f, .1f, .1f);
            isBig = false;
        }
    }

    private void OnMouseDown()
    {
        if (_button.interactable && isBig)
        {
            _button.transform.localScale -= new Vector3(.1f, .1f, .1f);
            isBig = false;
        }
    }
}

