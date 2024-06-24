using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AnimationCreator : MonoBehaviour
{
    public GameObject dicePrefab;
    public string filePath = "Assets/DiceData.csv";

    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();

    void Start()
    {
        ReadDataFromFile();
        CreateAnimationClip();
    }

    void ReadDataFromFile()
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            // Skip the header line
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');

                Vector3 position = new Vector3(
                    float.Parse(values[0]),
                    float.Parse(values[1]),
                    float.Parse(values[2])
                );

                Quaternion rotation = new Quaternion(
                    float.Parse(values[3]),
                    float.Parse(values[4]),
                    float.Parse(values[5]),
                    float.Parse(values[6])
                );

                positions.Add(position);
                rotations.Add(rotation);
            }
        }
    }

    void CreateAnimationClip()
    {
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;

        Keyframe[] posX = new Keyframe[positions.Count];
        Keyframe[] posY = new Keyframe[positions.Count];
        Keyframe[] posZ = new Keyframe[positions.Count];
        Keyframe[] rotX = new Keyframe[rotations.Count];
        Keyframe[] rotY = new Keyframe[rotations.Count];
        Keyframe[] rotZ = new Keyframe[rotations.Count];
        Keyframe[] rotW = new Keyframe[rotations.Count];

        for (int i = 0; i < positions.Count; i++)
        {
            float time = i * Time.fixedDeltaTime;
            Vector3 pos = positions[i];
            Quaternion rot = rotations[i];

            posX[i] = new Keyframe(time, pos.x);
            posY[i] = new Keyframe(time, pos.y);
            posZ[i] = new Keyframe(time, pos.z);
            rotX[i] = new Keyframe(time, rot.x);
            rotY[i] = new Keyframe(time, rot.y);
            rotZ[i] = new Keyframe(time, rot.z);
            rotW[i] = new Keyframe(time, rot.w);
        }

        clip.SetCurve("", typeof(Transform), "localPosition.x", new AnimationCurve(posX));
        clip.SetCurve("", typeof(Transform), "localPosition.y", new AnimationCurve(posY));
        clip.SetCurve("", typeof(Transform), "localPosition.z", new AnimationCurve(posZ));
        clip.SetCurve("", typeof(Transform), "localRotation.x", new AnimationCurve(rotX));
        clip.SetCurve("", typeof(Transform), "localRotation.y", new AnimationCurve(rotY));
        clip.SetCurve("", typeof(Transform), "localRotation.z", new AnimationCurve(rotZ));
        clip.SetCurve("", typeof(Transform), "localRotation.w", new AnimationCurve(rotW));

        GameObject dice = Instantiate(dicePrefab);
        Animation animation = dice.AddComponent<Animation>();
        animation.AddClip(clip, "DiceRoll");
        animation.Play("DiceRoll");
    }
}

