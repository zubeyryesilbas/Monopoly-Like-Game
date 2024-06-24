using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dice
{
    public class PhysicalDice : DiceBase
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private DiceParamaterHoldersSO _diceParamaterHolder;
        private Transform _target;
       
        private void OnMouseDown()
        {
            Roll();
        }
        public void SetTarget(Transform target)
        {
            _target = target;
        }
        public async override void Roll()
        {
            _rigidbody.isKinematic = false;
            _isRolling = true;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            var maxTorque = _diceParamaterHolder.MaxTorque;
            var randomX = Random.Range(0, maxTorque);
            var randomY = Random.Range(0, maxTorque);
            var randomZ = Random.Range(0, maxTorque);
            var dir = transform.position - _target.transform.position;
            
            var finalForce =-dir.normalized * _diceParamaterHolder.MaxRollForce;
            var finalTorque = new Vector3(randomX, randomY, randomZ);
            
            _rigidbody.AddForce(finalForce);
            _rigidbody.AddTorque(finalTorque);
            await CheckRollingEndedAsync();
        }

        private async Task CheckRollingEndedAsync()
        {
            await Task.Delay(500); 

            while (_rigidbody.velocity.magnitude > 0.01f || _rigidbody.angularVelocity.magnitude > 0.01f)
            {
                await Task.Yield();
            }
            
            _isRolling = false;
            OnRollingEnd?.Invoke();
            _rigidbody.isKinematic = true;
        }
        public override int GetTopFace()
        { 
            var face = DiceConstants.GetFace(transform.eulerAngles);
            Debug.Log("Dice face" + face);
            return face;
        }
    }
}

