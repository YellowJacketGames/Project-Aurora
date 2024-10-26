using System;
using System.Linq;
using Cinemachine;
using UnityEngine;

namespace InteractableElements
{
    public class CodexManager : MonoBehaviour
    {
        [SerializeField] private Codex[] codexes;

        [SerializeField] private Codex currentCodex;
        private int currentCodexIndex = 0;
        [SerializeField] private CodexElement currentCodexElement;


        [SerializeField] private bool inCodexesTab = true;
        [SerializeField] private bool inCodexesElementsTab;

        [SerializeField] private CinemachineVirtualCamera puzzleCamera;
        private PuzzleDoorElement _puzzleDoorElement;

        private void Awake()
        {
            _puzzleDoorElement = GetComponentInParent<PuzzleDoorElement>();
            codexes = GetComponentsInChildren<Codex>();
        }

        private void Start()
        {
            currentCodex = codexes[currentCodexIndex];
            currentCodex.Select(puzzleCamera);
        }

        private void OnEnable()
        {
            EventsManager.OnCodexDown.AddListener(MoveDown);
            EventsManager.OnCodexUp.AddListener(MoveUp);
            EventsManager.onCodexIn.AddListener(MoveIn);
            EventsManager.onCodexOut.AddListener(MoveOut);
            EventsManager.OnCodexLeft.AddListener(MoveLeft);
            EventsManager.OnCodexRight.AddListener(MoveRight);
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

        private void MoveUp()
        {
            Debug.Log("MoveUp");
            if (inCodexesTab)
            {
                currentCodexIndex--;
                if (currentCodexIndex < 0)
                    currentCodexIndex = codexes.Length - 1;
                currentCodex = codexes[currentCodexIndex];
                DeselectCodexes();
                currentCodex.Select(puzzleCamera);
            }

            if (inCodexesElementsTab)
            {
                currentCodex.MoveUp();
            }
        }

        private void MoveLeft()
        {
            if (inCodexesTab) return;
            currentCodex.MoveLeft();
        }

        private void MoveRight()
        {
            if (inCodexesTab) return;
            currentCodex.MoveRight();
        }

        private void MoveDown()
        {
            Debug.Log("MoveDown");
            if (inCodexesTab)
            {
                currentCodexIndex++;
                if (currentCodexIndex > codexes.Length - 1)
                    currentCodexIndex = 0;
                currentCodex = codexes[currentCodexIndex];
                DeselectCodexes();
                currentCodex.Select(puzzleCamera);
            }

            if (inCodexesElementsTab)
            {
                currentCodex.MoveDown();
            }
        }


        private void MoveOut()
        {
            if (inCodexesElementsTab)
            {
                inCodexesTab = true;
                inCodexesElementsTab = false;
                DeselectCodexes();
                currentCodex.Select(puzzleCamera);
                return;
            }

            if (inCodexesTab)
            {
                inCodexesTab = true;
                _puzzleDoorElement.ExitPuzzle();
            }

            Debug.Log("MoveOut");
        }

        private void MoveIn()
        {
            if (inCodexesTab)
            {
                inCodexesTab = false;
                inCodexesElementsTab = true;
                //3.35 new camera x position, to zoom in
                currentCodex.SuperSelect(puzzleCamera);
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