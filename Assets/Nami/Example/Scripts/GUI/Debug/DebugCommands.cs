using System;
using System.Collections.Generic;
using UnityEngine;

namespace NamiExample
{
    public class DebugCommands : MonoBehaviour
    {
        [SerializeField] private CommandButton commandButtonInstance;

        private Dictionary<string, Action> commands;
        private readonly List<CommandButton> commandButtons = new List<CommandButton>();

        private void Awake()
        {
            InitCommands();
            foreach (var command in commands)
            {
                var commandButton = Instantiate(commandButtonInstance, commandButtonInstance.transform.parent);
                commandButton.Name = command.Key;
                commandButton.Action = command.Value;

                commandButtons.Add(commandButton);
            }

            commandButtonInstance.gameObject.SetActive(false);
        }

        private void InitCommands()
        {
            commands = new Dictionary<string, Action>
            {
                {
                    "example", () =>
                    {
                        Debug.Log("example called");
                    }
                },
                {
                    "example2", () =>
                    {
                        Debug.Log("example2 called");
                    }
                },
            };
        }
    }
}
