using System;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace InteractableElements
{
    public class CodexManager : MonoBehaviour
    {
        [SerializeField] private CodexGroup[] codexes;

        public CodexGroup[] Codexes => codexes;

        public CodexGroup codexesEagle;
        public CodexGroup codexeCroco;
        public CodexGroup codexeBull;

        [FormerlySerializedAs("currentCodex")] [SerializeField]
        private CodexGroup currentCodexGroup;

        private int currentCodexIndex = 0;
        [SerializeField] private CodexElement currentCodexElement;


        [SerializeField] private bool inCodexesTab = true;
        [SerializeField] private bool inCodexesElementsTab;

        [SerializeField] private CinemachineVirtualCamera puzzleCamera;
        private PuzzleDoorElement _puzzleDoorElement;


        private string papire_eagle = "obj_Pergamino_Aguila"; //GameManager.instance.Data.HasObject(valueKey)
        private string papire_bull = "obj_Pergamino_Toro";
        private string papire_croco = "obj_Pergamino_Cocodrilo";

        private void Awake()
        {
            _puzzleDoorElement = GetComponentInParent<PuzzleDoorElement>();
            codexes = GetComponentsInChildren<CodexGroup>();
        }

        private void Start()
        {
            currentCodexGroup = codexes[currentCodexIndex];
            // currentCodexGroup.Select(puzzleCamera);
        }

        private void OnEnable()
        {
            EventsManager.OnCodexDown.AddListener(MoveDown);
            EventsManager.OnCodexUp.AddListener(MoveUp);
            EventsManager.onCodexIn.AddListener(MoveIn);
            EventsManager.onCodexOut.AddListener(MoveOut);
            EventsManager.OnCodexLeft.AddListener(MoveLeft);
            EventsManager.OnCodexRight.AddListener(MoveRight);

            CheckUnlocks();
        }

        private void OnDisable()
        {
            EventsManager.OnCodexDown.RemoveListener(MoveDown);
            EventsManager.OnCodexUp.RemoveListener(MoveUp);
            EventsManager.onCodexIn.RemoveListener(MoveIn);
            EventsManager.onCodexOut.RemoveListener(MoveOut);
            EventsManager.OnCodexLeft.RemoveListener(MoveLeft);
            EventsManager.OnCodexRight.RemoveListener(MoveRight);
        }

        private void CheckUnlocks()
        {
            if (GameManager.instance.Data.HasObject(papire_eagle))
                codexesEagle.UnlockCodex();
            if (GameManager.instance.Data.HasObject(papire_croco))
                codexeCroco.UnlockCodex();
            if (GameManager.instance.Data.HasObject(papire_bull))
                codexeBull.UnlockCodex();
        }

        private void MoveUp()
        {
            Debug.Log("MoveUp");
            if (inCodexesTab)
            {
                var auxIndex = currentCodexIndex;
                auxIndex--;
                if (auxIndex < 0)
                    auxIndex = codexes.Length - 1;

                if (codexes[auxIndex].GetUnlocked())
                {
                    currentCodexIndex--;
                    if (currentCodexIndex < 0)
                        currentCodexIndex = codexes.Length - 1;
                    currentCodexGroup = codexes[currentCodexIndex];
                    DeselectCodexes();
                    currentCodexGroup.Select(puzzleCamera);
                }
            }

            if (inCodexesElementsTab)
            {
                currentCodexGroup.MoveUp();
            }
        }

        private void MoveLeft()
        {
            if (inCodexesTab) return;
            currentCodexGroup.MoveLeft();
        }

        private void MoveRight()
        {
            if (inCodexesTab) return;
            currentCodexGroup.MoveRight();
        }

        private void MoveDown()
        {
            Debug.Log("MoveDown");
            if (inCodexesTab)
            {
                var auxIndex = currentCodexIndex;
                auxIndex++;
                if (auxIndex > codexes.Length - 1)
                    auxIndex = 0;
                if (codexes[auxIndex].GetUnlocked())
                {
                    currentCodexIndex++;
                    if (currentCodexIndex > codexes.Length - 1)
                        currentCodexIndex = 0;
                    currentCodexGroup = codexes[currentCodexIndex];
                    DeselectCodexes();
                    currentCodexGroup.Select(puzzleCamera);
                }
            }

            if (inCodexesElementsTab)
            {
                currentCodexGroup.MoveDown();
            }
        }


        public void MoveOut()
        {
            if (inCodexesElementsTab)
            {
                inCodexesTab = true;
                inCodexesElementsTab = false;
                DeselectCodexes();
                currentCodexGroup.Select(puzzleCamera);
                return;
            }

            if (inCodexesTab)
            {
                inCodexesTab = true;
                DeselectCodexes();
                _puzzleDoorElement.ExitPuzzle();
            }

            Debug.Log("MoveOut");
        }

        private void MoveIn()
        {
            if (inCodexesElementsTab)
            {
                if(currentCodexGroup.selectedElement.IsTryCombinationButton)
                    currentCodexGroup.MoveIn();
            }
            if (inCodexesTab)
            {
                if (!currentCodexGroup.GetUnlocked()) return;
                if (currentCodexGroup.GetDeciphered()) return;
                inCodexesTab = false;
                inCodexesElementsTab = true;
                //3.35 new camera x position, to zoom in
                currentCodexGroup.SuperSelect(puzzleCamera);
                return;
            }

            Debug.Log("MoveIn");
        }

        private void DeselectCodexes()
        {
            foreach (var codex in codexes)
            {
                codex.Deselect();
            }
        }
    }
}