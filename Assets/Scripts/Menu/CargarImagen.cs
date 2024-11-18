using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CargarImagen : MonoBehaviour
{

    public string imageUrl="https://img.freepik.com/fotos-premium/planetas-e-fundo-de-nebulosa-em-estilo-pixel-art_534972-2443.jpg";
    public Image targetImage;


    void Start()
    {
        StartCoroutine(LoadImage());
    }

    private IEnumerator LoadImage()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error al cargar la imagen");
        }
        else
        {
            //Descargar la textura
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            //Convertir la textura a Sprite y se asigna a sprite
            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            targetImage.sprite = newSprite;
        }
    }

}
