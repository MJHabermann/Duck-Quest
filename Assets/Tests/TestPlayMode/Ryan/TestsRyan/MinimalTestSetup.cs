using NUnit.Framework;
using UnityEngine;

public class MinimalSetupTest
{
    private GameObject testObject;

    [SetUp]
    public void Setup()
    {
        Debug.Log("Starting minimal setup...");
        testObject = new GameObject("TestObject");
        Assert.IsNotNull(testObject, "Test object was not created.");
        Debug.Log("Minimal setup complete.");
    }

    [Test]
    public void TestObjectExists()
    {
        Assert.IsNotNull(testObject, "Test object is null.");
    }
}
