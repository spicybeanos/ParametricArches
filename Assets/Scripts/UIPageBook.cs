using UnityEngine;
using TMPro;

public class UIPageBook : MonoBehaviour
{
    [SerializeField]
    TMP_Text page;
    int current = 0;
    int pages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pages = transform.childCount;
        Display();
    }

    void Display()
    {
        page.text = $"{current+1}/{pages}";
        for (int i = 0; i < pages; i++)
        {
            if(i != current)
                transform.GetChild(i).gameObject.SetActive(false);
            else
                transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public void Next()
    {
        current = (current + 1) % pages;
        Display();
    }
    public void Previous()
    {
        current = current - 1;
        if (current == -1)
            current = pages - 1;

        Display();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
