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
      Matrix4x4 originalMatrix = Gizmos.matrix;
      Gizmos.matrix = transform.localToWorldMatrix;
      Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
      Gizmos.matrix = originalMatrix;

   }
}
