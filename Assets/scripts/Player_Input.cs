using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Input : MonoBehaviour
{
    public float speed;

    public Image fillRigth;
    public Image fillLefth;
    private bool isInCooldown;
    public float shieldDecressRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Actions();
        if(fillLefth.fillAmount <= 0 && fillRigth.fillAmount <= 0)
        {
            fillLefth.fillAmount = 0f;
            fillRigth.fillAmount = 0f;
            isInCooldown = false;
        }
        if(isInCooldown)
        {
            fillLefth.fillAmount -= shieldDecressRate * Time.deltaTime;
            fillRigth.fillAmount -= shieldDecressRate * Time.deltaTime;
        }
    }
    void Movement()
    {

        float xMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.position += new Vector3(xMovement, 0, 0);

        float yMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += new Vector3(0, yMovement, 0);

        if(xMovement==0)
        { GetComponent<Rigidbody2D>().velocity = new Vector2 (0,yMovement); }
        if (yMovement == 0)
        { GetComponent<Rigidbody2D>().velocity = new Vector2(xMovement, 0); }

    }
    void Actions()
    {
        if (!isInCooldown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                fillLefth.fillAmount = 0.1215f;
                fillRigth.fillAmount = 0.1215f;
                isInCooldown = true;
            }
        }
    }

}
