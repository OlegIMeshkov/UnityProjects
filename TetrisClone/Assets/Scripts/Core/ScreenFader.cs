using UnityEngine;
using System.Collections;
using UnityEngine.UI;
	


[RequireComponent(typeof(MaskableGraphic))]
public class ScreenFader : MonoBehaviour {

	public float m_startAlpha = 1f;
	public float m_targetAlpha = 0f;
	public float m_delay = 0f;
	public float m_timeToFade = 1f;

	float m_inc;
	float m_currentAlpha;
	MaskableGraphic m_graphic;
	Color m_originalColor;


	// Use this for initialization
	void Start () {
		m_graphic = GetComponent<MaskableGraphic> ();
		m_originalColor = m_graphic.color;
		m_currentAlpha = m_startAlpha;
		Color tempColor = new Color (m_originalColor.r, m_originalColor.g, m_originalColor.b, m_currentAlpha);
		m_graphic.color = tempColor;

		m_inc = ((m_targetAlpha - m_startAlpha) / m_timeToFade) * Time.deltaTime;
	
		StartCoroutine ("FadeRoutine");

	}
	
	IEnumerator FadeRoutine ()
	{
		yield return new WaitForSeconds (m_delay); //ждем m_delay секунд

		while (Mathf.Abs (m_targetAlpha - m_currentAlpha) > 0.01f) 
		{
			yield return new WaitForEndOfFrame ();//ждем до конца кадра
			m_currentAlpha += m_inc; //прибавляем значение инкремента
			Color tempColor = new Color (m_originalColor.r, m_originalColor.g, m_originalColor.b, m_currentAlpha);
			m_graphic.color = tempColor; //задаем значение цвета
		}

		Debug.Log ("Корутина Закончилась");
	}
}
