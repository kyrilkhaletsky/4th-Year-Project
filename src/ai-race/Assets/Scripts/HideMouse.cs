using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouse : MonoBehaviour {

    private Coroutine Hide;

    void Update() {
        if (Input.GetAxis("Mouse X") == 0 && (Input.GetAxis("Mouse Y") == 0)) {
            if (Hide == null) {
                Hide = StartCoroutine(HideCursor());
            }
        } else {
            if (Hide != null) {
                StopCoroutine(Hide);
                Hide = null;
                Cursor.visible = true;
            }
        }
    }

    private IEnumerator HideCursor() {
        yield return new WaitForSeconds(3);
        Cursor.visible = false;
    }
}
