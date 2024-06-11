using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControler : MonoBehaviour
{
    [SerializeField]
    private Text LifesText;
    [SerializeField]
    private Text survivedMisils;
    [SerializeField]
    private Text MisilLifeTime;
    [SerializeField]
    private Text WinText;
    [SerializeField]
    private Text GameOverText;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.PlayerInstance!= null)
        LifesText.text = "X"+GameManager.Instance.PlayerInstance.GetComponent<Character_Player_Controler>().Life_Controller.CurrentLife;
        if(GameManager.Instance.LvlManager!=null)
        survivedMisils.text = GameManager.Instance.LvlManager.GetComponent<LevelManager>().SurvivedMisils + "x";
        if(GameManager.Instance.Misil!=null)
        MisilLifeTime.text = Mathf.Round(GameManager.Instance.Misil.GetComponent<MisilControler>().CurrentLifeTime).ToString();
        else
        {
            if (GameManager.Instance.MisilSpawner != null)
                MisilLifeTime.text = "Misil IN " + Mathf.Round(GameManager.Instance.MisilSpawner.GetComponent<MisilFactory>().CurrentCreateCoolDown);
            else
                MisilLifeTime.text = "0,0";
        }
        if (GameManager.Instance.Win)
        {
            MisilLifeTime.text = "All misils pass";
            WinText.gameObject.SetActive(true);
        }
        else
            WinText.gameObject.SetActive(false);
        if(GameManager.Instance.GameOver1)
            GameOverText.gameObject.SetActive(true);
        else
           GameOverText.gameObject.SetActive(false);
    }
}
