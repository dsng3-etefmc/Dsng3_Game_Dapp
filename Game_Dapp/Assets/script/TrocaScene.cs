using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaScene : MonoBehaviour
{
    public int SceneValue;
    BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        SceneManager.LoadScene(SceneValue);
    }
    void OnCollisionEnter2D(Collision2D coll) {
        print("touching");
        if (coll.gameObject.tag == "Player")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            print("Player touching");
            SceneManager.LoadScene(SceneValue);
        }
    }

}
