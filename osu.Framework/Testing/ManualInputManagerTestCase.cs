﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using NUnit.Framework;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing.Input;
using OpenTK;

namespace osu.Framework.Testing
{
    /// <summary>
    /// An abstract test case which is tested with manual input management.
    /// </summary>
    public abstract class ManualInputManagerTestCase : TestCase
    {
        protected override Container<Drawable> Content => InputManager;

        protected virtual Vector2? InitialMousePosition => null;

        protected ManualInputManager InputManager { get; }

        protected ManualInputManagerTestCase()
        {
            base.Content.Add(InputManager = new ManualInputManager());
            AddStep("return user input", () => InputManager.UseParentInput = true);
        }

        protected void ResetInput()
        {
            InputManager.UseParentInput = false;
            var currentState = InputManager.CurrentState;

            var mouse = currentState.Mouse;
            var position = InitialMousePosition;
            if (position != null) InputManager.MoveMouseTo(position.Value);
            mouse.Buttons.ForEach(InputManager.ReleaseButton);

            var keyboard = currentState.Keyboard;
            keyboard.Keys.ForEach(InputManager.ReleaseKey);
        }

        [SetUp]
        public virtual void SetUp()
        {
            ResetInput();
        }
    }
}
