using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TextureAssignment : MonoBehaviour
{
#if UNITY_EDITOR
    public List<TextureSets> TextureList = new List<TextureSets>();
    public Shader ShaderPreset;
    private Texture[] Textures;

    private void Start()
    {
        LoadTexures();
        CreateMaterials();
        AssignMaterials();
    }

    public void LoadTexures()
    {
        Textures = Resources.LoadAll<Texture>("Textures");

        for (int i = 0; i < Textures.Length; i++)
        {
            Debug.Log($"Loaded: {Textures[i].name}");
        }
    }

    public void CreateMaterials()
    {
        for (int i = 0; i < Textures.Length; i++)
        {
            TextureList.Add(new TextureSets(Textures[i], Textures[i + 1], Textures[i + 2], Textures[i + 3], Textures[i + 4]));
            i += 4;
        }
    }

    public void AssignMaterials()
    {
        for (int i = 0; i < TextureList.Count; i++)
        {
            Material _material = new Material(ShaderPreset);
            _material.EnableKeyword("_NORMALMAP");
            _material.EnableKeyword("_METALLICGLOSSMAP");
            _material.EnableKeyword("_PARALLAXMAP");
            _material.name = TextureList[i].Albedo.name;
            _material.SetTexture("_MainTex", TextureList[i].Albedo);
            _material.SetTexture("_BumpMap", TextureList[i].Normal);
            _material.SetTexture("_ParallaxMap", TextureList[i].Height);
            _material.SetTexture("_MetallicGlossMap", TextureList[i].Metallic);

            AssetDatabase.CreateAsset(_material, $"Assets/Materials/Building/{_material.name}.mat");

        }
    }
}

[System.Serializable]
public class TextureSets
{
    public Texture Albedo;
    public Texture Metallic;
    public Texture Height;
    public Texture Normal;
    public Texture Emission;

    public TextureSets(Texture _Albedo, Texture _Emission,Texture _Height ,Texture _Metallic, Texture _Normal)
    {
        Albedo = _Albedo;
        Metallic = _Metallic;
        Height = _Height;
        Normal = _Normal;
        Emission = _Emission;
    }
#endif
}