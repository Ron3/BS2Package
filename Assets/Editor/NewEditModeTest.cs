using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;

public class AutoLayoutTest {

	[Test]
	public void TestLayout() 
	{
		GameObject panelObj = GameObject.Find("TestPanel");
		Debug.Log("TestLayout ===> " + panelObj.name);
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator NewEditModeTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame

		GameObject panel = GameObject.Find("TestPanel");
		Image bgImg = panel.GetComponent<Image>();
		bgImg.color = Color.red;
		
		// yield return new WaitForSeconds(5f);
		yield return null;
	}
}
