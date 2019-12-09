using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewController : MonoBehaviour
{
    public Camera cam;
    public GameObject cookie;
    public GameObject pumpkin;
    public float timeLeft;
    public Text timerText;
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject splashScreen;
    public GameObject playButton;

    private float maxWidth;
    public static bool playing;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
       
        playing = false;
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float cookieWidth = cookie.gameObject.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - cookieWidth;
        Spawn();

        UpdateText();
    }

    void FixedUpdate()
    {
        if (playing)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 0;
            }
            UpdateText();
        }

    }

    public void PlayGame ()
    {
        splashScreen.SetActive(false);
        playButton.SetActive(false);
        StartCoroutine(Spawn());
        playing = true;
    } 
    IEnumerator Spawn ()
    {
        yield return new WaitForSeconds(1.0f);

        while (timeLeft > 0)
        {
            // Spawn cookie
            Vector3 spawnPosition = new Vector3(
                Random.Range(-maxWidth, maxWidth),
                transform.position.y,
                0.0f
            );
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(cookie, spawnPosition, spawnRotation);

            // Spawn pumpkin
            spawnPosition = new Vector3(
                Random.Range(-maxWidth, maxWidth),
                transform.position.y,
                0.0f
            );
            spawnRotation = Quaternion.identity;
            Instantiate(pumpkin, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }
        yield return new WaitForSeconds(1.0f);
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        restartButton.SetActive(true);
    }

    void UpdateText()
    {
            timerText.text = "Time Left:\n" + Mathf.RoundToInt(timeLeft);
    }
}
