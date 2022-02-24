using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float jumpPower;
    public int itemCount;
    public GameManager manager;

    bool isJump;
    Rigidbody rigid;
    AudioSource itemSound;

    void Awake() 
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        itemSound = GetComponent<AudioSource>();
    }

    void Update() 
    {
        if(Input.GetButtonDown("Jump") && !isJump){
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }
    void FixedUpdate() 
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Floor")
            isJump = false;
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Item"){
            itemCount++;
            itemSound.Play();
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
        }
        else if(other.tag == "Finish"){
            if(itemCount == manager.totalItemCount){
                Debug.Log("결승점 도달!");
                //Game Clear!
                if(manager.stage == 2){
                    SceneManager.LoadScene(0);
                }
                else{
                    Debug.Log("다음 스테이지로 넘어갑니다!");
                    SceneManager.LoadScene(manager.stage + 1);
                }
            }
            else{
                //Restart
                SceneManager.LoadScene(manager.stage);
            }
        }
    }
}
