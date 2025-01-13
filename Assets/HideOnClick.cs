using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnClick : MonoBehaviour
{
    private void Update()
    {
        // Kiểm tra nếu nhấp chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            // Tạo ray từ vị trí chuột
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Kiểm tra va chạm với Collider của hình ảnh
            if (Physics.Raycast(ray, out hit) && hit.collider == GetComponent<Collider>())
            {
                // Thực hiện hành động khi hình ảnh được nhấp
                Debug.Log("Hình ảnh đã được nhấp!");
            }
        }
    }
}
