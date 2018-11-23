using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;



public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public RectTransform[] m_panelsArray;
	public Slider m_prioritySlider;
	public Slider m_scheduledTimeSlider;
	public Text m_priorityNumber;
	public Text m_scheduledTimeNumber;
	public Text m_title;

	public string name;
	public string priority;
	public string time;

	public List<GameObject> m_tasksList;
	public GameObject taskPrefab;
	public Transform tasksPanel;
    public RectTransform newTaskButton;

	public int newTaskPriority;
    public ItemEntry currentTask;
    protected static int globalItemID = 0;

    public InputField taskInRuntimeDescrtiption;
    public Text taskInRuntimePriority;
    public Text taskInRuntimeScheduledTime_hours;
    public Text taskInRuntimeScheduledTime_minutes;
    public Text taskInRuntimeScheduledTime_seconds;

    public bool taskIsActive = false;
    public bool taskIsCompleted = false;


    void Update()
    {
        if (taskIsActive)
        {
            GameManager.instance.currentTask.itemTimeEstimationInSeconds -= Time.deltaTime;
            DisplayTimeTracking();
        }

    }


    public void DisplayTimeTracking()
    {
        taskInRuntimeScheduledTime_hours.text = (Mathf.FloorToInt(currentTask.itemTimeEstimationInSeconds / 3600)).ToString();
        taskInRuntimeScheduledTime_minutes.text = (Mathf.FloorToInt(currentTask.itemTimeEstimationInSeconds % 3600) / 60).ToString();
        taskInRuntimeScheduledTime_seconds.text = (Mathf.FloorToInt(instance.currentTask.itemTimeEstimationInSeconds % 60)).ToString();
    }

    public void StartTimeTracking()
    {
        taskIsActive = !taskIsActive;


    }
    #region Unity standart methods

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(instance);
        }
    }
    // Use this for initialization
    void Start()
    {

        SetPanelActive(0);
        if (PlayerPrefs.HasKey("id"))
        {
            globalItemID = PlayerPrefs.GetInt("id");
        }
        else
        {
            globalItemID = 0;
            PlayerPrefs.SetInt("id", globalItemID);
        }
    }

    

    #endregion

    #region UI methods

    /// <summary>
    /// Sets the panel active.
    /// </summary>
    /// <param name="panelIndex">Panel index.</param>
    public void SetPanelActive(int panelIndex)
    {
        foreach (RectTransform p in m_panelsArray)
        {
            p.gameObject.SetActive(false);
        }

        m_panelsArray[panelIndex].gameObject.SetActive(true);
    }

    /// <summary>
    /// Sets the panel active.
    /// </summary>
    /// <param name="panelIndex_1">Panel index 1.</param>
    /// <param name="panelIndex_2">Panel index 2.</param>
    public void SetPanelActive(int panelIndex_1, int panelIndex_2)
    {
        foreach (RectTransform p in m_panelsArray)
        {
            p.gameObject.SetActive(false);
        }

        m_panelsArray[panelIndex_1].gameObject.SetActive(true);
        m_panelsArray[panelIndex_1].gameObject.SetActive(true);
    }



    /// <summary>
    /// Changes the priority.
    /// </summary>
    public void ChangePriority()
    {
        ConnectSliderAndText(m_prioritySlider, m_priorityNumber);
        priority = m_priorityNumber.text;
    }


    /// <summary>
    /// Changes the scheduled time. 
    /// </summary>
    public void ChangeScheduledTime()
    {
        ConnectSliderAndText(m_scheduledTimeSlider, m_scheduledTimeNumber);
        time = m_scheduledTimeNumber.text;
    }

    /// <summary>
    /// Connects the slider and text.
    /// </summary>
    /// <param name="s">S.</param>
    /// <param name="t">T.</param>
    public void ConnectSliderAndText(Slider s, Text t)
    {
        t.text = s.value.ToString();
    }

    #endregion




    public void AddNewTask2()
    {
        ItemEntry newEntry = new ItemEntry() { itemID = globalItemID, itemDescription = m_title.text, itemPriority = System.Int32.Parse(m_priorityNumber.text), itemTimeEstimationInSeconds = System.Int32.Parse(m_scheduledTimeNumber.text)*60f };
        globalItemID++;
        Debug.Log("New task created with ID "  + newEntry.itemID);
        XMLManager.ins.itemDB.list.Add(newEntry);
        tasksPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(tasksPanel.GetComponent<RectTransform>().sizeDelta.x,  (tasksPanel.childCount+1) * 100  -Screen.height + newTaskButton.rect.height);
       
    }

    public void CompleteTask()
    {
        currentTask.isCompleted = true;
    }


    private void OnApplicationQuit()
    {
        XMLManager.ins.LoadItems();
        PlayerPrefs.SetInt("id", globalItemID);
    }

}
