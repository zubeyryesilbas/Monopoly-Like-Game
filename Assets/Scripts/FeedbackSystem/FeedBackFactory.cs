using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;

public class FeedBackFactory : MonoBehaviour
{
    [SerializeField] private Feedback _multiplierFeedback, _looseAllFeedBack ,_earningFeedBack;
    
    public Feedback CreateFeedBack(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Looseall:
                _looseAllFeedBack.gameObject.SetActive(true);
                _looseAllFeedBack.Play();
                return _looseAllFeedBack;
                break;
            case ItemType.X2:
                _multiplierFeedback.gameObject.SetActive(true);
                _looseAllFeedBack.Play();
                return _multiplierFeedback;
                break;
            default:
                _earningFeedBack.gameObject.SetActive(true);
                _earningFeedBack.Play();
                return _earningFeedBack;
        }
    }
}
