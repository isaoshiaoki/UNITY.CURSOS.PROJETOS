using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class Temporario : MonoBehaviour
{
    public int idPersonagem;
    public string nomePersonagem;
    public int pontuacao;
    public string[] letras;
    // Start is called before the first frame update
    void Start()
    {

        //salva no pc
        print(Application.persistentDataPath);


        //salva onde o jogo esta sendo salvo
        print(Application.dataPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("Save Game");
            save();
        }
    }
    //salva arquivo
    public void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveGame.dat");

        PlayerData2 data = new PlayerData2();
        data.idPersonagem = idPersonagem;
        data.nomePersonagem = nomePersonagem;
        data.pontuacao = pontuacao;
        data.letras = letras;
        bf.Serialize(file,data);
        file.Close();
    }


    //carrega arquivo
    public void load()
    {
        if (File.Exists(Application.persistentDataPath + "/saveGame.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveGame.dat",FileMode.Open);
            PlayerData2 data = (PlayerData2) bf.Deserialize(file);

            idPersonagem = data.idPersonagem;
            nomePersonagem = data.nomePersonagem;
            pontuacao = data.pontuacao;
            letras = data.letras;
        }
    }



}
[Serializable]

  class PlayerData2
{
    public int idPersonagem;
    public string nomePersonagem;
    public int pontuacao;

    public string[] letras;

}