using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;


public class DefaultImportSettings : AssetPostprocessor
{
    #region 
    void OnPostprocessTexture(Texture2D texture)
    {
        TextureImporter importer = assetImporter as TextureImporter;
        importer.textureType = TextureImporterType.Sprite;
        importer.anisoLevel = 1;
        importer.filterMode = FilterMode.Bilinear;
        importer.alphaIsTransparency = true;
        importer.isReadable = true;
        importer.maxTextureSize = 1024;
        importer.mipmapEnabled = true;
        importer.borderMipmap = true;
        importer.compressionQuality = 100;

        Object asset = AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(Texture2D));
        if (asset)
        {
            EditorUtility.SetDirty(asset);
        }
        else
        {
            texture.anisoLevel = 0;
            texture.filterMode = FilterMode.Trilinear;
        }
    }
    #endregion
}
#endif