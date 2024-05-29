using TMPro;
using UnityEngine;

public class TextHighlight : MonoBehaviour
{
    private TMP_Text _text;
    private string _w;
    
    // Start is called before the first frame update
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    /*
     * Underline the text on mouse enter
     */
    private void OnMouseEnter()
    {
        if(StatsManager.instance.GetLevel()>1)
        {
            _w = _text.text;
            var uw = $"<u>{_w}</u>";
            _text.text = uw;
        }
    }

    /*
     * Remove the line on mouse exit
     */
    private void OnMouseExit()
    {
        if(StatsManager.instance.GetLevel()>1)
            _text.text = _w;
    }
}