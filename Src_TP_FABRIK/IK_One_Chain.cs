using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK_One_Chain : MonoBehaviour
{
    // Le transform (noeud) racine de l'arbre,
    // le constructeur créera une sphère sur ce point pour en garder une copie visuelle.
    public GameObject rootNode = null;
    // Un transform (noeud) (probablement une feuille) qui devra arriver sur targetNode
    public Transform srcNode = null;
    // Le transform (noeud) cible pour srcNode
    public Transform targetNode = null;
    // Si vrai, recréer toutes les chaines dans Update
    public bool createChains = true;
    // Toutes les chaines cinématiques
    public List<IKChain_One_Chain> chains = new List<IKChain_One_Chain>();
    // Nombre d'itération de l'algo à chaque appel
    public int nb_ite = 10;
    void Start()
    {
        if (createChains)
        {
            Debug.Log("(Re)Create CHAIN");
            createChains = false;
            // la chaîne est créée une seule fois, au début
            // TODO :
            // Création des chaînes : une chaîne cinématique est un chemin entre deux nœuds carrefours.
            // Dans la 1ere question, une unique chaine sera suffisante entre srcNode et rootNode.
            // TODO-1 : Créer une chaine cinématique entre srcNode et rootNode.
            GameObject sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere1.transform.position = rootNode.transform.position;

            chains.Add(new IKChain_One_Chain(rootNode.transform, srcNode, sphere1.transform, targetNode));
            
        }
    }

    void Update()
    {
        if (createChains)
            Start();
        if (Input.GetKeyDown(KeyCode.I))
        {
            IKOneStep(true);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            IKOneStep(false);
        }
        for(int j = 0; j < nb_ite; j++)
        {
            foreach(IKChain_One_Chain ch in chains)
            {
                ch.Backward();
                ch.ToTransform();
            }
        }
        for(int j = 0; j < nb_ite; j++)
        {
            foreach(IKChain_One_Chain ch in chains)
            {
                ch.Forward();
                ch.ToTransform();
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Chains count="+chains.Count);
            foreach (IKChain_One_Chain ch in chains)
            ch.Check();
        }
    }
    void IKOneStep(bool down)
    {
        if(down)
        {
            for(int j = 0; j < nb_ite; j++)
            {
                foreach(IKChain_One_Chain ch in chains)
                {
                    ch.Backward();
                    ch.ToTransform();
                }
            }
        }
        else
        {
            for(int j = 0; j < nb_ite; j++)
            {
                foreach(IKChain_One_Chain ch in chains)
                {
                    ch.Forward();
                    ch.ToTransform();
                }
            }
        }

    }
}
