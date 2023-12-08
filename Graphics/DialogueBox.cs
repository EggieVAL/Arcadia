using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Arcadia.Graphics
{
    public class DialogueBox
    {
        private SpriteFont font; // Font for displaying text
        private Vector2 position; // Position of the dialogue box
        private string currentText; // Text to display
        private int maxCharsPerLine = 35; // Maximum characters per line
        private int charsPerFrame = 1; // Characters per frame for typing effect
        private float timeSinceLastChar = 0; // Time passed since last character displayed
        private int currentPage = 0; // Current page of dialogue
        private List<string> dialoguePages; // List of dialogue pages
        private bool isTyping; // Flag to check if typing effect is active

        public DialogueBox(SpriteFont font, Vector2 position)
        {
            this.font = font;
            this.position = position;
            dialoguePages = new List<string>();
            // Other initialization...
        }

        public void StartDialogue(List<string> dialogue)
        {
            dialoguePages.Clear();
            currentPage = 0;

            foreach (string page in dialogue)
            {
                // Split the dialogue into pages with word wrapping
                string[] words = page.Split(' ');
                string wrappedText = string.Empty;
                string line = string.Empty;

                foreach (string word in words)
                {
                    if ((line + word).Length > maxCharsPerLine)
                    {
                        wrappedText += line.Trim() + "\n";
                        line = string.Empty;
                    }
                    line += word + " ";
                }

                wrappedText += line.Trim();
                dialoguePages.Add(wrappedText);
            }

            currentText = dialoguePages[currentPage];
            isTyping = true;
        }

        public void Update(GameTime gameTime)
        {
            if (isTyping)
            {
                timeSinceLastChar += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timeSinceLastChar >= 0.05f)
                {
                    timeSinceLastChar = 0;

                    if (currentText.Length < dialoguePages[currentPage].Length)
                    {
                        currentText = dialoguePages[currentPage].Substring(0, currentText.Length + charsPerFrame);
                    }
                    else
                    {
                        isTyping = false;
                    }
                }
            }
        }

        public void NextPage()
        {
            if (currentPage < dialoguePages.Count - 1)
            {
                currentPage++;
                currentText = string.Empty;
                isTyping = true;
            }
            else
            {
                // End of dialogue
                currentText = string.Empty;
                currentPage = 0;
                // Other actions when dialogue ends...
            }
        }

		public void Draw(SpriteBatch spriteBatch)
		{
			// Draw the dialogue box background
			spriteBatch.Draw(dialogueBoxTexture, dialogueBoxPosition, Color.White);

			// Calculate text area within the dialogue box
			Vector2 textAreaPosition = new Vector2(dialogueBoxPosition.X + padding, dialogueBoxPosition.Y + padding);

			// Set the maximum width for text within the dialogue box
			float maxTextWidth = dialogueBoxWidth - (2 * padding);

			// Draw the current text
			Vector2 textPosition = textAreaPosition;
			string[] lines = currentText.Split('\n');
	
			foreach (string line in lines)
		{
			// Draw each line of text within the dialogue box
			spriteBatch.DrawString(font, line, textPosition, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

			// Increment Y position for the next line
			textPosition.Y += font.MeasureString(line).Y;
    }
}

    }
}
