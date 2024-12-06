using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ArchBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject emptyPrefab;
    [SerializeField]
    GameObject childParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Build(int segs,Expression ex,Expression ey)
    {
        if(childParent != null)
        {
            Destroy(childParent);
        }
        childParent = Instantiate(emptyPrefab,Vector3.zero,Quaternion.identity);

        Evaluator evaluator = new Evaluator();
        for (int i = 0; i <= segs; i++)
        {
            float t = ((float)i) / segs;
            float t1 = ((float)((i+1))) / segs;
            float t0 = ((float)((i-1))) / segs;
            GameObject go = Instantiate(prefab);

            float x = evaluator.Evaluate(ex, t);
            float y = evaluator.Evaluate(ey, t);

            float x1 = evaluator.Evaluate(ex, t1);
            float y1 = evaluator.Evaluate(ey, t1);
            float x0 = evaluator.Evaluate(ex, t0);
            float y0 = evaluator.Evaluate(ey, t0);


            Transform transform = go.transform;
            transform.position = new Vector3(x,y,0);

            if(x1 - x0 != 0)
            {
                float slope = (y1 - y0) / (x1 - x0);
                float angle = Mathf.Atan(slope) * 180 / Mathf.PI;
                transform.Rotate(0,0,angle);
            }
            else
            {
                transform.Rotate(0, 0, 90);
            }

            go.transform.SetParent(childParent.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
