using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;



public class CameraTests
{


    GameObject gameObject;


    [SetUp]
    public void BeforeEveryTest()
    {
        GameObject mainCamera = Resources.Load("MainCamera") as GameObject;
        MonoBehaviour.Instantiate(mainCamera);
    }

    [UnityTest]
    public IEnumerator MainCameraTest()
    {
        
        GameObject mainCamera = Resources.Load("MainCamera") as GameObject;
        MonoBehaviour.Instantiate(mainCamera);
        var cameraSpawned= GameObject.FindWithTag("MainCamera");
        yield return null;
        Assert.IsNotNull(cameraSpawned);
    }

 
} 
