using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoWorkAround : MonoBehaviour
{
    [SerializeField]
    string videoName = "Upper";
    VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer)
        {
            if (videoPlayer.clip)
            {
                videoName = videoPlayer.clip.name;
            }
            videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,videoName+"_SA.mp4");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
