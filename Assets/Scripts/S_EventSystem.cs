using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S_EventSystem : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private carMovement s_carMove;

    [Header("UI")]
    [SerializeField] TMP_Text m_TextMeshPro;
    [SerializeField] private GameObject endPanel;

    private void Update()
    {
        m_TextMeshPro.text = s_carMove.score.ToString();
        if (s_carMove.gameover) { endPanel.SetActive(true); }
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }
}
