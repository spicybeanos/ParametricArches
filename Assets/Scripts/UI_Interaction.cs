using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Interaction : MonoBehaviour
{
    [SerializeField] TMP_Text output;
    [SerializeField] TMP_InputField x;
    [SerializeField] TMP_InputField y;
    [SerializeField] Slider divisions;
    [SerializeField] TMP_Text divsDisplay;

    [SerializeField]
    ArchBuilder archBuilder;

    public void divsAmountChanged()
    {
        divsDisplay.text = $"divs[{(int)divisions.value}]";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(archBuilder == null)
        {
            archBuilder = GetComponent<ArchBuilder>();
        }
    }

    public void GenerateButtonPressed()
    {
        if (output == null) { Debug.LogError("Error text element is null!"); return; }

        string error = "";
        if (!formCheck(out error))
        {
            output.text = error;
        }
        else
        {
            output.text = "";
        }

        List<Token> xFxn = Tokenizer.TokenizeString(x.text);
        List<Token> yFxn = Tokenizer.TokenizeString(y.text);

        string _xout = "", _yout = "";

        foreach (var item in xFxn)
        {
            _xout += item.ToString() + ", ";
        }

        foreach (var item in yFxn)
        {
            _yout += item.ToString() + ", ";
        }
        Debug.Log("x function : " + _xout);
        Debug.Log("y function : " + _yout);

        Parser px = new Parser(xFxn);
        Parser py = new Parser(yFxn);
        Expression ex = px.Parse();
        Expression ey = py.Parse();

        archBuilder.Build((int)divisions.value, ex, ey);        
    }

    private bool formCheck(out string msg)
    {
        if (x == null || y == null || divisions == null)
        {
            msg = "input fields are null!";
            return false;
        }

        if (x.text == string.Empty)
        {
            msg = "function for x is empty";
            return false;
        }
        if (y.text == string.Empty)
        {
            msg = "function for y is empty";
            return false;
        }
        
        if ((int)divisions.value <= 0)
        {
            msg = "divisions fields is not an integer > 0";
            return false;
        }

        msg = "";
        return true;
    }
}
