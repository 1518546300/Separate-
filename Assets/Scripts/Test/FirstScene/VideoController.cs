using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public static VideoController instance;
    private UnityEngine.Video.VideoPlayer videoPlayer;
    private RawImage rawImage;
    private bool videoStatus;

    public GameObject videoCanvas;


    void Awake()
    {
        instance = this;
        videoPlayer = this.GetComponent<UnityEngine.Video.VideoPlayer>();
        rawImage = this.GetComponent<RawImage>();
        videoStatus = false;

        videoPlayer.prepareCompleted += OnPrepareFinish;
        videoPlayer.Prepare();
    }

    void Update()
    {
        if (videoPlayer.texture == null)
        {
            return;
        }
        rawImage.texture = videoPlayer.texture;
    }

    private void OnPrepareFinish(VideoPlayer player)
    {
        videoStatus = true;
    }

    public void PlayVideo()
    {
        if (videoStatus) {
            videoPlayer.Play();
            videoCanvas.GetComponent<Canvas>().sortingOrder = 10;
        }
        
    }
}
