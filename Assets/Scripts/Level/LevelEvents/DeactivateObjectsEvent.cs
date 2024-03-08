using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObjectsEvent : LevelEvent
{
    [SerializeField] private List<GameObject> objectsToDeactivate;

    [SerializeField] private float disappearTime;
    public override void OnEvent()
    {
        base.OnEvent();

        foreach (GameObject o in objectsToDeactivate)
        {
            o.transform.position = new Vector3(1000, 1000, 1000);
        }
    }


    //I'm not able to make this work so i'll leave it until i come up with a better solution.
    //The only solution that comes to mind is that we create to different dialogue files when making a 
    //conversation with this type of event, and when the event triggers the conversation stops and does the event.
    //When the event is done, it loads the new dialogue, but I'm pretty sure we can come up with a better solution.
    IEnumerator DisappearObjects()
    {
        GameManager.instance.currentController.playerUIComponent.HideConversationBox();
        GameManager.instance.currentTransitionManager.SetFadeIn();
        GameManager.instance.currentController.playerConversationComponent.canContinue = false;

        yield return new WaitForSeconds(disappearTime);

        

        yield return new WaitForSeconds(0.5f);
        GameManager.instance.currentTransitionManager.SetFadeOut();
        GameManager.instance.currentController.ChangeState(PlayerState.Conversation); 

        yield return new WaitForSeconds(disappearTime);
        GameManager.instance.currentController.playerConversationComponent.canContinue = true;

        GameManager.instance.currentController.playerUIComponent.ShowConversationBox();

        eventRunning = false;

    }
}
