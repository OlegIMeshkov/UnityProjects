using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {


	int m_score = 0;// текущий счет
	int m_lines;	//количество линий
	public int m_level = 1;//текущий уровень

	public int m_linesPerLevel = 5; //линий для следующего уровня

	public Text m_linesText;
	public Text m_levelText;
	public Text m_scoreText;

	public bool m_didLevelUp = false;

	const int m_minLines = 1;//минимальное кол-во рядов, которое может быть удалено за раз
	const int m_maxLines = 4;//макс


	public void ScoreLines (int n)
	{
		m_didLevelUp = false;
		n = Mathf.Clamp (n, m_minLines, m_maxLines);

		switch (n)
		{
		case 1:
			m_score += 40 * m_level;
			break;
		case 2:
			m_score += 100 * m_level;
			break;
		case 3:
			m_score += 300 * m_level;
			break;
		case 4:
			m_score += 1200 * m_level;
			break;
		}

		m_lines -= n;

		if (m_lines<=0) {
			LevelUp ();
		}


		UpdateText ();
	}

	public void Reset ()
	{
		m_level = 1;
		m_lines = m_linesPerLevel * m_level;
		UpdateText ();
	}


	void UpdateText ()
	{
		if (m_linesText) {
			m_linesText.text = m_lines.ToString ();
		}
		if (m_levelText) {
			m_levelText.text = m_level.ToString ();
		}
		if (m_scoreText) {
			m_scoreText.text = PadZero(m_score, 5);
		}
	}

	string PadZero (int n, int padDigits)
	{
		string nStr = n.ToString ();

		while (nStr.Length < padDigits) 
		{
			nStr = "0" + nStr;
		}
		return nStr;
	}

	public void LevelUp ()
	{
		m_level++; // увеличиваем уровень
		m_lines = m_linesPerLevel * m_level; //увеличиваем количество линий, необходимых для достижения следующего уровня
		m_didLevelUp = true;
	}




	// Use this for initialization
	void Start () 
	{
		Reset ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
