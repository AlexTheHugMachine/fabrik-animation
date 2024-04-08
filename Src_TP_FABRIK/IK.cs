using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IK : MonoBehaviour
{
    // Le transform (noeud) racine de l'arbre,
    // le constructeur créera une sphère sur ce point pour en garder une copie visuelle.
    public GameObject rootNode = null;
    // Un transform (noeud) (probablement une feuille) qui devra arriver sur targetNode
    public Transform srcNode = null;
    public Transform srcNode2 = null;
    // Le transform (noeud) cible pour srcNode
    public Transform targetNode = null;
    public Transform targetNode2 = null;
    // Si vrai, recréer toutes les chaines dans Update
    public bool createChains = true;
    // Toutes les chaines cinématiques
    public List<IKChain> chains = new List<IKChain>();
    // Nombre d'itération de l'algo à chaque appel
    public int nb_ite = 10;

    public bool BackwardFini = true;
    public bool ForwardFini = false;
    public Transform sphere_root_a = null;
    public Transform sphere_root_b = null;
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

            chains.Add(new IKChain(rootNode.transform, rootNode.transform.GetChild(0).GetChild(0), sphere1.transform, null));
            //Debug.Log("Nombre de childs de 0="+chains[0].First().transform.childCount);
            //chains.Add(RecursiveChain(rootNode.transform));
            //RecursiveChain(rootNode.transform, srcNode2, targetNode2);
            foreach (Transform root in gameObject.GetComponentsInChildren<Transform>())
            {
                // TODO-2 : Créer une chaine cinématique entre srcNode et tr.
                // TODO-2 : Créer une chaine cinématique entre tr et rootNode.
                // rajouter un test pour savoir si tr est un carrefour
                //Debug.Log("BackwardFini="+BackwardFini);
                //Debug.Log("là");
                if (root.childCount > 1)
                {
                    MultiChain(root);
                }
            }
            
            /*Debug.Log("Nombre de chaines="+chains.Count);
            Debug.Log("Nombre de joints 0="+chains[0].Count());
            Debug.Log("Noms des joints 0="+chains[0].GetJoint(2).transform.name);
            Debug.Log("Nombre de joints 1="+chains[1].Count());
            Debug.Log("Nombre de joints 2="+chains[2].Count());*/

            // TODO-2 : Pour parcourir tous les transform d'un arbre d'Unity vous pouvez faire une
            // fonction récursive
            // ou utiliser GetComponentInChildren comme ceci :
            /*foreach (Transform tr in gameObject.GetComponentsInChildren<Transform>())
            {
                // TODO-2 : Créer une chaine cinématique entre srcNode et tr.
                // TODO-2 : Créer une chaine cinématique entre tr et rootNode.
                // rajouter un test pour savoir si tr est un carrefour
                if (tr.childCount > 1)
                {
                    MultiChain(tr);
                    //chains.Add(new IKChain(tr, srcNode, sphere2.transform, targetNode));
                }
            }*/
    
            // TODO-2 : Dans le cas où il y a plusieurs chaines, fusionne les IKJoint entre chaque
            // articulation.
            /*foreach (IKChain ch in chains)
            {
                foreach (IKChain ch2 in chains)
                {
                    if (ch != ch2)
                    {
                        ch.Merge(ch2.First());
                    }
                }
            }*/
            
        }
    }
    void MultiChain(Transform root)
    {
        chains.Add(new IKChain(root.GetChild(0).transform, srcNode, null, targetNode));
        chains.Add(new IKChain(root.GetChild(1).transform, srcNode2, null, targetNode2));
        sphere_root_a.position = root.position;
        sphere_root_b.position = root.position;
        
        chains[1].addJointStart(new IKJoint(sphere_root_a.transform));
        chains[1].addConstraintStart(Vector3.Distance(sphere_root_a.position, root.GetChild(0).position));
        chains[1].First().changeParent(sphere_root_a.transform);
        chains[1].changeColor(Color.green);
        
        chains[2].addJointStart(new IKJoint(sphere_root_b.transform));
        chains[2].addConstraintStart(Vector3.Distance(sphere_root_a.position, root.GetChild(0).position));
        chains[2].First().changeParent(sphere_root_b.transform);
        chains[2].changeColor(Color.yellow);

        //chains[0].Last().SetPosition((sphere_root_a.position + sphere_root_b.position) / 2);
        
        root.position = (sphere_root_a.position + sphere_root_b.position) / 2;
        sphere_root_a.position = root.position;
        sphere_root_b.position = root.position;
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
            //BackwardFini = true;
            foreach(IKChain ch in chains)
            {
                //BackwardFini = false;
                ch.Backward();
                foreach (Transform root in gameObject.GetComponentsInChildren<Transform>())
                {
                    // TODO-2 : Créer une chaine cinématique entre srcNode et tr.
                    // TODO-2 : Créer une chaine cinématique entre tr et rootNode.
                    // rajouter un test pour savoir si tr est un carrefour
                    if (root.childCount > 1)
                    {
                        /*sphere_root_a.position = root.position;
                        sphere_root_b.position = root.position;*/
                        //ch.applyJointPositions();
                        Vector3 pos_a = chains[1].First().positionTransform;
                        Vector3 pos_b = chains[2].First().positionTransform;
                        /*Debug.Log("pos_a="+pos_a);
                        Debug.Log("pos_b="+pos_b);
                        Debug.Log("Root pos="+root.position);*/

                        chains[0].Last().SetPosition((pos_a + pos_b) / 2);
                        root.position = (pos_a + pos_b) / 2;

                        chains[1].First().SetPosition(chains[0].Last().positionTransform);
                        chains[2].First().SetPosition(chains[0].Last().positionTransform);
                    }
                }
                ch.ToTransform();
                //BackwardFini = true;
            }
        }
        for(int j = 0; j < nb_ite; j++)
        {
            foreach(IKChain ch in chains)
            {
                //ForwardFini = false;
                ch.Forward();
                ch.ToTransform();
                //ForwardFini = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Chains count="+chains.Count);
            foreach (IKChain ch in chains)
            ch.Check();
        }
    }
    void IKOneStep(bool down)
    {
        if(down)
        {
            for(int j = 0; j < nb_ite; j++)
            {
                foreach(IKChain ch in chains)
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
                foreach(IKChain ch in chains)
                {
                    ch.Forward();
                    ch.ToTransform();
                }
            }
        }

    }
}