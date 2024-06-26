using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Character_Player_Input))]
[RequireComponent(typeof(Character_PLayer_Model))]
public class Character_Player_Controler : MonoBehaviour,ILive
{
    private Rigidbody2D _rb;
    private Character_Player_Input _inputs;
    private Character_PLayer_Model _model;
    private Life_Controller _life_Controller;

    [SerializeField]
    private LineOfSight misilInRange;

   
    [SerializeField]
    private float _maxLife;
    [SerializeField]
    private Transform SpawnPoint;
    public Image fillRigth;
    public Image fillLefth;
    public float shieldDecressRate;
    private bool isInCooldown;

    public Rigidbody2D Rb => _rb;
    public float MaxLife => _maxLife;
    public Life_Controller Life_Controller => _life_Controller;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inputs = GetComponent<Character_Player_Input>();
        _model = GetComponent<Character_PLayer_Model>();
        _life_Controller = new Life_Controller(_maxLife);
    }
    // Start is called before the first frame update
    void Start() {
        _life_Controller.Dead.AddListener(OnDead);
        _life_Controller.Damaged.AddListener(ReSpawn);
    }

    // Update is called once per frame
    void Update()
    {
            Actions();
        #region shield fill dont go below 0
        if (fillLefth.fillAmount <= 0 && fillRigth.fillAmount <= 0)
        {
            fillLefth.fillAmount = 0f;
            fillRigth.fillAmount = 0f;
            isInCooldown = false;
            return;
        }
        #endregion 

        if (isInCooldown) 
        {
            fillLefth.fillAmount -= shieldDecressRate * Time.deltaTime;
            fillRigth.fillAmount -= shieldDecressRate * Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.Win==false)
        {
            if (_inputs.xMovement() != 0 || _inputs.yMovement() != 0)
            {
                 var DirToMove = new Vector2(Mathf.Round( _inputs.xMovement() ),
                                           Mathf.Round( _inputs.yMovement() ) );

                _model.Movement(DirToMove);
                return;
            }
            else
            { _model.Movement(Vector2.zero); return; }
        }

    }
   
    void Actions()
    {
        if (!isInCooldown)
        {
            #region deflection cheat
            if (GameManager.Instance.Misil != null)
            {
                if (misilInRange.InSight(GameManager.Instance.Misil.transform))
                {
                    fillLefth.fillAmount = 0.06f;
                    fillRigth.fillAmount = 0.06f;
                    isInCooldown = true;
                    UnityEvent @event = GameManager.Instance.Misil.GetComponent<MisilControler>().Deflection;
                    if (@event != null)
                        GameManager.Instance.EventQueue.Add(@event);
                    // else
                    // Debug.Log("deflection null");
                }
            }
            #endregion
            if (_inputs.Action1()) 
            {
                fillLefth.fillAmount = 0.06f;
                fillRigth.fillAmount = 0.06f;
                isInCooldown = true;
                if (GameManager.Instance.Misil != null)
                {
                    if (misilInRange.InSight(GameManager.Instance.Misil.transform))
                    {
                        UnityEvent @event = GameManager.Instance.Misil.GetComponent<MisilControler>().Deflection;
                        if (@event != null)
                            GameManager.Instance.EventQueue.Add(@event);
                       // else
                           // Debug.Log("deflection null");
                    }
                }
                //else
                  //Debug.Log("null misil");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Misil"))
        {
            _life_Controller.GetDamage(1);
            other.gameObject.GetComponent<MisilControler>().Destroy();
        }
    }
    private void ReSpawn()
    {
        if(Life_Controller.CurrentLife>0)
            transform.position = SpawnPoint.position;
    }

    private void OnDead() {
        GameManager.Instance.GameOver();
    }
}
