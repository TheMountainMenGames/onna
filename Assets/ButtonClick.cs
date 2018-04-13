using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClick : MonoBehaviour
{
    public GameObject selected;

    private GameObject table;
    private GameObject bed;

    public Button yourButton;

    private void Awake()
    {
        table = Resources.Load("kitchen_table_2", typeof(GameObject)) as GameObject;
        bed = Resources.Load("bed_1", typeof(GameObject)) as GameObject;
    }

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(SelectTable);
    }

    public void ClickTest(){
        Debug.Log("Clicked");
    }

    public void SelectTable()
    {
        selected = table;
    }
}