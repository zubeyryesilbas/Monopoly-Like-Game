using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseSystem : MonoBehaviour
{
        [SerializeField] private CaseView _caseView;
        public void Initialize(BoardController boardController  ,  SpriteTypeHolderSO  spriteTypeHolderSo , IItemManager itemManager , ICharacterManager characterManager , EventManager eventManager)
        {
            var model = new CaseModel(itemManager, characterManager);
            var presenter = new CasePresenter(model, _caseView,boardController, spriteTypeHolderSo , eventManager);
        }
}
