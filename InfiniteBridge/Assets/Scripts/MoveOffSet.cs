using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffSet : MonoBehaviour
{
//privacidade Tipo Nome
    private Renderer rendererizadorMalha;
    private Material materialAtual;
    private float offSet;
    public float velocidade;
    public float incrementoOffSet;
    public int ordemRenderizacao;

    // Start is called before the first frame update
    void Start()
    {
        rendererizadorMalha = GetComponent<Renderer>();
        rendererizadorMalha.sortingOrder = ordemRenderizacao;
        materialAtual = rendererizadorMalha.material;
    }

    // Update is called once per frame
    void Update()
    {
        offSet += incrementoOffSet;
        materialAtual.SetTextureOffset("_MainTex",new Vector2(offSet * velocidade,0));
    }
}
