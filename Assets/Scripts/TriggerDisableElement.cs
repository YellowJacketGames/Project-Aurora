using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerDisableElement : MonoBehaviour
{
   [SerializeField] private GameObject target;
   [SerializeField] private bool value;
   [SerializeField] private string otherTag;
   private BoxCollider _boxCollider;

   private void Awake()
   {
      _boxCollider = GetComponent<BoxCollider>();
      _boxCollider.isTrigger = true;
   }
   private void OnTriggerEnter(Collider other)
   {
      if(other.gameObject.CompareTag(otherTag))
         target.SetActive(value);
   }
   private void OnDrawGizmos()
   {
      Gizmos.color = Color.green;
      Vector3 objectSize = transform.localScale;
      Gizmos.DrawWireCube(transform.position, objectSize);
   }
}
