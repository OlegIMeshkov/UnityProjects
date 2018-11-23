using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileTouchControl : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float smoothing;

    private Vector2 origin;
    private Vector2 direction;
    private Vector2 smoothDirection;
    private bool touched;
    private int pointerID;



    void Awake()
    {
        direction = Vector2.zero;
        touched = false;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("uha");
        Debug.Log("Screen touched");
        if (!touched)
        {
            touched = true;
            pointerID = eventData.pointerId;
            origin = eventData.position;
            Debug.Log(origin);
        }

            /*
            foreach (RectTransform child in GameManager.instance.tasksPanel)
            {
                if (origin.y < child.rect.yMax && origin.y > child.rect.yMin)
                {
                    RectTransform objectUnderPointer = child;
                    
                    Debug.Log(objectUnderPointer.gameObject.GetComponent<ItemEntry>().itemTimeEstimation);
                }
            }
            

        }
        */
    }
            

        public void OnDrag(PointerEventData eventData)
    {
        
        if (eventData.pointerId == pointerID)
        {
            if (eventData.position.x > origin.x)
            {
                Debug.Log("Движение вправо");
                gameObject.transform.position = new Vector2(eventData.position.x, gameObject.transform.position.y);
            }
             if (eventData.position.x < origin.x)
            {
                Debug.Log("Движение влево");
                gameObject.transform.position =  new Vector2(eventData.position.x, gameObject.transform.position.y);
            }
             if (eventData.position.y < origin.y)
            {
                Debug.Log("Движение вниз");
                GameManager.instance.tasksPanel.position = new Vector2(GameManager.instance.tasksPanel.position.x, eventData.position.y);
            }
             if (eventData.position.y > origin.y)
            {
                Debug.Log("Движение вверз");
                GameManager.instance.tasksPanel.position = new Vector2(GameManager.instance.tasksPanel.position.x, eventData.position.y);
            }
            /* Vector2 currentPosition = eventData.position;
             Vector2 directionRaw = currentPosition - origin;
             direction = directionRaw.normalized;
             if (directionRaw.x > 0f)
             {
                 gameObject.transform.position = new Vector2(eventData.position.x, gameObject.transform.position.y);
             }
             */
        }

    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            direction = Vector3.zero;
            touched = false;
        }

    }
}
