using UnityEngine;
using System.Collections;
using Random = System.Random;

public class GameStart : MonoBehaviour
{
    private string[] X;
    private string[] Y;
    private float Value;
	// Use this for initialization
	void Start ()
	{
	    X = new[] {"X测试", "X测试", "X测试", "X测试", "X测试", "X测试", "X测试", "X测试", "X测试", "X测试"};
	    Y = new[] { "Y测试", "Y测试", "Y测试", "Y测试", "Y测试", "Y测试"};
        StartCoroutine(iEnumerator());
    }
	
	// Update is called once per frame
	void Update () {
	    DataSheet.DataSheet1.Setup(Value,100,X,Y,5);
	}

    /// <summary>
    /// 产生随机数值
    /// </summary>
    /// <returns></returns>
    IEnumerator iEnumerator()
    {
        while (true)
        {
            Random rd=new Random();
            Value = rd.Next(1, 100);
            yield return new WaitForSeconds(2);
        }
    }
}
