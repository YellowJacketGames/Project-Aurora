public class DoorElementLoadNextLevel : DoorElement

{
    public override void OpenDoor()
    {
        // base.OpenDoor();
        //load next leve
        
        // GameManager.instance.currentTransitionManager.SetLoadingClip();
        GameManager.instance.currentTransitionManager.NextLevel(); 
    }
}