using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class DataSheet
{
    private static DataSheet dataSheet;

    public static DataSheet DataSheet1
    {
        get
        {
            if (dataSheet==null)
            {
                dataSheet=new DataSheet();
            }
            return dataSheet;
        }
    }

    private Canvas canvas;
    public int Quantity = 5;
    private GameObject Line;
    private GameObject DataPoint;
    private RectTransform DataPointRectTramsform;
    public  float LineThickness;
    DataSheet()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Line = GameObject.Find("Line");
        DataPoint = GameObject.Find("DataPoint");
        DataPointRectTramsform = DataPoint.GetComponent<RectTransform>();
    }

    private float[] Values;
    private List<GameObject> DataPoints;
    private List<GameObject> Lines;
    private List<GameObject> YLines=new List<GameObject>();
    private float k;
    private int ValueCount;
    /// <summary>
    /// 开始执行:浮动值，浮动值范围，X轴属性，Y轴属性,浮动线粗细
    /// </summary>
    /// <param name="Value"></param>
    /// <param name="hight"></param>
    /// <param name="XNames"></param>
    /// <param name="YName"></param>
    public void Setup(float Value,float hight,string[] XNames,string[] YNames,float LineThickness)
    {
        //生成数据点与线条
        if (DataPoints == null)
        {
            this.LineThickness = LineThickness;
            ValueCount =XNames.Length;
            //生成数据点
            Lines=new List<GameObject>();
            DataPoints = new List<GameObject>();
            var GoX = DataPointRectTramsform.rect.width / ValueCount;
            var GoXStart = 0-(DataPointRectTramsform.rect.width / 2);
            for (int i = 0; i < ValueCount; i++)
            {
                GameObject Go = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/DataPoint"));
                Go.transform.SetParent(DataPoint.transform, false);
                DataPoints.Add(Go);
                Go.GetComponent<RectTransform>().localPosition = new Vector3(GoXStart+(GoX*(i+1)-GoX/2),-DataPointRectTramsform.rect.height/2);

                //生成X轴属性名
                GameObject Xname = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/NameText"));
                Xname.transform.SetParent(DataPoint.transform.parent.GetChild(2), false);
                Xname.GetComponent<Text>().text = XNames[i];

                if (i == ValueCount-1) { break; }
                //生成连接线条
                GameObject line = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/LineChild"));
                line.transform.SetParent(Line.transform,false);
                Lines.Add(line);
            }
            for (int i = 0; i <YNames.Length ; i++)
            {
                //生成Y轴属性名
                GameObject Yname = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/NameText"));
                Yname.transform.SetParent(DataPoint.transform.parent.GetChild(0), false);
                Yname.GetComponent<Text>().text = YNames[i];
                YLines.Add(Yname);
                //生成Y轴线
                GameObject YnameLine = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/YNameLineImage"));
                YnameLine.transform.SetParent(DataPoint.transform.parent.GetChild(1), false);
                var Y = Yname.GetComponent<RectTransform>().position;
                YnameLine.GetComponent<RectTransform>().localPosition=new Vector3(0,0,0);
            }
            
        }
        DataFloat(Value,ValueCount,hight);
        LineFloat();
       
    }

    //设置数据点浮动
    void DataFloat(float Value, int ValueCount, float hight)
    {
        if (Values == null)
        {
            Values = new float[ValueCount];
            for (int i = 0; i < Values.Length; i++)
            {
                Values[i] = -DataPointRectTramsform.rect.height / 2;
            }
        }
        float percent = (Convert.ToSingle(Value) / Convert.ToSingle(hight) * DataPointRectTramsform.rect.height) + (-DataPointRectTramsform.rect.height / 2);

        if (Values[0] != k)
        {
            for (int i = Values.Length - 1; i < Values.Length; i--)
            {

                Values[i] = Values[i - 1];
                if (i == 1)
                {
                    Values[0] = percent;
                    break;
                }
            }

            for (int i = 0; i < Values.Length; i++)
            {
                DataPoints[i].GetComponent<RectTransform>().DOLocalMoveY(Values[i], 0.5f);
            }
        }
        k = percent;
    }

    //设置线条浮动
    void LineFloat()
    {
        for (int i = 0; i < ValueCount - 1; i++)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                DataPoints[i].transform.position, canvas.worldCamera, out pos))
            {
            }


            Vector2 pos1;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                DataPoints[i + 1].transform.position, canvas.worldCamera, out pos1))
            {
            }

            var height = pos.y - pos1.y;
            var widht = pos.x - pos1.x;
            Lines[i].GetComponent<RectTransform>().sizeDelta = new Vector2(widht, height);
            var x = (pos.x + pos1.x) / 2;
            var y = (pos.y + pos1.y) / 2;
            Lines[i].GetComponent<RectTransform>().localPosition = new Vector3(x, y);
        }
    }
}
