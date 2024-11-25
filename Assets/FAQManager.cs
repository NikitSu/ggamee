using System.Collections.Generic;
using UnityEngine;

public class FAQManager : MonoBehaviour
{
    public List<FAQItem> faqItems;

    public void ToggleItem(FAQItem itemToToggle)
    {
        foreach (FAQItem item in faqItems)
        {
            if (item == itemToToggle)
            {
                item.ToggleAnswer();
            }
            else
            {
                item.CloseAnswer();
            }
        }
    }
}
