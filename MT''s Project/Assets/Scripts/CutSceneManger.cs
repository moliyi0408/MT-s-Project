using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CutSceneManger : MonoBehaviour
{
  //  public GameObject videoPlayer;
    public PlayableDirector timeline;
    public GameObject thePlayer;
    public GameObject timelineCanvas;
    //public GameObject cutSceneCam;
    public float secs = 5.0f;
   // public Vector3 playerChange; //¨Ïª±®a²¾°Ê

    void Start()
    {
       // videoPlayer.SetActive(false);
        timeline.GetComponent<PlayableDirector>();
        timeline.enabled = false;
        if (timelineCanvas.activeInHierarchy)
            timelineCanvas.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timelineCanvas.SetActive(true);
            timeline.enabled = true;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //  cutSceneCam.SetActive(true);
            //collision.transform.position += playerChange;
            thePlayer.SetActive(false);
           // videoPlayer.SetActive(true);
            timeline.Play();
            Debug.Log("Video is playing");
            StartCoroutine(FinishCut());
            
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timelineCanvas.SetActive(false);
        }
    }
       
   
    public IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(secs);
        thePlayer.SetActive(true);
        //cutSceneCam.SetActive(false);
        //Destroy(videoPlayer);
    }
}
