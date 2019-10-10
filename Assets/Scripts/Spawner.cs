using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject[] groups;
    public Camera uiCamera;
    public Button ButtonTransLeft;
    public Button ButtonTransRight;
    public Button ButtonChange;
    public Button ButtonDown;
    public Text TextScore;
    // Use this for initialization
    void Start()
    {
        Grid.uiCamera = uiCamera;
        Grid.ButtonTransLeft = ButtonTransLeft;
        Grid.ButtonTransRight = ButtonTransRight;
        Grid.ButtonChange = ButtonChange;
        Grid.ButtonDown = ButtonDown;
        Grid.TextScore = TextScore;
        spawnNext();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnNext()
    {
        //创建一个数组下标范围内的随机数
        int i = Random.Range(0, groups.Length);
        GameObject goGroup=Instantiate(groups[i]);
        goGroup.transform.parent = transform;
        goGroup.transform.localPosition = Vector3.zero;
        goGroup.transform.localScale = Vector3.one;
        goGroup.transform.localEulerAngles = Vector3.zero;
    }
}
