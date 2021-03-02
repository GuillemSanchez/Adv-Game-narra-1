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

using System.Collections;
using UnityEngine;

namespace RayWenderlich.KQClone.Core
{
    [RequireComponent(typeof(Animator), typeof(SetSortingOrder))]


    public class CharacterMovement : MonoBehaviour
    {
        private SetSortingOrder sortingScript;

        [SerializeField] private float speed = 2f;
        private Animator animator;
        private Vector2 currentDirection = Vector2.zero;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            animator.speed = 0;
            sortingScript = GetComponent<SetSortingOrder>();

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) ToggleMovement(Vector2.up);
            if (Input.GetKeyDown(KeyCode.LeftArrow)) ToggleMovement(Vector2.left);
            if (Input.GetKeyDown(KeyCode.DownArrow)) ToggleMovement(Vector2.down);
            if (Input.GetKeyDown(KeyCode.RightArrow)) ToggleMovement(Vector2.right);
        }

        private void StopMovement()
        {
            animator.speed = 0;
            StopAllCoroutines();
        }

        private void ToggleMovement(Vector2 direction)
        {
            StopMovement();

            if (currentDirection != direction)
            {
                animator.speed = 1;
                animator.SetInteger("X", (int)direction.x);
                animator.SetInteger("Y", (int)direction.y);
                StartCoroutine(MovementRoutine(direction));

                currentDirection = direction;
            }
            else
            {
                currentDirection = Vector2.zero;
            }
        }

        private IEnumerator MovementRoutine(Vector2 direction)
        {
            while (true)
            {
                sortingScript.SetOrder();

                transform.Translate(direction * speed * Time.deltaTime);
                yield return null;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            StopMovement();
        }


    }
}