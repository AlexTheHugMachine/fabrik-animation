using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKChain_One_Chain : MonoBehaviour
{
    // Quand la chaine comporte une cible pour la racine.
    // Ce sera le cas que pour la chaine comportant le root de l'arbre.
    //private IKJoint rootTarget = null;
    // Quand la chaine à une cible à atteindre,
    // ce ne sera pas forcément le cas pour toutes les chaines.
    //private IKJoint endTarget = null;
    // Toutes articulations (IKJoint) triées de la racine vers la feuille. N articulations.
    private List<IKJoint> joints = new List<IKJoint>();
    private Vector3 rootTargetPos = new Vector3(0.0f, 0.0f, 0.0f);

    public Transform endTarget = null;

    // Contraintes pour chaque articulation : la longueur (à pour
    // ajouter des contraintes sur les angles). N-1 contraintes.
    private List<float> constraints = new List<float>();
    // Un cylndre entre chaque articulation (Joint). N-1 cylindres.
    private List<GameObject> cylinders = new List<GameObject>();
    // Créer la chaine d'IK en partant du noeud endNode et en remontant jusqu'au noeud plus haut, ou
    // jusqu'à la racine
    public IKChain_One_Chain(Transform _rootNode, Transform _endNode, Transform _rootTarget, Transform _endTarget)
    {
        Debug.Log("=== IKChain_One_Step::createChain: ===");
        // TODO : construire la chaine allant de _endNode vers _rootTarget en remontant dans l'arbre (ou
        // l'inverse en descente).
        // Chaque Transform dans Unity a accès à son parent 'tr.parent' 
        endTarget = _endTarget;
        rootTargetPos = new Vector3(_rootTarget.position.x, _rootTarget.position.y, _rootTarget.position.z);
        if(_rootNode.childCount > 0)
        {
            for (Transform tr = _rootNode; tr != _endNode && tr.childCount > 0; tr = tr.GetChild(0))
            {
                // TODO : ajouter un IKJoint à la liste joints
                joints.Add(new IKJoint(tr));
                // TODO : ajouter une contrainte à la liste constraints
                constraints.Add(Vector3.Distance(tr.position, tr.GetChild(0).position));
                // TODO : ajouter un cylindre entre les deux articulations
                /*Quaternion rot = Quaternion.FromToRotation(Vector3.up, tr.GetChild(0).position - tr.position);
                GameObject cyl = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                cyl.transform.position = tr.position + (tr.GetChild(0).position - tr.position) / 2.0f;
                cyl.transform.up = tr.GetChild(0).position - tr.position;
                cyl.transform.localScale = new Vector3(0.5f, (tr.GetChild(0).position - tr.position).magnitude / 2.0f, 0.5f);
                cyl.transform.parent = tr.parent;
                cyl.transform.rotation = rot;
                //cyl.transform.localRotation = rot;
                cylinders.Add(cyl);*/
            }
            joints.Add(new IKJoint(_endNode));
        }
    }
    public IKJoint First()
    {
        return joints[0];
    }
    public IKJoint Last()
    {
        return joints[ joints.Count-1 ];
    }
    public void Backward()
    {
        // TODO : une passe remontée de FABRIK. Placer le noeud N-1 sur la cible,
        // puis on remonte du noeud N-2 au noeud 0 de la liste
        // en résolvant les contrainte avec la fonction Solve de IKJoint.
        Last().SetPosition(endTarget.position);
        for (int i = joints.Count - 2; i >= 0; --i)
        {
            joints[i].Solve(joints[i + 1], constraints[i]);
        }
    }
    public void Forward()
    {
        // TODO : une passe descendante de FABRIK. Placer le noeud 0 sur son origine puis on descend.
        // Codez et deboguez déjà Backward avant d'écrire celle-ci.
        First().SetPosition(rootTargetPos);
        for (int i = 1; i < joints.Count; i++)
        {
            joints[i].Solve(joints[i - 1], constraints[i - 1]);
        }
    }
    public void ToTransform()
    {
        // TODO : pour tous les noeuds de la liste appliquer la position au transform : voir ToTransform
        // de IKJoint
        foreach (IKJoint j in joints)
        {
            j.ToTransform();
        }
    }
    public void Check()
    {
        // TODO : des Debug.Log pour afficher le contenu de la chaine (ne sert que pour le debug)
        Debug.Log("=== IKChain_One_Step::Check: ===");
        foreach (IKJoint j in joints)
        {
            Debug.Log("IKJoint : " + j.position + " with ID : " + joints.IndexOf(j));
        }
        foreach (float c in constraints)
        {
            Debug.Log("Constraint : " + c + " with ID : " + constraints.IndexOf(c));
        }

        Debug.Log("Nombre de cylindres : " + cylinders.Count);
        Debug.Log("Nombre de joints : " + joints.Count);
        Debug.Log("Nombre de contraintes : " + constraints.Count);
    }
}
