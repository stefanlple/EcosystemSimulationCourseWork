using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class AnimalSpawner
{
    [UnityTest]
    public IEnumerator SpawnerList()
    {  
        var spawner= new GameObject().AddComponent<Spawner>();
        yield return null;
        List<Vector3> list = new List<Vector3>();
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
        Assert.AreEqual(spawner.list, list);
    }
   
    
    [UnityTest]
    public IEnumerator HareSpawnerWithEnumeratorPasses()
    {  
        var HarePrefab= Resources.Load("HarePrefab");
        
        var hareSpawner= new GameObject().AddComponent<Spawner>();
        hareSpawner.bunnynum =3;
        yield return null;
        
        
        var spawnedHare= GameObject.FindWithTag("Prey");
        //var prefabOfSpawnedHare= PrefabUtility.GetPrefabParent(spawnedHare);
        Assert.AreEqual(HarePrefab, HarePrefab);
    } 
    
}
