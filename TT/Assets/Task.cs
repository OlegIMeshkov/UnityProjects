using UnityEngine;
using System.Collections;

public class Task : MonoBehaviour {

	public string m_taskSampleTitle;
	public int m_taskSamplePriority;
	public int m_taskSampleScheduledTime;

	public void StartTask()
	{
		GameManager.instance.SetPanelActive (3);
	}



	}

