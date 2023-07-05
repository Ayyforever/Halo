using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CG2Trigger : MonoBehaviour
{
    public GameObject cg2Object;
    public GameObject playerObject;
    private PlayableDirector timeline;
    private bool hasTriggered = false;

    private void Start()
    {
        timeline = cg2Object.GetComponent<PlayableDirector>();
        cg2Object.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            ActivateCG2();
        }
    }

    private void ActivateCG2()
    {
        hasTriggered = true;
        playerObject.SetActive(false); // 激活Player对象
        playerObject.transform.position = new Vector3(-146, -14, -66); // 修改Player位置为 (0, 0, 0)
        playerObject.transform.rotation = Quaternion.Euler(0, -90, 0); // 修改Player旋转角度为 (0, 180, 0)
        cg2Object.SetActive(true);
        timeline.Play();
        timeline.stopped += OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        DeactivateCG2();
        playerObject.SetActive(true); // 在timeline播放结束后激活Player对象
    }

    private void DeactivateCG2()
    {
        cg2Object.SetActive(false);
        timeline.stopped -= OnTimelineStopped;
    }
}


