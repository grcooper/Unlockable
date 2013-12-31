using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Unlockable
{
    class Hero : Sprite
    {
        const string HERO_ASSETNAME = "hero";
        const int START_POSITION_X = 30;
        const int START_POSITION_Y = 390;
        const int HERO_SPEED_Y = 180;
        const int HERO_SPEED_X = 160;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;

        //Get score
        public int HERO_SCORE = 0;

        //check if they can move to the side.
        public bool moveUp = true;
        public bool moveDown = true;
        public bool moveLeft = true;
        public bool moveRight = true;

        public bool touchingOrb1 = true;
        public bool touchingOrb2 = true;
        public bool touchingOrb3 = true;
        public bool touchingOrb4 = true;
        public bool touchingOrb5 = true;
        public bool touchingOrb6 = true;
        public bool touchingOrb7 = true;
        public bool touchingOrb8 = true;

        public bool aorb = false;

        public int a1 = 0;
        public int a2 = 0;
        public int a3 = 0;
        public int a4 = 0;
        public int a5 = 0;

        public bool HERO_DEAD = false;
        public bool openExit = false;
        public bool win = false;

        public int jumpCounter = 0;

        enum State
        {
            Walking,
            Jumping
        }
        State currentState = State.Walking;

        Vector2 direction = Vector2.Zero;
        Vector2 speed = Vector2.Zero;
        Vector2 startingPosition = Vector2.Zero;

        KeyboardState lastKeyboardState;

        
        public void LoadContent(ContentManager contentManager)
        {
            spritePosition = new Vector2(START_POSITION_X, START_POSITION_Y);
            base.LoadContent(contentManager, HERO_ASSETNAME);
        }

        public void Update(GameTime gameTime)
        {
            

            KeyboardState currentKeyboardState = Keyboard.GetState();

            UpdateMovement(currentKeyboardState);
            UpdateJump(currentKeyboardState);

            lastKeyboardState = currentKeyboardState;

            base.Update(gameTime, speed, direction);
        }

        public void UpdateMovement(KeyboardState currentKeyboardState)
        {
            jumpCounter++;
            if (currentState == State.Walking)
            {
                speed = Vector2.Zero;
                direction = Vector2.Zero;

                if (moveDown == true)
                {
                    speed.Y = HERO_SPEED_Y;
                    direction.Y = MOVE_DOWN;
                    AirMove(currentKeyboardState);
                }
                else if (currentKeyboardState.IsKeyDown(Keys.Left) && moveLeft == true)
                {
                    speed.X = HERO_SPEED_X;
                    direction.X = MOVE_LEFT;
                }
                else if (currentKeyboardState.IsKeyDown(Keys.Right) && moveRight == true)
                {
                    speed.X = HERO_SPEED_X;
                    direction.X = MOVE_RIGHT;
                }
            }
        }

        private void UpdateJump(KeyboardState currentKeyboardState)
        {
            if (currentState == State.Walking)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Space))
                {
                    Jump();
                }
            }

            if (currentState == State.Jumping)
            {
                AirMove(currentKeyboardState);

                if (startingPosition.Y - spritePosition.Y > 75 + speed.Y - 180 || moveLeft == false || moveRight == false || moveUp == false)
                {
                    direction.Y = MOVE_DOWN;
                    
                    if (moveLeft == false || moveRight == false)
                    {
                        speed = new Vector2(0, HERO_SPEED_Y);
                    }
                }

                if ((moveDown == false || moveLeft == false || moveRight == false) && direction.Y == MOVE_DOWN)
                {
                    currentState = State.Walking;
                    direction = Vector2.Zero;
                }
            }
            
        }

        private void Jump()
        {
            if (currentState != State.Jumping)
            {
                currentState = State.Jumping;
                speed = new Vector2(HERO_SPEED_X, HERO_SPEED_Y);
                startingPosition = spritePosition;
                direction.Y = MOVE_UP;
                jumpCounter = 0;
                
            }
        }

        private void AirMove(KeyboardState currentKeyboardState)
        {
            if (currentKeyboardState.IsKeyDown(Keys.Left) && moveLeft == true)
                {
                    if (direction.X == MOVE_LEFT)
                    {
                        speed.X = HERO_SPEED_X + 30;
                        
                    }
                    else if (direction.X == MOVE_RIGHT)
                    {
                        speed.X = HERO_SPEED_X - 180;
                    }
                    else
                    {
                        speed.X = HERO_SPEED_X;
                        direction.X = MOVE_LEFT;
                    }
                }

            else if (currentKeyboardState.IsKeyDown(Keys.Right) && moveRight == true)
            {
                if (direction.X == MOVE_RIGHT)
                {
                    speed.X = HERO_SPEED_X + 30;
                }
                else if (direction.X == MOVE_LEFT)
                {
                    speed.X = HERO_SPEED_X - 190;
                }
                else
                {
                    speed.X = HERO_SPEED_X;
                    direction.X = MOVE_RIGHT;
                }
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) && moveUp == true)
            {
                if (direction.Y == MOVE_UP)
                {
                    speed.Y = HERO_SPEED_Y + 30;
                }
                else if (direction.Y == MOVE_DOWN)
                {
                    speed.Y = HERO_SPEED_Y - 60;
                }
                else
                {
                    speed.Y = HERO_SPEED_Y;
                }
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Down) && moveDown == true)
            {
                if (direction.Y == MOVE_DOWN)
                {
                    speed.Y = HERO_SPEED_Y + 30;
                }

                else if (direction.Y == MOVE_UP)
                {
                    speed.Y = HERO_SPEED_Y - 60;
                }
                else
                {
                    speed.Y = HERO_SPEED_Y;
                }
            }

                
        }
    }
}
