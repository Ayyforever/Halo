using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CG1script : MonoBehaviour
{
    public GameObject cg1Object;
    public GameObject playerObject;
    private PlayableDirector cg1Timeline;

    private void Start()
    {
        cg1Timeline = cg1Object.GetComponent<PlayableDirector>();
        cg1Timeline.stopped += OnCG1TimelineStopped;
    }

    private void OnCG1TimelineStopped(PlayableDirector director)
    {
        DeactivateCG1();
        EnablePlayer();
    }

    private void DeactivateCG1()
    {
        cg1Object.SetActive(false);
    }

    private void EnablePlayer()
    {
        playerObject.SetActive(true);
    }
}
