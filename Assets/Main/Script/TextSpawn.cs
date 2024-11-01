using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSpawn : MonoBehaviour
{
  private TextMeshProUGUI _text;
  [SerializeField] private Animator _animator;
//   private void Start()
//   {
//     _text = gameObject.GetComponent<TextMeshProUGUI>();
//     _animator = gameObject.GetComponent<Animator>();
//     Debug.Log(gameObject.GetComponent<TextMeshProUGUI>());
//   }

  public void SetupText(int val)
  {
    _text = gameObject.GetComponent<TextMeshProUGUI>();
    if (_text)
    {
      if (val >= 0)
      {
        _text.text = "+" + val.ToString();
      }
      else
      {
        _text.text = val.ToString();
      }
    }
    if (_animator)
    {
      _animator.SetTrigger("MoveText");
    }
    Destroy(gameObject.transform.parent.gameObject, 3f);
  }
}
