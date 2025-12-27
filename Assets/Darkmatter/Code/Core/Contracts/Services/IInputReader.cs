using System;
using UnityEngine;

namespace Darkmatter.Core
{
    public interface IInputReader
    {
        public event Action OnJumpPerformed;
        public Vector2 moveInput { get; }
        public Vector2 lookInput { get; }
        public bool isShooting { get; }
    }
}
