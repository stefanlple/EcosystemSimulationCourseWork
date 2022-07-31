using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Spawner : MonoBehaviour
{
    public GameObject bunnyPrefab;
    public GameObject foxPrefab;
    public int bunnynum;
    public int foxnum;
    int i = 0;
    int j = 0;
    [SerializeField]
    public List<Vector3> list = new List<Vector3>(); //{,new Vector3(20, 10, 45), new Vector3(80, 10, 80), new Vector3(60, 10, 88), new Vector3(28, 10, 75) };
    
    // Start is called before the first frame update
    void Start()
    {
        list.Add(new Vector3(13f, 7f, 22f));
        list.Add(new Vector3(29f, 7f, 25f));
        list.Add(new Vector3(50f, 7f, 30f));
        list.Add(new Vector3(77f, 7f, 33f));
        list.Add(new Vector3(58f, 7f, 55f));
        list.Add(new Vector3(30f, 7f, 55f));
        list.Add(new Vector3(20f, 7f, 45f));
        list.Add(new Vector3(80f, 7f, 80f));
        list.Add(new Vector3(60f, 7f, 88f));
        list.Add(new Vector3(28f, 7f, 75f));


    }

    // Update is called once per frame
    
    void Update()
    {   bunnynum = SettingsMenuHare.hareNumber;
        foxnum = SettingsMenuFox.fox;

        while ( i < bunnynum) {
            int q = Random.Range(0, list.Count);
            Vector3 pos= list[q];
            Instantiate(bunnyPrefab, pos, Quaternion.identity);
            i++;

                  }
        while (j < foxnum)
        {
            int q = Random.Range(0, list.Count);
            Vector3 pos = list[q];
            Instantiate(foxPrefab, pos, Quaternion.identity);
            j++;

        }
    }

        
}




