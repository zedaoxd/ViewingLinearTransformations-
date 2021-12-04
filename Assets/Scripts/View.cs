using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Vector2 vector;
    [SerializeField] private List<Matrix2X2> listMatrix;

    private Vector2 i = new Vector2(1, 0);
    private Vector2 j = new Vector2(0, 1);

    private void OnDrawGizmos()
    {
        var linearTranformation = CalculateTransformation();
        Gizmos.color = Color.red;
        GizmosUtils.DrawVectorAtOrigin(new Vector2(linearTranformation.M00, linearTranformation.M10));
        Gizmos.color = Color.green;
        GizmosUtils.DrawVectorAtOrigin(new Vector2(linearTranformation.M01, linearTranformation.M11));
        Gizmos.color = Color.yellow;
        var vec = linearTranformation * vector;
        GizmosUtils.DrawVectorAtOrigin(vec);
        UpdateText(vec, linearTranformation);
    }

    private void UpdateText(Vector2 vec, Matrix2X2 m)
    {
        text.text = $"V = ({vec.x}, {vec.y})\n{m}";
    }

    private Matrix2X2 CalculateTransformation()
    {
        var ret = Matrix2X2.Identity;
        foreach (var t in listMatrix)
        {
            ret = t * ret;
        }
        return ret;
    }
}
