/*
 * Copyright (c) 2020 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;

namespace RayWenderlich.KQClone.Utilities
{
    public struct ParsedCommand
    {
        public string verb;
        public string primaryEntity;
        public string secondaryEntity;
    }

    public static class CommandParser
    {
        private static readonly string[] Verbs = { "get", "look", "pick", "pull", "push" };
        private static readonly string[] Prepositions = { "to", "at", "up", "into", "using", "with" };
        private static readonly string[] Articles = { "a", "an", "the" };

        //2
        public static ParsedCommand Parse(string command)
        {
            var pCmd = new ParsedCommand();
            var words = new Queue<string>(command.ToLowerInvariant().
                Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries));

            try
            {
                if (Verbs.Contains(words.Peek())) pCmd.verb = words.Dequeue();

                if (Prepositions.Contains(words.Peek())) words.Dequeue();

                if (Articles.Contains(words.Peek())) words.Dequeue();

                pCmd.primaryEntity = words.Dequeue();
                while (!Prepositions.Contains(words.Peek()))
                    pCmd.primaryEntity = $"{pCmd.primaryEntity} {words.Dequeue()}";
                words.Dequeue();

                if (Articles.Contains(words.Peek())) words.Dequeue();

                pCmd.secondaryEntity = words.Dequeue();
                while (words.Count > 0)
                    pCmd.secondaryEntity = $"{pCmd.secondaryEntity} {words.Dequeue()}";
            }
            catch (System.InvalidOperationException)
            {
                return pCmd;
            }

            return pCmd;
        }

        //1
        public static bool Contains(this string[] array, string element)
        {
            return System.Array.IndexOf(array, element) != -1;
        }

    }
}