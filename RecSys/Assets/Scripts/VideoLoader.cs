using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.General;
using UnityEngine;
using UnityEngine.Networking;
using VideoLibrary;
using YoutubeExtractor;

public class VideoLoader : MonoBehaviour
{
    private string url = "";
    private UnityEngine.Video.VideoPlayer videoPlayer;
    //public UnityEngine.UI.RawImage rawImage;

    void Start()
    {
        videoPlayer = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    public bool ChangeVideo(string _newUrl)
    {
        Debug.Log("Receive: " + _newUrl);
        url = _newUrl;

        Renderer rend = GetComponent<Renderer>();
        rend.material = new Material(Shader.Find("Unlit/Transparent"));

        var youTube = YouTube.Default; // starting point for YouTube actions

        
        try
        {
            Debug.Log("Getting URI");
            var video = youTube.GetVideo(url); // gets a Video object with info about the video
            Debug.Log(video.Uri);
            videoPlayer.url = video.Uri;
        }
        catch (System.InvalidOperationException e)
        {
            Debug.Log(e);
            return false;
        }
        catch (System.ArgumentNullException e)
        {
            Debug.Log(e);
            return false;
        }

        //File.WriteAllBytes(@"C:\" + video.FullName, video.GetBytes());
        PauseVideoPlaying();
        return true;
    }

    public void ChangeVideoPlayMode()
    {
        if (videoPlayer.isPlaying)
        {
            PauseVideoPlaying();
        }
        else if (videoPlayer.isPaused)
        {
            PlayVideoPlaying();
        }
    }

    public void StopVideoPlaying()
    {
        videoPlayer.Stop();
        Renderer rend = GetComponent<Renderer>();
        var color = new Color(255, 255, 225, 0);
        rend.material = new Material(Shader.Find("UI/Lit/Transparent"));
        rend.material.color = color;
    }

    private void PauseVideoPlaying()
    {
        videoPlayer.Pause();
    }

    private void PlayVideoPlaying()
    {
        videoPlayer.Play();
    }
}
